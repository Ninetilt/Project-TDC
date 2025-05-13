using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class UpdateUsernameTests
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
        public void UpdateUsername_AccountDoesNotExist_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?) null);

            var actual = _target.UpdateUsername("other-user", "new-user");

            _target._accountRepository.DidNotReceive().UpdateUsername(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }

        [Test]
        public void UpdateUsername_NewNameIsTaken_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));
            _target._accountRepository.GetAccountByUsername("new-user").Returns(new AccountDbo("new-user", "", "", ""));

            var actual = _target.UpdateUsername("test-user", "new-user");

            _target._accountRepository.DidNotReceive().UpdateUsername(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }

        [Test]
        public void UpdateUsername_NewNameIsAvailable_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));
            _target._accountRepository.GetAccountByUsername("new-user").Returns((AccountDbo?) null);

            var actual = _target.UpdateUsername("test-user", "new-user");

            _target._accountRepository.Received().UpdateUsername("test-user", "new-user");
            actual.Should().BeTrue();
        }
    }
}
