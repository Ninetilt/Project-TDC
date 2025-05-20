using NSubstitute;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.CharacterHandlerTests
{
    public class UpdateCharacterColorTests
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
        public void UpdateCharacterColor_InvalidColor_DoesNotCallRepository()
        {
            _characterBodyRepository.GetCharacterBodyImage("wrong-color").Returns((string?)null);
            _target.UpdateCharacterColor("test-user", "wrong-color");
            _characterRepository.DidNotReceive().UpdateColor(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void UpdateCharacterColor_ValidColor_CallsRepository()
        {
            _characterBodyRepository.GetCharacterBodyImage("valid-color").Returns("valid");
            _target.UpdateCharacterColor("test-user", "valid-color");
            _characterRepository.Received().UpdateColor("test-user", "valid-color");
        }
    }
}
