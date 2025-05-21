using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using FluentAssertions;
using TDC.Backend.IDataRepository.Models;
using Microsoft.Data.SqlClient;

namespace TDC.Backend.Test.RepositoryTests
{
    public class OpenRewardsRepositoryTests
    {
        private OpenRewardsRepository _target;
        private ListRepository _listRepository;
        private AccountRepository _accountRepository;
        private ListRewardingRepository _listRewardingRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new OpenRewardsRepository(connectionFactory);
            _listRepository = new ListRepository(connectionFactory);
            _accountRepository = new AccountRepository(connectionFactory);
            _listRewardingRepository = new ListRewardingRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
            _accountRepository.CleanTable();
            _listRewardingRepository.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
            _accountRepository.CleanTable();
            _listRewardingRepository.CleanTable();
        }

        [Test]
        public void GetOpenRewardsForUser_UserHasNoEntries_ReturnsEmptyList()
        {
            var actual = _target.GetOpenRewardsForUser("test-user");

            actual.Should().BeEmpty();
        }

        [Test]
        public void AddOpenRewardForUser_AddsValues()
        {
            var listId1 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));
            var listId2 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));

            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));

            _listRewardingRepository.AddNewRewarding(listId1, "");
            _listRewardingRepository.AddNewRewarding(listId2, "");

            _target.AddOpenRewardForUser("test-user", listId1);
            _target.AddOpenRewardForUser("test-user", listId2);

            var expected = new List<long>
            {
                listId1, listId2
            };

            var actual = _target.GetOpenRewardsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void AddOpenRewardForUser_DuplicateEntry_ThrowsSqlException()
        {
            var listId1 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));

            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));

            _listRewardingRepository.AddNewRewarding(listId1, "");

            _target.AddOpenRewardForUser("test-user", listId1);

            var act = () => _target.AddOpenRewardForUser("test-user", listId1);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void AddOpenRewardForUser_InvalidId_ThrowsSqlException()
        {
            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));

            var act = () => _target.AddOpenRewardForUser("test-user", 1);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void AddOpenRewardForUser_InvalidUsername_ThrowsSqlException()
        {
            var listId1 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));
            _listRewardingRepository.AddNewRewarding(listId1, "");

            var act = () => _target.AddOpenRewardForUser("test-user", listId1);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void RemoveSeenReward_ExistingEntry_RemovesEntry()
        {
            var listId1 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));
            var listId2 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));

            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));

            _listRewardingRepository.AddNewRewarding(listId1, "");
            _listRewardingRepository.AddNewRewarding(listId2, "");

            _target.AddOpenRewardForUser("test-user", listId1);
            _target.AddOpenRewardForUser("test-user", listId2);

            var expected = new List<long>
            {
                listId1, listId2
            };

            var actual = _target.GetOpenRewardsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            expected = new List<long> {
                listId1
            };

            _target.RemoveSeenReward("test-user", listId2);

            actual = _target.GetOpenRewardsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void RemoveSeenReward_NonExistingEntry_DoesNotRemoveEntry()
        {
            var listId1 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));
            var listId2 = _listRepository.CreateList(new ToDoListDbo(0, "", false, true));

            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));

            _listRewardingRepository.AddNewRewarding(listId1, "");
            _listRewardingRepository.AddNewRewarding(listId2, "");

            _target.AddOpenRewardForUser("test-user", listId1);
            _target.AddOpenRewardForUser("test-user", listId2);

            var expected = new List<long>
            {
                listId1, listId2
            };

            var actual = _target.GetOpenRewardsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.RemoveSeenReward("test-user", listId2 + 1);

            actual = _target.GetOpenRewardsForUser("test-user");
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
