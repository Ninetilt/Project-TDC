using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class FinishListTests
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
        }

        [Test]
        public void FinishList_UserIsNotMember_DoesNotCallRepository() {
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(true);
            _target._listMemberRepository.GetListMembers(1).Returns([]);
            _target._listItemRepository.GetItemsForList(1).Returns([new ToDoListItemDbo(1, 1, "", 1), new ToDoListItemDbo(2, 1, "", 1)]);

            _target._listItemRepository.GetItemStatus(1, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(1, "test-user-2").Returns(false);
            _target._listItemRepository.GetItemStatus(2, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(2, "test-user-2").Returns(false);


            _target.FinishList(1, "test-user");

            _target._listRepository.DidNotReceive().FinishList(Arg.Any<long>());
        }

        [Test]
        public void FinishList_NotAllItemsFinished_DoesNotCallRepository()
        {
            _target._listMemberRepository.GetListMembers(1).Returns(["test-user", "test-user-2"]);
            _target._listItemRepository.GetItemsForList(1).Returns([new ToDoListItemDbo(1, 1, "", 1), new ToDoListItemDbo(2, 1,"", 1)]);

            _target._listItemRepository.GetItemStatus(1, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(1, "test-user-2").Returns(true);
            _target._listItemRepository.GetItemStatus(2, "test-user").Returns(false);
            _target._listItemRepository.GetItemStatus(2, "test-user-2").Returns(false);


            _target.FinishList(1, "test-user");

            _target._listRepository.DidNotReceive().FinishList(Arg.Any<long>());
        }

        [Test]
        public void FinishList_AllItemsFinished_CallsRepository()
        {
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(true);
            _target._listMemberRepository.GetListMembers(1).Returns(["test-user", "test-user-2"]);
            _target._listItemRepository.GetItemsForList(1).Returns([new ToDoListItemDbo(1, 1, "", 1), new ToDoListItemDbo(2, 1, "", 1)]);

            _target._listItemRepository.GetItemStatus(1, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(1, "test-user-2").Returns(false);
            _target._listItemRepository.GetItemStatus(2, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(2, "test-user-2").Returns(false);


            _target.FinishList(1, "test-user");

            _target._listRepository.Received().FinishList(1);
        }
    }
}
