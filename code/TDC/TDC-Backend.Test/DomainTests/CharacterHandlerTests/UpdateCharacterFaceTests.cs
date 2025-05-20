using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.CharacterHandlerTests
{
    public class UpdateCharacterFaceTests
    {
        private CharacterHandler _target;
        private ICharacterRepository _characterRepository;
        private IFaceRepository _faceRepository;
        private ICharacterBodyRepository _characterBodyRepository;
        private IDefaultCharacterRepository _defaultCharacterRepository;

        [SetUp]
        public void SetUp()
        {
            _characterRepository = Substitute.For<ICharacterRepository>();
            _faceRepository = Substitute.For<IFaceRepository>();
            _characterBodyRepository = Substitute.For<ICharacterBodyRepository>();
            _defaultCharacterRepository = Substitute.For<IDefaultCharacterRepository>();
            _target = new CharacterHandler(_characterRepository, _characterBodyRepository, _faceRepository, _defaultCharacterRepository);
        }

        [Test]
        public void UpdateCharacterFace_InvalidFaceId_DoesNotCallRepository()
        {
            _faceRepository.GetImageForFaceId("wrong-face-id").Returns((string?)null);
            _target.UpdateCharacterFace("test-user", "wrong-face-id");
            _characterRepository.DidNotReceive().UpdateFace(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void UpdateCharacterFace_ValidFaceId_CallsRepository()
        {
            _faceRepository.GetImageForFaceId("happy").Returns("valid");
            _target.UpdateCharacterFace("test-user", "happy");
            _characterRepository.Received().UpdateFace("test-user", "happy");
        }
    }
}
