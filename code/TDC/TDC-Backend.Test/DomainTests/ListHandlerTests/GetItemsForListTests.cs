using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class GetItemsForListTests
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
        public void GetItemsForList_ReturnsCorrectValues() {

            _target._listMemberRepository.GetListMembers(1).Returns(["test-user", "test-user-2"]);
            var item1 = new ToDoListItemDbo(1, 1, "item1", 1);
            var item2 = new ToDoListItemDbo(2, 2, "item2", 2);
            _target._listItemRepository.GetItemsForList(1).Returns([item1]);
            _target._listItemRepository.GetItemStatus(1, "test-user").Returns(true);
            _target._listItemRepository.GetItemStatus(1, "test-user-2").Returns(true);

            List<ToDoListItemLoadingDto> itemList1 = [new(1, "item1", true, ["test-user-2"], 1)];

            var actual = _target.GetItemsForList(1, "test-user");
        }
    }
}
