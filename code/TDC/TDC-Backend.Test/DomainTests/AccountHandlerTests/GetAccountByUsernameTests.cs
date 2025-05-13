using FluentAssertions;
using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Test.DomainTests.AccountHandlerTests
{
    public class GetAccountByUsernameTests
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
        public void GetAccountByUsername_CallsRepository_AndReturnsCorrectDto()
        {
            _target._accountRepository.GetAccountByUsername("test-user").Returns(new AccountDbo("test-user", "test-email", "test-password", "test-description"));
            var expected = new AccountLoadingDto("test-user", "test-email", "test-description");

            var actual = _target.GetAccountByUsername("test-user");

            _target._accountRepository.Received().GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
