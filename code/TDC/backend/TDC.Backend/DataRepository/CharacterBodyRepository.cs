using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class CharacterBodyRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[CharacterBody]"), ICharacterBodyRepository
    {
        public string? GetCharacterBodyImage(string color)
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE Color = @color";
            var parameters = new
            {
                color,
            };

            var result = this.Query<CharacterBodyDbo>(sql, parameters).FirstOrDefault();
            return result?.Image;
        }
    }
}
