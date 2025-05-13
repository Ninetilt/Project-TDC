using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class UpdateUserDescriptionTests
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
        public void UpdateUserDescription_UserExists_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));

            var actual = _target.UpdateUserDescription("test-user", "new-description");

            _target._accountRepository.Received().UpdateDescription("test-user", "new-description");
            actual.Should().BeTrue();
        }

        [Test]
        public void UpdateUserDescription_UserDoesNotExist_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?) null);

            var actual = _target.UpdateUserDescription("test-user", "new-description");

            _target._accountRepository.DidNotReceive().UpdateDescription(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }
    }
}
