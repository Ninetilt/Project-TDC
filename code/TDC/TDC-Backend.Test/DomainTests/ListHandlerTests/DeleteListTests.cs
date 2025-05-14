using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class DeleteListTests
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
        public void DeleteList_UserIsCreator_CallsRepository()
        {
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(true);
            _target.DeleteList(1, "test-user");
            _target._listRepository.Received().DeleteList(1);
        }

        [Test]
        public void DeleteList_UserIsNotCreator_DoesNotCallRepository()
        {
            _target._listMemberRepository.UserIsCreator(1, "test-user").Returns(false);
            _target.DeleteList(1, "test-user");
            _target._listRepository.DidNotReceive().DeleteList(Arg.Any<long>());
        }
    }
}
