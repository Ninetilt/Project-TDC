using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;
using FluentAssertions;

namespace TDC.Backend.Test.RepositoryTests
{
    public class CharacterBodyRepositoryTests
    {
        private CharacterBodyRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new CharacterBodyRepository(connectionFactory);
        }

        [Test]
        public void ExistingId_ReturnsCorrectValue()
        {
            var actual = _target.GetCharacterBodyImage("blue");
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
        }

        [Test]
        public void NonExistingId_ReturnsNull()
        {
            var actual = _target.GetCharacterBodyImage("not-existing");
            actual.Should().BeNull();
        }
    }
}
