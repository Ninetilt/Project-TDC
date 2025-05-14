using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using TDC.Backend.IDataRepository.Models;
using FluentAssertions;
using Microsoft.Data.SqlClient;

namespace TDC.Backend.Test.RepositoryTests
{
    public class FriendRequestRepositoryTests
    {
        private FriendRequestRepository _target;
        private AccountRepository _accountRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new FriendRequestRepository(connectionFactory);
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
        public void GetFriendRequestsForUser_UserExists_ReturnsCorrectValues()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriendRequest("test-user", "friend1");
            _target.AddFriendRequest("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetFriendRequestsForUser_UserDoesNotExist_ReturnsEmptyList()
        {
            var actual = _target.GetRequestsForUser("other-user");
            actual.Should().BeEmpty();
        }

        [Test]
        public void SendFriendRequest_RequestAlreadyExists_ShouldThrowSqlException()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            _accountRepository.CreateAccount(friend1);
            _target.AddFriendRequest("test-user", "friend1");

            var act = () => _target.AddFriendRequest("test-user", "friend1");

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void DeleteRequest_RequestsExists_RemovesRequest()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriendRequest("test-user", "friend1");
            _target.AddFriendRequest("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            expected = ["friend1"];
            _target.DeleteFriendRequest("test-user", "friend2");
            actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteRequest_RequestDoesNotExist_DoesNotRemoveAny()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriendRequest("test-user", "friend1");
            _target.AddFriendRequest("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.DeleteFriendRequest("test-user", "friend3");
            actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteRequest_UserDoesNotExist_DoesNotRemoveAny()
        {
            var friend1 = new AccountDbo("friend1", "1", "", "");
            var friend2 = new AccountDbo("friend2", "2", "", "");
            _accountRepository.CreateAccount(friend1);
            _accountRepository.CreateAccount(friend2);

            _target.AddFriendRequest("test-user", "friend1");
            _target.AddFriendRequest("test-user", "friend2");

            var expected = new List<string>
            {
                "friend1",
                "friend2"
            };

            var actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.DeleteFriendRequest("other-test-user", "friend2");
            actual = _target.GetRequestsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
