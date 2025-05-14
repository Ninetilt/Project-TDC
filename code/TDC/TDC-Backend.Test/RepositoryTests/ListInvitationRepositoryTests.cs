using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using TDC.Backend.IDataRepository.Models;
using System.Linq.Expressions;
using FluentAssertions;
using Microsoft.Data.SqlClient;

namespace TDC.Backend.Test.RepositoryTests
{
    public class ListInvitationRepositoryTests
    {
        private ListInvitationRepository _target;
        private AccountRepository _accountRepository;
        private ListRepository _listRepository;
        private long testListId;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new ListInvitationRepository(connectionFactory);
            this._accountRepository = new AccountRepository(connectionFactory);
            this._listRepository = new ListRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();
            this._listRepository.CleanTable();

            var testAccount = new AccountDbo("test-user", "1", "", "");
            this._accountRepository.CreateAccount(testAccount);

            testAccount = new AccountDbo("test-user2", "2", "", "");
            this._accountRepository.CreateAccount(testAccount);

            var testList = new ToDoListDbo(0, "test-list", true, false);
            testListId = _listRepository.CreateList(testList);
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            this._accountRepository.CleanTable();
            this._listRepository.CleanTable();
        }

        [Test]
        public void GetListInvitationsForUser_UserExists_ReturnsCorrectValues() {
            _target.AddListInvitation("test-user", "test-user2", testListId);

            var expected = new List<ListInvitationDbo> { new ListInvitationDbo("test-user", "test-user2", testListId) };

            var actual = _target.GetInvitationsForUser("test-user");

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetInvitationsForUser_UserDoesNotExist_ReturnsEmptyList()
        {
            var actual = _target.GetInvitationsForUser("other-user");
            actual.Should().BeEmpty();
        }

        [Test]
        public void AddListInvitation_InvitationExists_ShouldThrowSqlException() {
            _target.AddListInvitation("test-user", "test-user2", testListId);

            var act = () => _target.AddListInvitation("test-user", "test-user2", testListId);

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void DeleteListInvitation_InvitationExists_DeletesInvitation() {
            _target.AddListInvitation("test-user", "test-user2", testListId);

            var expected = new List<ListInvitationDbo> { new ListInvitationDbo("test-user", "test-user2", testListId) };

            var actual = _target.GetInvitationsForUser("test-user");

            actual.Should().BeEquivalentTo(expected);

            _target.DeleteListInvitation("test-user", "test-user2", testListId);

            actual = _target.GetInvitationsForUser("test-user");
            actual.Should().BeEmpty();
        }
    }
}
