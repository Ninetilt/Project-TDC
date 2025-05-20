using FluentAssertions;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;

namespace TDC.Backend.Test.RepositoryTests
{
    internal class DefaultCharacterRepositoryTests
    {
        private DefaultCharacterRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new DefaultCharacterRepository(connectionFactory);
        }

        [Test]
        public void ExistingId_ReturnsCorrectValue()
        {
            var actual = _target.GetDefaultCharacterImage();
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
        }
    }
}
