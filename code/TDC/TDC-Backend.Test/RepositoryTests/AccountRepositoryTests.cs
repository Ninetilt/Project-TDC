using FluentAssertions;
using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class AccountRepositoryTests
    {
        private AccountRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new AccountRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
        }

        [Test]
        public void GetAccountByUsername_AccountExists_ReturnsCorrectData()
        {
            var expected = new AccountDbo("test-user", "test-mail@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = _target.GetAccountByUsername("test-user");

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAccountByUsername_AccountDoesNotExist_ReturnsNull()
        {
            var expected = new AccountDbo("test-user", "test-mail@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = _target.GetAccountByUsername("other-test-user");

            actual.Should().BeNull();
        }

        [Test]
        public void GetAccountEmail_AccountExists_ReturnsCorrectData()
        {
            var expected = new AccountDbo("test-user", "test-mail@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = _target.GetAccountByEmail("test-mail@mail.de");

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetAccountByEmail_AccountDoesNotExist_ReturnsNull()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = _target.GetAccountByEmail("wrong-mail");

            actual.Should().BeNull();
        }

        [Test]
        public void CreateAccount_AccountDoesNotExist_AddsAccount()
        {
            _target.CreateAccount(new AccountDbo("other-user", "", "", ""));
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = _target.GetAccountByUsername("test-user");

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CreateAccount_UsernameExists_ThrowsSqlException()
        {
            _target.CreateAccount(new AccountDbo("test-user", "", "", ""));
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            var act = () => _target.CreateAccount(expected);

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void CreateAccount_EmailExists_ThrowsSqlException()
        {
            _target.CreateAccount(new AccountDbo("test-user", "mail", "", ""));
            var expected = new AccountDbo("other-user", "mail", "pw1", "test");

            var act = () => _target.CreateAccount(expected);

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void DeleteAccount_UsernameExists_DeletesAccount()
        {
            var acc = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(acc);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().NotBeNull();

            _target.DeleteAccount("test-user");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeNull();
        }

        [Test]
        public void DeleteAccount_UsernameDoesNot_DoesNotDeleteAccount()
        {
            var acc = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(acc);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().NotBeNull();

            _target.DeleteAccount("other-test-user");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().NotBeNull();
        }

        [Test]
        public void UpdateUsername_AccountExists_UpdatesUsername()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateUsername("test-user", "new-user");
            actual = this._target.GetAccountByUsername("new-user");
            expected.Username = "new-user";
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateUsername_AccountDoesNotExist_DoesNotUpdateUsername()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateUsername("not-test-user", "new-user");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateUsername_UsernameTaken_ThrowsSqlException()
        {
            _target.CreateAccount(new AccountDbo("user1", "mail1", "", ""));
            _target.CreateAccount(new AccountDbo("user2", "mail2", "", ""));

            var act = () => _target.UpdateUsername("user1", "user2");

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void UpdateEmail_AccountExists_UpdatesEmail()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateEmail("test-user", "new-mail");
            actual = this._target.GetAccountByUsername("test-user");
            expected.Email = "new-mail";
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateEmail_AccountDoesNotExist_DoesNotUpdateEmail()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateEmail("not-test-user", "new-mail");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateEmail_EmailTaken_ThrowsSqlException()
        {
            _target.CreateAccount(new AccountDbo("user1", "mail1", "", ""));
            _target.CreateAccount(new AccountDbo("user2", "mail2", "", ""));

            var act = () => _target.UpdateEmail("user1", "mail2");

            act.Should().Throw<SqlException>();
        }

        [Test]
        public void UpdateDescription_AccountExists_UpdatesDescription()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateDescription("test-user", "new");
            actual = this._target.GetAccountByUsername("test-user");
            expected.Description = "new";
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdateDescription_AccountDoesNotExist_DoesNotUpdateDescription()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdateDescription("other-test-user", "new");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdatePassword_AccountExists_UpdatesPassword()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdatePassword("test-user", "new");
            actual = this._target.GetAccountByUsername("test-user");
            expected.Password = "new";
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void UpdatePassword_AccountDoesNotExist_DoesNotUpdatePassword()
        {
            var expected = new AccountDbo("test-user", "test@mail.de", "pw1", "test");

            _target.CreateAccount(expected);

            var actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);

            _target.UpdatePassword("other-test-user", "new");
            actual = this._target.GetAccountByUsername("test-user");
            actual.Should().BeEquivalentTo(expected);
        }
    }
}
