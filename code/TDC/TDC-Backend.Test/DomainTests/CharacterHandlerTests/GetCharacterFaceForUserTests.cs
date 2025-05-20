using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.Test.DomainTests.CharacterHandlerTests
{
    public class GetCharacterFaceForUserTests
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
        public void GetCharacterFaceForUser_CharacterDoesNotExist_DoesNotCallRepository()
        {
            _characterRepository.GetCharacterForUser("test-user").Returns((CharacterDbo?)null);
            _target.GetCharacterFaceForUser("test-user");
            _faceRepository.DidNotReceive().GetImageForFaceId(Arg.Any<string>());
        }

        [Test]
        public void GetCharacterFaceForUser_CharacterDoesExist_DoesNotCallRepository()
        {
            _characterRepository.GetCharacterForUser("test-user").Returns(new CharacterDbo("test-user", "face-id", "", 0));
            _target.GetCharacterFaceForUser("test-user");
            _faceRepository.Received().GetImageForFaceId("face-id");
        }
    }
}
