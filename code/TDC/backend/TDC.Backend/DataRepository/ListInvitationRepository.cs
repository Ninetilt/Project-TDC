using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListInvitationRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[ListInvitations]"), IListInvitationRepository
    {
        public void AddListInvitation(string forUser, string fromUser, long listId)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (ForUser, FromUser, ListId)"
                      + $"VALUES("
                      + $"  @forUser, @fromUser, @listId);";

            var parameter = new
            {
                forUser = forUser,
                fromUser = fromUser,
                listId = listId
            };

            this.Insert<ListInvitationDbo>(sql, parameter);
        }

        public void DeleteListInvitation(string forUser, string fromUser, long listId)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + "WHERE ForUser = @forUser AND FromUser = @fromUser AND ListId = @listId";

            var parameter = new
            {
                forUser = forUser,
                fromUser = fromUser,
                listId = listId
            };

            this.Execute<ListInvitationDbo>(sql, parameter);
        }

        public List<ListInvitationDbo> GetInvitationsForUser(string username)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE ForUser = @username;";
            var parameters = new
            {
                username,
            };

            return this.Query<ListInvitationDbo>(sql, parameters);
        }
    }
}
