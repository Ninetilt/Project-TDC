using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class UpdatePasswordTests
    {
        private AccountHandler _target;
        private IAccountRepository _accountRepository;

        [SetUp]
        public void SetUp()
        {
            _accountRepository = Substitute.For<IAccountRepository>();
            _target = new AccountHandler(_accountRepository);
        }

        [Test]
        public void UpdatePassword_UserDoesNotExist_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?)null);

            var actual = _target.UpdatePassword("test-user", "new-password");

            _target._accountRepository.DidNotReceive().UpdatePassword(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }

        [Test]
        public void UpdatePassword_PasswordIsOldPassword_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "new-password", ""));

            var actual = _target.UpdatePassword("test-user", "new-password");

            _target._accountRepository.DidNotReceive().UpdatePassword(Arg.Any<string>(), Arg.Any<string>());
            actual.Should().BeFalse();
        }

        [Test]
        public void UpdatePassword_UserExistsAndPasswordIsNew_CallRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "other-password", ""));

            var actual = _target.UpdatePassword("test-user", "new-password");

            _target._accountRepository.Received().UpdatePassword("test-user", "new-password");
            actual.Should().BeTrue();
        }
    }
}
