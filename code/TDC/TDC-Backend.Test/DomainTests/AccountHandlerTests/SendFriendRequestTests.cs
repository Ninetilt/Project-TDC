﻿using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

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

            _accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));
            _accountRepository.GetAccountByUsername("test-request").Returns(new AccountDbo("test-request", "", "", ""));
        }

        [Test]
        public void SendFriendRequest_UserHasNotSendRequestYetAndIsNotFriends_CallsRepository() {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-user").Returns([]);
            _friendRequestRepository.GetRequestsForUser("test-request").Returns([]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);
            

            _target.SendFriendRequest("test-user", "test-request");

            _friendRequestRepository.Received().AddFriendRequest("test-user", "test-request");
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

        [Test]
        public void SendFriendRequest_UserSendsToThemselves_DoesNotCallRepository()
        {
            _target.SendFriendRequest("test-user", "test-user");
            _friendRequestRepository.DidNotReceive().AddFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SendFriendRequest_UserDoesNotExist_DoesNotCallRepository()
        {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-user").Returns([]);
            _friendRequestRepository.GetRequestsForUser("test-request").Returns([]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);
            _accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?)null);

            _target.SendFriendRequest("test-user", "test-request");
            
            _friendRequestRepository.DidNotReceive().AddFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SendFriendRequest_RequestUserDoesNotExist_DoesNotCallRepository()
        {
            _friendRequestRepository.GetRequestsForUser("test-user").Returns([]);
            _friendRepository.GetFriendsForUser("test-user").Returns([]);
            _friendRequestRepository.GetRequestsForUser("test-request").Returns([]);
            _friendRepository.GetFriendsForUser("test-request").Returns([]);
            _accountRepository.GetAccountByUsername("test-request").Returns((AccountDbo?)null);

            _target.SendFriendRequest("test-user", "test-request");
            
            _friendRequestRepository.DidNotReceive().AddFriendRequest(Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
