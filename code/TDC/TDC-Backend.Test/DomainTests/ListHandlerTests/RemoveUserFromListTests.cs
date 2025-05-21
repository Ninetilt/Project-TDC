using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class RemoveUserFromListTests
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

            _target._listMemberRepository.GetListMembers(1).Returns(["test-user"]);
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(false);
        }

        [Test]
        public void RemoveUserFromList_UserIsNotMember_DoesNotCallRepository()
        {
            _target.RemoveUserFromList(1, "other-user");
            _target._listMemberRepository.DidNotReceive().RemoveListMember(Arg.Any<long>(), Arg.Any<string>());
        }

        [Test]
        public void RemoveUserFromList_UserIsCreator_DoesNotCallRepository()
        {
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(true);
            _target.RemoveUserFromList(1, "test-user");
            _target._listMemberRepository.DidNotReceive().RemoveListMember(Arg.Any<long>(), Arg.Any<string>());
        }

        [Test]
        public void RemoveUserFromList_UserIsNotCreatorAndNotMember_CallsRepository()
        {
            _target.RemoveUserFromList(1, "test-user");
            _target._listMemberRepository.Received().RemoveListMember(1, "test-user");
        }
    }
}
