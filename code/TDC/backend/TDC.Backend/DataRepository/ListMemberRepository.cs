using DataRepository;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListMemberRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[ListMembers]"), IListMemberRepository
    {
        public void AddListMember(long listId, string userId, bool isCreator)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (ListId, Username, IsCreator)"
                      + $"VALUES("
                      + $"  @listId, @username, @isCreator);";
            var parameter = new
            {
                listId = listId,
                username = userId,
                isCreator = isCreator
            };

            this.Insert<ListMemberDbo>(sql, parameter);
        }

        public void RemoveListMember(long listId, string userId)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + $"WHERE ListId = @listId AND Username = @username;";

            var parameter = new
            {
                listId = listId,
                username = userId
            };

            this.Execute<ListMemberDbo>(sql, parameter);
        }

        public bool UserIsCreator(long listId, string username)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE ListId = @listId AND Username = @username;";
            var parameters = new
            {
                listId = listId,
                username = username
            };

            var result = this.Query<ListMemberDbo>(sql, parameters);
            return !result.IsNullOrEmpty() && result.First().IsCreator;
        }

        public List<string> GetListMembers(long listId)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE ListId = @listId;";
            var parameters = new
            {
                listId,
            };

            return this.Query<ListMemberDbo>(sql, parameters).Select(m => m.Username)
                       .ToList();
        }

        public List<long> GetListsForUser(string userId)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE Username = @userId";
            var parameters = new
            {
                userId,
            };

            return this.Query<ListMemberDbo>(sql, parameters).Select(m => m.ListId).ToList();
        }
    }
}
