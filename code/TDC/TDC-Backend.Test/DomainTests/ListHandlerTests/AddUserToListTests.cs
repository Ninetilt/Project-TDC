using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.ListHandlerTests
{
    public class AddUserToListTests
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

            _target._listMemberRepository.GetListMembers(1).Returns([]);
            _target._listRepository.GetById(1).Returns(new ToDoListDbo(1, "", true, false));
        }

        [Test]
        public void AddUserToList_UserIsMemberAlready_DoesNotCallRepository()
        {
            _target._listMemberRepository.GetListMembers(1).Returns(["test-user"]);

            _target.AddUserToList(1, "test-user");

            _target._listMemberRepository.DidNotReceive().AddListMember(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<bool>());
        }

        [Test]
        public void AddUserToList_UserIsNotMember_CallsRepository()
        {
            _target.AddUserToList(1, "test-user");

            _target._listMemberRepository.Received().AddListMember(1, "test-user", false);
        }

        [Test]
        public void AddUserToList_ListIsNotCollaborative_CallsRepository()
        {
            _target._listRepository.GetById(1).Returns(new ToDoListDbo(1, "", false, false));

            _target.AddUserToList(1, "test-user");

            _target._listMemberRepository.DidNotReceive().AddListMember(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<bool>());
        }

        [Test]
        public void AddUserToList_ListDoesNotExist_CallsRepository()
        {
            _target.AddUserToList(2, "test-user");

            _target._listMemberRepository.DidNotReceive().AddListMember(Arg.Any<long>(), Arg.Any<string>(), Arg.Any<bool>());
        }
    }
}
