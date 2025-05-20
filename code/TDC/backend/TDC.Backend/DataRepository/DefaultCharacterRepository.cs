using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class DefaultCharacterRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[DefaultCharacter]"), IDefaultCharacterRepository
    {
        public string GetDefaultCharacterImage()
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE Id = @id";
            var parameters = new
            {
                id = "default"
            };

            var result = this.Query<DefaultCharacterDbo>(sql, parameters).FirstOrDefault();
            return result!.Image;
        }
    }
}
