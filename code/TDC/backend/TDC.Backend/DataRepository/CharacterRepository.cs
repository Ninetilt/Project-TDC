using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class CharacterRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Character]"), ICharacterRepository
    {
        public void AddCharacter(CharacterDbo character)
        {
            throw new NotImplementedException();
        }
        public CharacterDbo GetCharacterForUser(string username)
        {
            throw new NotImplementedException();
        }
        public void UpdateFace(string username, long faceId)
        {
            throw new NotImplementedException();
        }
        public void UpdateColor(string username, string color)
        {
            throw new NotImplementedException();
        }
    }
}
