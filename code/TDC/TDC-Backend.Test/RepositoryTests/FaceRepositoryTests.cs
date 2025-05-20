using FluentAssertions;
using TDC.Backend.DataRepository.Test;
using TDC.Backend.DataRepository;

namespace TDC.Backend.Test.RepositoryTests
{
    public class FaceRepositoryTests
    {
        private FaceRepository _target;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var connectionFactory = TestStartUp.GetConnectionFactory();
            this._target = new FaceRepository(connectionFactory);
        }

        [Test]
        public void ExistingId_ReturnsCorrectValue()
        {
            var actual = _target.GetImageForFaceId("happy");
            actual.Should().NotBeNull();
            actual.Should().NotBeEmpty();
        }

        [Test]
        public void NonExistingId_ReturnsNull()
        {
            var actual = _target.GetImageForFaceId("not-existing");
            actual.Should().BeNull();
        }
    }
}
