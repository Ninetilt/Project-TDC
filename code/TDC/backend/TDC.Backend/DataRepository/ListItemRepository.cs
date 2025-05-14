using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using DataRepository;
using TDC.Backend.DataRepository.Helper;

namespace TDC.Backend.DataRepository
{
    public class ListItemRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[ListItem]"), IListItemRepository
    {
        public List<ToDoListItemDbo> GetItemsForList(long listId)
        {
            var sql = $"SELECT * FROM dbo.{this.TableName} "
                      + $"WHERE ListId = @listId";
            var parameters = new
            {
                listId,
            };

            return this.Query<ToDoListItemDbo>(sql, parameters);
        }

        public long AddItemToList(ToDoListItemDbo item)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (ListId, Description, Effort)"
                      + $"VALUES("
                      + $"  @listId, @description, @effort);";
            var parameter = new
            {
                listId = item.ListId,
                description = item.Description,
                effort = item.Effort
            };

            return this.Insert<ToDoListItemDbo>(sql, parameter);
        }

        public void DeleteItem(long itemId)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + $"WHERE Id = @itemId;";

            var parameter = new
            {
                itemId = itemId
            };

            this.Execute<ToDoListItemDbo>(sql, parameter);
        }

        public void UpdateItemDescription(long itemId, string description)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Description = @description "
                      + $"WHERE Id = @itemId;";

            var parameter = new
            {
                itemId = itemId,
                description = description
            };

            this.Execute<ToDoListItemDbo>(sql, parameter);
        }

        public void UpdateItemEffort(long itemId, int effort)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Effort = @effort "
                      + $"WHERE Id = @itemId;";

            var parameter = new
            {
                itemId = itemId,
                effort = effort
            };

            this.Execute<ToDoListItemDbo>(sql, parameter);
        }

        public void SetItemStatus(long itemId, string userId, bool status)
        {
            const string sql = 
                $"IF EXISTS (SELECT 1 FROM dbo.ItemStatus WHERE ItemId = @itemId AND Username = @username)"
                + $"UPDATE dbo.ItemStatus SET IsDone = @status WHERE ItemId = @itemId AND Username = @username;"
                + $"ELSE "
                + $"INSERT INTO dbo.ItemStatus (ItemId, Username, IsDone) VALUES (@itemId, @username, @status);";

            var parameter = new
            {
                itemId = itemId,
                username = userId,
                status = status
            };

            this.Execute<ToDoItemStatusDbo>(sql, parameter);
        }

        public bool GetItemStatus(long itemId, string userId)
        {
            const string sql = $"SELECT * FROM dbo.ItemStatus " +
                               $"WHERE itemId = @itemId AND Username = @username";

            var parameters = new
            {
                itemId = itemId,
                username = userId
            };

            var result = this.Query<ToDoItemStatusDbo>(sql, parameters).FirstOrDefault();

            return result?.IsDone ?? false;
        }

        public long GetListIdFromItem(long itemId)
        {
            return this.GetById<ToDoListItemDbo>(itemId).ListId;
        }

    }
}
