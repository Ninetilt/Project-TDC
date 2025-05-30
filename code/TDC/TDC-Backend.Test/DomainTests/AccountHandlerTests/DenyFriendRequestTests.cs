﻿using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class DenyFriendRequestTests
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
        public void DenyFriendRequest_CallsDeleteOnRepository() {
            _target.DenyFriendRequest("test-user", "test-request");
            _friendRequestRepository.Received().DeleteFriendRequest("test-user", "test-request");
        }
    }
}
