using FluentAssertions;
using Microsoft.Data.SqlClient;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.RepositoryTests
{
    public class CharacterRepositoryTests
    {
        private CharacterRepository _target;
        private AccountRepository _accountRepository;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new CharacterRepository(connectionFactory);
            _accountRepository = new AccountRepository(connectionFactory);
        }

        [SetUp]
        public void Setup()
        {
            this._target.CleanTable();
            _accountRepository.CleanTable();
            _accountRepository.CreateAccount(new AccountDbo("test-user", "", "", ""));
        }

        [TearDown]
        public void TearDown()
        {
            this._target.CleanTable();
            _accountRepository.CleanTable();
        }

        [Test]
        public void AddCharacter_DoesNotExistYet_AddsCharacter()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var actual = _target.GetCharacterForUser("test-user");

            actual.Should().BeEquivalentTo(character);
        }

        [Test]
        public void AddCharacter_ExistsAlready_ThrowsSqlException()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var act = () => this._target.AddCharacter(character);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void AddCharacter_InvalidFaceId_ThrowsSqlException()
        {
            var character = new CharacterDbo("test-user", "false-id", "blue", 0);

            var act = () => this._target.AddCharacter(character);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void AddCharacter_InvalidColor_ThrowsSqlException()
        {
            var character = new CharacterDbo("test-user", "happy", "no-color", 0);

            var act = () => this._target.AddCharacter(character);
            act.Should().Throw<SqlException>();
        }

        [Test]
        public void GetCharacterForUser_DoesNotExist_ReturnsNull()
        {
            var actual = _target.GetCharacterForUser("test-user");
            actual.Should().BeNull();
        }

        [Test]
        public void UpdateFace_ValidFaceId_UpdatesFace()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var actual = _target.GetCharacterForUser("test-user");

            actual.Should().BeEquivalentTo(character);

            character.FaceId = "suspicious";
            _target.UpdateFace("test-user", "suspicious");

            actual = _target.GetCharacterForUser("test-user");

            actual.Should().BeEquivalentTo(character);
        }

        [Test]
        public void UpdateFace_InvalidFaceId_ThrowsSqlException()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var actual = () => _target.UpdateFace("test-user", "invalid-face");
            actual.Should().Throw<SqlException>();
        }

        [Test]
        public void UpdateColor_ValidColor_UpdatesColor()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var actual = _target.GetCharacterForUser("test-user");

            actual.Should().BeEquivalentTo(character);

            character.Color = "green";
            _target.UpdateColor("test-user", "green");

            actual = _target.GetCharacterForUser("test-user");

            actual.Should().BeEquivalentTo(character);
        }

        [Test]
        public void UpdateColor_InvalidColor_ThrowsSqlException()
        {
            var character = new CharacterDbo("test-user", "happy", "blue", 0);
            this._target.AddCharacter(character);

            var actual = () => _target.UpdateColor("test-user", "no-color");
            actual.Should().Throw<SqlException>();
        }
    }
}
