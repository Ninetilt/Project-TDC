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
    internal class GetCharacterBodyForUserTests
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
        public void GetCharacterBodyForUser_CharacterDoesNotExist_DoesNotCallRepository()
        {
            _characterRepository.GetCharacterForUser("test-user").Returns((CharacterDbo?)null);
            _target.GetCharacterBodyForUser("test-user");
            _characterBodyRepository.DidNotReceive().GetCharacterBodyImage(Arg.Any<string>());
        }

        [Test]
        public void GetCharacterBodyForUser_CharacterDoesExist_CallsRepository()
        {
            _characterRepository.GetCharacterForUser("test-user").Returns(new CharacterDbo("test-user", "", "test-color", 0));
            _target.GetCharacterBodyForUser("test-user");
            _characterBodyRepository.Received().GetCharacterBodyImage("test-color");
        }
    }
}
