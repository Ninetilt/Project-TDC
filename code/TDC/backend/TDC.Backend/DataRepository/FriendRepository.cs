using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class FriendRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Friends]"), IFriendRepository
    {
        public void AddFriend(string username, string friendName)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (Username, Friend)"
                      + $"VALUES("
                      + $"  @username, @friend);";

            var parameter = new
            {
                username = username,
                friend = friendName
            };

            this.Insert<FriendDbo>(sql, parameter);
        }

        public List<string> GetFriendsForUser(string username)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE Username = @username;";
            var parameters = new
            {
                username,
            };

            return this.Query<FriendDbo>(sql, parameters).Select(m => m.Friend)
                       .ToList();
        }

        public void RemoveFriend(string username, string friendName)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + "WHERE Username = @username AND Friend = @friend";

            var parameter = new
            {
                username = username,
                friend = friendName
            };

            this.Execute<FriendDbo>(sql, parameter);
        }
    }
}
