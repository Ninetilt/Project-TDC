using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using TDC.Backend.IDataRepository.Models;
using FluentAssertions;
using Microsoft.Data.SqlClient;

namespace TDC.Backend.Test.RepositoryTests
{
    public class ListRewardingRepositoryTests
    {
        private ListRewardingRepository _target;
        private ListRepository _listRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new ListRewardingRepository(connectionFactory);
            _listRepository = new ListRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            _listRepository.CleanTable();
        }

        [Test]
        public void GetRewardingById_NoMatchingId_ReturnsNull()
        {
            var actual = _target.GetRewardingById(1);
            actual.Should().BeNull();
        }

        [Test]
        public void AddNewRewarding_AddsRewarding()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "test-list", false, true));

            _target.AddNewRewarding(listId, "test-message");
            
            var actual = _target.GetRewardingById(listId);
            var expected = "test-message";

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void AddNewRewarding_NoValidList_ThrowsSqlException()
        {
            var act = () => _target.AddNewRewarding(1, "test-message");
            act.Should().Throw<SqlException>();
        }
    }
}
