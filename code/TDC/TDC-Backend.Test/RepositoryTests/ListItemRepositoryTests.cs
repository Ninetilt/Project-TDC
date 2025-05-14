using FluentAssertions;
using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class ListItemRepositoryTests
    {
        private ListItemRepository _target;
        private ListRepository _listRepository;
        private AccountRepository _accountRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new ListItemRepository(connectionFactory);
            this._listRepository = new ListRepository(connectionFactory);
            this._accountRepository = new AccountRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            this._listRepository.CleanTable();
            this._accountRepository.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            this._listRepository.CleanTable();
            this._accountRepository.CleanTable();
        }

        [Test]
        public void GetItemsForList_ListExists_ReturnsItems()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));
            var id2 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d2", 2));

            var expected = new List<ToDoListItemDbo>()
            {
                new ToDoListItemDbo(id1, listId, "d1", 1),
                new ToDoListItemDbo(id2, listId, "d2", 2)
            };

            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetItemsForList_NoEntriesForList_ReturnsEmptyList()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));
            _target.AddItemToList(new ToDoListItemDbo(0, listId, "d2", 2));

            var actual = _target.GetItemsForList(listId + 1);
            actual.Should().BeEmpty();
        }

        [Test]
        public void AddItemToList_ListExists_AddsItem()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var expected = new List<ToDoListItemDbo>()
            {
                new ToDoListItemDbo(id1, listId, "d1", 1),
            };

            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void AddItemToList_ListDoesNotExist_ThrowsSqlException()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var act = () => _target.AddItemToList(new ToDoListItemDbo(0, listId + 1, "", 1));
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void DeleteItem_IdExists_DeletesItem()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));
            var id2 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d2", 2));

            var expected = new List<ToDoListItemDbo>()
            {
                new ToDoListItemDbo(id1, listId, "d1", 1),
                new ToDoListItemDbo(id2, listId, "d2", 2)
            };

            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            expected = [new ToDoListItemDbo(id1, listId, "d1", 1)];

            _target.DeleteItem(id2);

            actual = _target.GetItemsForList(listId);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void DeleteItem_IdDoesNotExist_DoesNotDeleteItem()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));
            var id2 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d2", 2));

            var expected = new List<ToDoListItemDbo>()
            {
                new ToDoListItemDbo(id1, listId, "d1", 1),
                new ToDoListItemDbo(id2, listId, "d2", 2)
            };

            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            _target.DeleteItem(id2 + 1);

            actual = _target.GetItemsForList(listId);

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateItemDescription_CorrectId_UpdatesDescription()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var expected = new List<ToDoListItemDbo>{ new ToDoListItemDbo(id1, listId, "d1", 1) };
            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            expected = [new ToDoListItemDbo(id1, listId, "new", 1)];
            _target.UpdateItemDescription(id1, "new");

            actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateItemDescription_WrongId_DoesNotUpdateDescription()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var expected = new List<ToDoListItemDbo> { new ToDoListItemDbo(id1, listId, "d1", 1) };
            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateItemDescription(id1 + 1, "new");

            actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateItemEffort_CorrectId_UpdatesEffort()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var expected = new List<ToDoListItemDbo> { new ToDoListItemDbo(id1, listId, "d1", 1) };
            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            expected = [new ToDoListItemDbo(id1, listId, "d1", 2)];
            _target.UpdateItemEffort(id1, 2);

            actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateItemEffort_WrongId_DoesNotUpdateEffort()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var expected = new List<ToDoListItemDbo> { new ToDoListItemDbo(id1, listId, "d1", 1) };
            var actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateItemEffort(id1 + 1, 2);

            actual = _target.GetItemsForList(listId);
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void SetItemStatus_ItemAndUserExist_SetsStatusCorrectly()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));
            _accountRepository.CreateAccount(new AccountDbo("user1", "", "", ""));

            var actual = _target.GetItemStatus(id1, "user1");
            actual.Should().BeFalse();

            _target.SetItemStatus(id1, "user1", true);
            actual = _target.GetItemStatus(id1, "user1");
            actual.Should().BeTrue();

            _target.SetItemStatus(id1, "user1", false);
            actual = _target.GetItemStatus(id1, "user1");
            actual.Should().BeFalse();
        }

        [Test]
        public void SetItemStatus_ItemDoesNotExist_ThrowsSqlException()
        {
            _accountRepository.CreateAccount(new AccountDbo("user1", "", "", ""));
            var act = () => _target.SetItemStatus(1, "user1", true);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void SetItemStatus_UserDoesNotExist_ThrowsSqlException()
        {
            var listId = _listRepository.CreateList(new ToDoListDbo(0, "", false, false));
            var id1 = _target.AddItemToList(new ToDoListItemDbo(0, listId, "d1", 1));

            var act = () => _target.SetItemStatus(id1, "other", true);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void GetItemStatus_NoEntry_ReturnsFalse()
        {
            var actual = _target.GetItemStatus(1, "user");
            actual.Should().BeFalse();
        }
    }
}
