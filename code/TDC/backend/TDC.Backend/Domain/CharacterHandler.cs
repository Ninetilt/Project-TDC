using TDC.Backend.IDataRepository;
using TDC.Backend.IDomain;

namespace TDC.Backend.Domain
{
    public class CharacterHandler(
        ICharacterRepository characterRepository, 
        ICharacterBodyRepository characterBodyRepository,
        IFaceRepository faceRepository,
        IDefaultCharacterRepository defaultCharacterRepository): ICharacterHandler
    {
        private ICharacterRepository _characterRepository = characterRepository;
        private ICharacterBodyRepository _characterBodyRepository = characterBodyRepository;
        private IFaceRepository _faceRepository = faceRepository;
        private IDefaultCharacterRepository _defaultCharacterRepository = defaultCharacterRepository;

        public string GetDefaultCharacterImage()
        {
            return _defaultCharacterRepository.GetDefaultCharacterImage();
        }

        public string? GetCharacterFaceForUser(string username)
        {
            var character = _characterRepository.GetCharacterForUser(username);
            return character == null ? null : _faceRepository.GetImageForFaceId(character.FaceId);
        }

        public string? GetCharacterBodyForUser(string username)
        {
            var character = _characterRepository.GetCharacterForUser(username);
            return character == null ? null : _characterBodyRepository.GetCharacterBodyImage(character.Color);
        }

        public Task UpdateCharacterFace(string username, string faceId)
        {
            if (_faceRepository.GetImageForFaceId(faceId) == null) { return Task.CompletedTask; }
            _characterRepository.UpdateFace(username, faceId);
            return Task.CompletedTask;
        }

        public Task UpdateCharacterColor(string username, string color)
        {
            if (_characterBodyRepository.GetCharacterBodyImage(color) == null) { return Task.CompletedTask; }
            _characterRepository.UpdateColor(username, color);
            return Task.CompletedTask;
        }
    }
}
