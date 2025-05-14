using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class RegisterUserTests
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
        public void RegisterUser_EmailAndUsernameAreAvailable_CallsRepositoryAndReturnsTrue()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?)null);
            _target._accountRepository.GetAccountByEmail("test-email").Returns((AccountDbo?)null);

            var testUserDto = new AccountSavingDto("test-user", "test-email", "test-password", "test-description");

            var actual = _target.RegisterUser(testUserDto);

            _target._accountRepository.Received().CreateAccount(Arg.Is<AccountDbo>(dbo =>
                                                                                       dbo.Username == "test-user" &&
                                                                                       dbo.Email == "test-email" &&
                                                                                       dbo.Password == "test-password" &&
                                                                                       dbo.Description == "test-description"
                                                                                  ));
            actual.Should().BeTrue();
        }

        [Test]
        public void RegisterUser_EmailExists_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns((AccountDbo?)null);
            _target._accountRepository.GetAccountByEmail("test-email").Returns(new AccountDbo("", "test-email", "", ""));

            var testUserDto = new AccountSavingDto("test-user", "test-email", "test-password", "test-description");

            var actual = _target.RegisterUser(testUserDto);

            _target._accountRepository.DidNotReceive().CreateAccount(Arg.Any<AccountDbo>());
            actual.Should().BeFalse();
        }

        [Test]
        public void RegisterUser_UsernameExists_DoesNotCallRepositoryAndReturnsFalse()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "", "", ""));
            _target._accountRepository.GetAccountByEmail("test-email").Returns((AccountDbo?)null);

            var testUserDto = new AccountSavingDto("test-user", "test-email", "test-password", "test-description");

            var actual = _target.RegisterUser(testUserDto);

            _target._accountRepository.DidNotReceive().CreateAccount(Arg.Any<AccountDbo>());
            actual.Should().BeFalse();
        }
    }
}
