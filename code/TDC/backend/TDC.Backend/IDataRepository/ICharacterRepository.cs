using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface ICharacterRepository
    {
        public void AddCharacter(CharacterDbo character);
        public CharacterDbo GetCharacterForUser(string username);
        public void UpdateFace(string username, long faceId);
        public void UpdateColor(string username, string color);
    }
}
