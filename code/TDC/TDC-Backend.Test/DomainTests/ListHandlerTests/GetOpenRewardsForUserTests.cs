using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class GetOpenRewardsForUserTests
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
        public void GetOpenRewardsForUser_CallsRepositoryForAllIds()
        {
            _openRewardsRepository.GetOpenRewardsForUser("test-user").Returns(new List<long> { 1, 2, 3 });
            _listRewardingRepository.GetRewardingById(Arg.Any<long>()).Returns("");

            _target.GetOpenRewardsForUser("test-user");

            _listRewardingRepository.Received().GetRewardingById(1);
            _listRewardingRepository.Received().GetRewardingById(2);
            _listRewardingRepository.Received().GetRewardingById(3);
        }
    }
}
