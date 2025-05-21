using DataRepository;
using System.Drawing;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListRewardingRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[Rewarding]"), IListRewardingRepository
    {
        public void AddNewRewarding(long listId, string message)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (ListId, RewardingMessage)"
                      + $"VALUES("
                      + $"  @listId, @message);";

            var parameter = new
            {
                listId = listId,
                message = message
            };

            this.Insert<RewardingDbo>(sql, parameter);
        }

        public string? GetRewardingById(long listId)
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE ListId = @listId";
            var parameters = new
            {
                listId,
            };

            var result = this.Query<RewardingDbo>(sql, parameters).FirstOrDefault();
            return result?.RewardingMessage;
        }
    }
}
