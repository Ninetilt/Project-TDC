using FluentAssertions;
using TDC.Backend.DataRepository;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class ListRepositoryTests
    {
        private ListRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new ListRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
        }

        [Test]
        public void GetById_ReturnCorrectValues()
        {

            var listId1 = this._target.CreateList(new ToDoListDbo(0, "list1", false, false));

            var expected = new ToDoListDbo(listId1, "list1", false, false);

            var actual = this._target.GetById(listId1);

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
