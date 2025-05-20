using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDC.Backend.Domain;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.Test.DomainTests.CharacterHandlerTests
{
    public class GetDefaultCharacterImageTests
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
        public void GetDefaultCharacterImage_CallsRepository()
        {
            _target.GetDefaultCharacterImage();
            _defaultCharacterRepository.Received().GetDefaultCharacterImage();
        }
    }
}
