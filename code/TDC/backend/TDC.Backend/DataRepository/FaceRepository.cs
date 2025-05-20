using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;

namespace TDC.Backend.DataRepository
{
    public class FaceRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Face]"), IFaceRepository
    {
        public string GetImageForFaceId(long faceId)
        {
            throw new NotImplementedException();
        }
    }
}
