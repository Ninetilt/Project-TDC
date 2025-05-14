using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class FriendRequestRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Requests]"), IFriendRequestRepository
    {
        public void DeleteFriendRequest(string username, string requestName)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + "WHERE Username = @username AND Request = @request";

            var parameter = new
            {
                username = username,
                request = requestName
            };

            this.Execute<RequestDbo>(sql, parameter);
        }

        public List<string> GetRequestsForUser(string username)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                     + $"WHERE Username = @username;";
            var parameters = new
            {
                username,
            };

            return this.Query<RequestDbo>(sql, parameters).Select(m => m.Request)
                       .ToList();
        }

        public void AddFriendRequest(string senderName, string receiverName)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (Username, Request)"
                      + $"VALUES("
                      + $"  @username, @request);";

            var parameter = new
            {
                username = senderName,
                request = receiverName
            };

            this.Insert<RequestDbo>(sql, parameter);
        }
    }
}
