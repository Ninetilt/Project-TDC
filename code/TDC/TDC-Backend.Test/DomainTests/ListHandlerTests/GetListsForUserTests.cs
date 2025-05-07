using System.ComponentModel.DataAnnotations;
using NSubstitute;
using FluentAssertions;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class GetListsForUserTests
    {
        private ToDoListHandler _target;
        private IListRepository _listRepository;
        private IListItemRepository _listItemRepository;
        private IListMemberRepository _listMemberRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository);

            _listRepository.GetById(1).Returns(new ToDoListDbo(1, "list1", true, false));
            _listRepository.GetById(2).Returns(new ToDoListDbo(2, "list2", false, false));
        }

        [Test]
        public void GetListsForUser_UserHasNoLists_ReturnsEmptyList()
        {
            _target._listMemberRepository.GetListsForUser("test-user").Returns([]);

            var actual = _target.GetListsForUser("test-user");

            actual.Should().BeEmpty();
        }

        [Test]
        public void GetListsForUser_UserHasLists_ReturnsCorrectValue()
        {
            _target._listMemberRepository.GetListMembers(1).Returns(["test-user", "test-user-2"]);
            _target._listMemberRepository.GetListMembers(2).Returns(["test-user"]);

            _target._listMemberRepository.GetListsForUser("test-user").Returns([1, 2]);
            var item1 = new ToDoListItemDbo(1, 1, "item1", 1);
            var item2 = new ToDoListItemDbo(2, 2,"item2", 2);
            _target._listItemRepository.GetItemsForList(1).Returns([item1]);
            _target._listItemRepository.GetItemsForList(2).Returns([item2]);
            _target._listItemRepository.GetItemStatus(1, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(1, "test-user-2").Returns(true);
            _target._listItemRepository.GetItemStatus(2, "test-user").Returns(false);

            List<ToDoListItemLoadingDto> itemList1 = [new(1, "item1", true, ["test-user-2"], 1)];
            List<ToDoListItemLoadingDto> itemList2 = [new(2, "item2", false, [], 2)];
            var list1 = new ToDoListLoadingDto(1, "list1", itemList1, ["test-user", "test-user-2"], true);
            var list2 = new ToDoListLoadingDto(2, "list2", itemList2, ["test-user"], false);
            var expected = new List<ToDoListLoadingDto>
            {
                list1,
                list2
            };

            var actual = _target.GetListsForUser("test-user");

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
