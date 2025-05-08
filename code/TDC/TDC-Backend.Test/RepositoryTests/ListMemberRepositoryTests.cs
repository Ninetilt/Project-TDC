using FluentAssertions;
using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class ListMemberRepositoryTests
    {
        private ListMemberRepository _target;
        private AccountRepository _accountRepository;
        private ListRepository _listRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new ListMemberRepository(connectionFactory);
            this._accountRepository = new AccountRepository(connectionFactory);
            this._listRepository = new ListRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();
            this._listRepository.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();
            this._listRepository.CleanTable();
        }

        [Test]
        public void AddListMember_ListAndUserExist_AddsMember()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var user1 = new AccountDbo("user1", "mail1", "", "");
            var user2 = new AccountDbo("user2", "mail2", "", "");

            _accountRepository.CreateAccount(user1);
            _accountRepository.CreateAccount(user2);

            _target.AddListMember(listId, "user1", true);
            _target.AddListMember(listId, "user2", false);

            var actual = _target.GetListMembers(listId);
            var expected = new List<string>
            {
                "user1",
                "user2"
            };

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void AddListMember_ListDoesNotExist_ShowsSqlException()
        {
            var user1 = new AccountDbo("user1", "mail1", "", "");

            _accountRepository.CreateAccount(user1);

            var act = () => _target.AddListMember(1, "user1", true);

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void AddListMember_UserDoesNotExist_Throws()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var act = () => _target.AddListMember(listId, "user1", true);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void RemoveListMember_UserIsMember_RemovesMember()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var user1 = new AccountDbo("user1", "mail1", "", "");
            var user2 = new AccountDbo("user2", "mail2", "", "");

            _accountRepository.CreateAccount(user1);
            _accountRepository.CreateAccount(user2);

            _target.AddListMember(listId, "user1", true);
            _target.AddListMember(listId, "user2", false);

            var actual = _target.GetListMembers(listId);
            var expected = new List<string>
            {
                "user1",
                "user2"
            };

            actual.Should().BeEquivalentTo(expected);

            expected = ["user1"];
            _target.RemoveListMember(listId, "user2");
            actual = _target.GetListMembers(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveListMember_UserIsNotMember_DoesNotRemoveMember()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var user1 = new AccountDbo("user1", "mail1", "", "");
            var user2 = new AccountDbo("user2", "mail2", "", "");

            _accountRepository.CreateAccount(user1);
            _accountRepository.CreateAccount(user2);

            _target.AddListMember(listId, "user1", true);
            _target.AddListMember(listId, "user2", false);

            var actual = _target.GetListMembers(listId);
            var expected = new List<string>
            {
                "user1",
                "user2"
            };

            actual.Should().BeEquivalentTo(expected);

            _target.RemoveListMember(listId, "user3");
            actual = _target.GetListMembers(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UserIsCreator_ReturnsCorrectValue()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var user1 = new AccountDbo("user1", "mail1", "", "");
            var user2 = new AccountDbo("user2", "mail2", "", "");

            _accountRepository.CreateAccount(user1);
            _accountRepository.CreateAccount(user2);

            _target.AddListMember(listId, "user1", true);
            _target.AddListMember(listId, "user2", false);

            var actual = _target.UserIsCreator(listId, "user1");
            actual.Should().BeTrue();

            actual = _target.UserIsCreator(listId, "user2");
            actual.Should().BeFalse();
        }

        [Test]
        public void UserIsCreator_UserNotMember_ReturnsFalse()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", true, false));
            var actual = _target.UserIsCreator(listId, "user");
            actual.Should().BeFalse();
        }
    }
}
