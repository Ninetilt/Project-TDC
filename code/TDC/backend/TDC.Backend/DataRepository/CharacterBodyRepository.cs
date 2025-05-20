using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.DataRepository
{
    public class CharacterBodyRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[CharacterBody]"), ICharacterBodyRepository
    {
        public string GetCharacterBodyImage(string color)
        {
            throw new NotImplementedException();
        }
    }
}
