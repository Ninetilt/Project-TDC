using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class FaceRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Face]"), IFaceRepository
    {
        public string? GetImageForFaceId(long faceId)
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE Id = @faceId";
            var parameters = new
            {
                faceId,
            };

            var result = this.Query<FaceDbo>(sql, parameters).FirstOrDefault();
            return result?.Image;
        }
    }
}
