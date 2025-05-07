using DataRepository;
using TDC.Backend.DataRepository.Helper;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListRepository(ConnectionFactory connectionFactory) : BaseRepository(connectionFactory, "[ToDoList]"), IListRepository
    {
        public long CreateList(ToDoListDbo list)
        {
            var sql = $"INSERT INTO dbo.{this.TableName} "
                      + $"  (Name, IsCollaborative, IsFinished)"
                      + $"VALUES("
                      + $"  @name, @isCollaborative, @isFinished);";
            var parameter = new
            {
                name = list.Name,
                isCollaborative = list.IsCollaborative,
                descriptisFinishedion = list.IsCollaborative
            };

            return this.Insert<ToDoListDbo>(sql, parameter);
        }

        public ToDoListDbo? GetById(long listId)
        {
            return this.GetById<ToDoListDbo>(listId);
        }

        public void UpdateListTitle(long listId, string name)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET Name = @name "
                      + $"WHERE Id = @listId;";

            var parameter = new
            {
                listId = listId,
                name = name
            };

            this.Execute<ToDoListDbo>(sql, parameter);
        }

        public void DeleteList(long listId)
        {
            var sql = $"DELETE FROM dbo.{this.TableName} "
                      + $"WHERE Id = @listId;";

            var parameter = new
            {
                listId = listId
            };

            this.Execute<ToDoListDbo>(sql, parameter);
        }

        public void FinishList(long listId)
        {
            var sql = $"UPDATE dbo.{this.TableName} "
                      + $"SET IsFinished = @IsFinished "
                      + $"WHERE Id = @listId;";

            var parameter = new
            {
                listId = listId,
                isFinished = true
            };

            this.Execute<ToDoListDbo>(sql, parameter);
        }
    }
}
