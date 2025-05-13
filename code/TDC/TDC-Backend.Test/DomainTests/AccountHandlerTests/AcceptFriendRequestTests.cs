using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class AcceptFriendRequestTests
    {
        private AccountHandler _target;
        private IAccountRepository _accountRepository;
        private IFriendRepository _friendRepository;
        private IFriendRequestRepository _friendRequestRepository;

        [SetUp]
        public void SetUp()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _friendRepository = Substitute.For<IFriendRepository>();
            _friendRequestRepository = Substitute.For<IFriendRequestRepository>();
            _target = new AccountHandler(_accountRepository, _friendRepository, _friendRequestRepository);
        }

        [Test]
        public void AcceptFriendRequest_UserIsNotFriendsWithRequestYet_CallsAddFriendForSenderAndReceiver() {
            _friendRepository.GetFriendsForUser("test-user").Returns(new List<string>());

            _target.AcceptFriendRequest("test-user", "test-receiver");

            _target.friendRepository.Received().AddFriend("test-user", "test-receiver");
            _target.friendRepository.Received().AddFriend("test-receiver", "test-user");
        }

        [Test]
        public void AcceptFriendRequest_UserIsFriendsWithRequest_DoesNotCallRepository()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns(new List<string> { "test-receiver"});

            _target.AcceptFriendRequest("test-user", "test-receiver");

            _target.friendRepository.DidNotReceive().AddFriend(Arg.Any<string>(), Arg.Any<string>()); 
        }

        [Test]
        public void AcceptFriendRequest_UserIsNotFriendsWithRequestYet_CallsDeleteFriendRequestOnBoth()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns(new List<string>());

            _target.AcceptFriendRequest("test-user", "test-receiver");

            _target.friendRequestRepository.Received().DeleteFriendRequest("test-user", "test-receiver");
            _target.friendRequestRepository.Received().DeleteFriendRequest("test-receiver", "test-user");
        }

        [Test]
        public void AcceptFriendRequest_UserIsFriendsWithRequest_DoesNotCallRequestRepository()
        {
            _friendRepository.GetFriendsForUser("test-user").Returns(new List<string> { "test-receiver" });

            _target.AcceptFriendRequest("test-user", "test-receiver");

            _target.friendRequestRepository.DidNotReceive().DeleteFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
