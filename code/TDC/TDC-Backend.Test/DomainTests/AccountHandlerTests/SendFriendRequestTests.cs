using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class SendFriendRequestTests
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
        public void SendFriendRequest_UserHasNotSendRequestYetAndIsntFriends_CallsRepository() {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-user").Returns([]);

            _target.SendFriendRequest("test-sender", "test-user");

            _friendRequestRepository.Received().AddFriendRequest("test-user", "test-sender");
        }

        [Test]
        public void SendFriendRequest_UserHasSentRequestBefore_DoesNotCallRepository() {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns(["test-sender"]);
            _friendRepository.GetFriendsForUser("test-user").Returns([]);

            _target.SendFriendRequest("test-sender", "test-user");

            _friendRequestRepository.DidNotReceive().AddFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SendFriendRequest_UserIsFriendAlready_DoesNotCallRepository()
        {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-user").Returns(["test-sender"]);

            _target.SendFriendRequest("test-sender", "test-user");

            _friendRequestRepository.DidNotReceive().AddFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
