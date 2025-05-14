using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class DenyListInvitationTests
    {
        private ToDoListHandler _target;
        private IListRepository _listRepository;
        private IListItemRepository _listItemRepository;
        private IListMemberRepository _listMemberRepository;
        private IListInvitationRepository _listInvitationRepository;

        [SetUp]
        public void SetUp()
        {
            _listRepository = Substitute.For<IListRepository>();
            _listItemRepository = Substitute.For<IListItemRepository>();
            _listMemberRepository = Substitute.For<IListMemberRepository>();
            _listInvitationRepository = Substitute.For<IListInvitationRepository>();
            _target = new ToDoListHandler(_listRepository, _listItemRepository, _listMemberRepository, _listInvitationRepository);
        }

        [Test]
        public void DenyListInvitation_CallsDeleteForEachUserWhoInvitedOnCorrectList() {
            _listInvitationRepository.GetInvitationsForUser("test-user").Returns(new List<ListInvitationDbo>
            {
                new ListInvitationDbo("test-user", "user1", 1),
                new ListInvitationDbo("test-user", "user2", 1),
                new ListInvitationDbo("test-user", "user3", 1),
                new ListInvitationDbo("test-user", "user1", 2),
                new ListInvitationDbo("test-user", "user2", 2),
            });

            _target.DenyListInvitation(1, "test-user");

            _listInvitationRepository.Received().DeleteListInvitation("test-user", "user1", 1);
            _listInvitationRepository.Received().DeleteListInvitation("test-user", "user2", 1);
            _listInvitationRepository.Received().DeleteListInvitation("test-user", "user3", 1);
            _listInvitationRepository.DidNotReceive().DeleteListInvitation("test-user", "user1", 2);
            _listInvitationRepository.DidNotReceive().DeleteListInvitation("test-user", "user2", 2);
        }
    }
}
