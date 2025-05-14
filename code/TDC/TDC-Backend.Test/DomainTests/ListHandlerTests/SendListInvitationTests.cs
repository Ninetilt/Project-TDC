using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class SendListInvitationTests
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
        public void SendListInvitation_AlreadyInvitedByUser_DoesNotCallRepository() {
            _listInvitationRepository.GetInvitationsForUser("test-user").Returns([new ListInvitationDbo("test-user", "test-sender", 1)]);

            _target.SendListInvitation(1, "test-sender", "test-user");

            _listInvitationRepository.DidNotReceive().AddListInvitation(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<long>());
        }

        [Test]
        public void SendListInvitation_NotInvitedYet_CallsRepository()
        {
            _listInvitationRepository.GetInvitationsForUser("test-user").Returns([]);

            _target.SendListInvitation(1, "test-sender", "test-user");

            _listInvitationRepository.Received().AddListInvitation("test-user", "test-sender", 1);
        }
    }
}
