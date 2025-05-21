using DataRepository;
using System.Collections.Generic;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class OpenRewardsRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[OpenRewards]"), IOpenRewardsRepository
    {
        public void AddOpenRewardForUser(string username, long listId)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (ListId, Username)"
                      + $"VALUES("
                      + $"  @listId, @username);";

            var parameter = new
            {
                username = username,
                listId = listId
            };

            this.Insert<OpenRewardDbo>(sql, parameter);
        }

        public List<long> GetOpenRewardsForUser(string username)
        {
            var sql = $"SELECT * FROM {this.TableName} "
                      + $"WHERE Username = @username";
            var parameters = new
            {
                username,
            };

            var result = this.Query<OpenRewardDbo>(sql, parameters);
            return result.Select(reward => reward.ListId).ToList();
        }

        public void RemoveSeenReward(string username, long listId)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + "WHERE Username = @username AND ListId = @listId";

            var parameter = new
            {
                username = username,
                listId = listId
            };

            this.Execute<OpenRewardDbo>(sql, parameter);
        }
    }
}
