using FluentAssertions;
using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class FriendRepositoryTests
    {
        private FriendRepository _target;
        private AccountRepository _accountRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new FriendRepository(connectionFactory);
            this._accountRepository = new AccountRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();

            var testAccount = new AccountDbo("test-user", "", "", "");
            this._accountRepository.CreateAccount(testAccount);
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();
        }

        [Test]
        public void GetFriendsForUser_UserExists_ReturnsCorrectValues() {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriend("test-user", "friend1");
            _target.AddFriend("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetFriendsForUser_UserDoesNotExist_ReturnsEmptyList()
        {
            var actual = _target.GetFriendsForUser("other-user");
            actual.Should().BeEmpty();
        }

        [Test]
        public void AddFriend_FriendAlreadyExists_ShouldThrowSqlException() {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            _accountRepository.CreateAccount(friend1);
            _target.AddFriend("test-user", "friend1");

            var act = () => _target.AddFriend("test-user", "friend1");

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void RemoveFriend_FriendExists_RemovesFriend() {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriend("test-user", "friend1");
            _target.AddFriend("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            expected = ["friend1"];
            _target.RemoveFriend("test-user", "friend2");
            actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveFriend_FriendDoesNotExist_DoesNotRemoveAny()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriend("test-user", "friend1");
            _target.AddFriend("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.RemoveFriend("test-user", "friend3");
            actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveFriend_UserDoesNotExist_DoesNotRemoveAny()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriend("test-user", "friend1");
            _target.AddFriend("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.RemoveFriend("other-test-user", "friend2");
            actual = _target.GetFriendsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
