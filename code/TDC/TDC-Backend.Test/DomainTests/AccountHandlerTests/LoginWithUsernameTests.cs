using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class LoginWithUsernameTests
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
        public void LoginWithUsername_CorrectMailAndPassword_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithUsername("test-user", "test-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeTrue();
        }

        [Test]
        public void LoginWithUsername_CorrectMailAndWrongPassword_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));

            var actual = _target.LoginWithUsername("test-user", "other-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeFalse();
        }

        [Test]
        public void LoginWithUsername_WrongMail_CallsRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?)null);

            var actual = _target.LoginWithUsername("test-user", "test-password");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeFalse();
        }
    }
}
