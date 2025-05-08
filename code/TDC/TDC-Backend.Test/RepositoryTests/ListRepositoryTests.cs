using FluentAssertions;
using NUnit.Framework.Constraints;
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
        public void GetById_IdExists_ReturnCorrectValues()
        {

            var listId1 = this._target.CreateList(new ToDoListDbo(0, "list1", false, false));

            var expected = new ToDoListDbo(listId1, "list1", false, false);

            var actual = this._target.GetById(listId1);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetById_IdDoesNotExist_ReturnsNull()
        {
            var listId1 = this._target.CreateList(new ToDoListDbo(0, "list1", false, false));

            var actual = this._target.GetById(listId1 + 1);

            actual.Should().BeNull();
        }

        [Test]
        public void CreateList_AddsListToDatabase()
        {
            var expected1 = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(expected1);
            expected1.Id = listId1;

            var expected2 = new ToDoListDbo(0, "list2", true, false);
            var listId2 = this._target.CreateList(expected2);
            expected2.Id = listId2;

            var actual = this._target.GetById(listId1);
            actual.Should().BeEquivalentTo(expected1);

            actual = this._target.GetById(listId2);
            actual.Should().BeEquivalentTo(expected2);
        }

        [Test]
        public void UpdateListTitle_CorrectListId_UpdatesListTitle()
        {
            var expected = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(expected);

            expected.Id = listId1;

            this._target.UpdateListTitle(listId1, "new-title");
            expected.Name = "new-title";

            var actual = _target.GetById(listId1);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateListTitle_WrongListId_DoesNotUpdateListTitle()
        {
            var expected = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(expected);

            expected.Id = listId1;

            this._target.UpdateListTitle(listId1 + 1, "new-title");

            var actual = _target.GetById(listId1);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteList_ListIdExists_DeletesList()
        {
            var newList = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(newList);
            newList.Id = listId1;

            var actual = this._target.GetById(listId1);
            actual.Should().NotBeNull();

            this._target.DeleteList(listId1);
            actual = this._target.GetById(listId1);
            actual.Should().BeNull();
        }

        [Test]
        public void DeleteList_ListIdDoesNotExist_DoesNotDeleteList()
        {
            var newList = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(newList);
            newList.Id = listId1;

            var actual = this._target.GetById(listId1);
            actual.Should().NotBeNull();

            this._target.DeleteList(listId1 + 1);
            actual = this._target.GetById(listId1);
            actual.Should().NotBeNull();
        }

        [Test]
        public void FinishList_ListIdCorrect_SetsFinishedToTrue()
        {
            var newList = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(newList);
            newList.Id = listId1;

            var actual = this._target.GetById(listId1);
            actual.IsFinished.Should().BeFalse();

            this._target.FinishList(listId1);
            actual = this._target.GetById(listId1);

            actual.IsFinished.Should().BeTrue();
        }

        [Test]
        public void FinishList_ListIdFalse_DoesNotSetFinishedToTrue()
        {
            var newList = new ToDoListDbo(0, "list1", false, false);
            var listId1 = this._target.CreateList(newList);
            newList.Id = listId1;

            var actual = this._target.GetById(listId1);
            actual.IsFinished.Should().BeFalse();

            this._target.FinishList(listId1 + 1);
            actual = this._target.GetById(listId1);

            actual.IsFinished.Should().BeFalse();
        }
    }
}
