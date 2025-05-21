using FluentAssertions;
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
    public class LoadListInvitationsForUserTests
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
        public void LoadListInvitationsForUser_CallsRepository() {
            _listInvitationRepository.GetInvitationsForUser("test-user").Returns([]);
            _target.LoadListInvitationsForUser("test-user");
            _target._listInvitationRepository.Received().GetInvitationsForUser("test-user");
        }

        [Test]
        public void LoadListInvitationsForUser_ReturnsCorrectValue()
        {
            _listInvitationRepository.GetInvitationsForUser("test-user").Returns([new ListInvitationDbo("test-user", "test-sender", 1)]);
            var actual = _target.LoadListInvitationsForUser("test-user");
            var expected = new List<ListInvitationDto>()
            {
                new ListInvitationDto("test-sender", 1)
            };

            actual.Should().BeEquivalentTo(expected);
        }
    }
}
