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
        private IListInvitationRepository _listInvitationRepository;
        private IListRewardingRepository _listRewardingRepository;
        private IOpenRewardsRepository _openRewardsRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _listInvitationRepository = Substitute.For<IListInvitationRepository>();
            _listRewardingRepository = Substitute.For<IListRewardingRepository>();
            _openRewardsRepository = Substitute.For<IOpenRewardsRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository, _listInvitationRepository, _listRewardingRepository, _openRewardsRepository);

            _listRepository.GetById(1).Returns(new ToDoListDbo(1, "list1", true, false));
            _listRepository.GetById(2).Returns(new ToDoListDbo(2, "list2", false, false));
            _listRepository.GetById(3).Returns(new ToDoListDbo(3, "list3", false, true));
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

            _target._listMemberRepository.GetListsForUser("test-user").Returns([1, 2, 3]);

            var list1 = new ToDoListLoadingDto(1, "list1", true);
            var list2 = new ToDoListLoadingDto(2, "list2", false);
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
