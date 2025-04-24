using System.Diagnostics.CodeAnalysis;
using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListRepository : IListRepository
    {
        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        public ListRepository()
        {
            filePath = Path.Combine(projectPath, "Data/lists.csv");
        }

        public long CreateList(ToDoListDbo list)
        {
            SaveListToFile(list);
            return list.listId; //TO:DO: with database -> return new id that sql creates
        }

        public ToDoListDbo? GetById(long listId, long userId)
        {
            return LoadListById(listId, userId);
            //TO-DO: should not return nullable later, will throw sql exception in error case
        }

        public void UpdateListTitle(long listId, string title)
        {
            throw new NotImplementedException();
        }
        public void DeleteList(long listId)
        {
            throw new NotImplementedException();
        }
        public void FinishList(long listId, long userId)
        {
            throw new NotImplementedException();
        }
        public long AddItemToList(long listId, string description, uint effort)
        {
            throw new NotImplementedException();
        }

        #region privates
        private void SaveListToFile(ToDoListDbo list)
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine($"{list.listId};{list.userId};{list.name};{list.isCollaborative};{list.isFinished}");
        }

        private ToDoListDbo? LoadListById(long listId, long userId)
        {
            return GetAllLists().FirstOrDefault(list => list.listId == listId && list.userId == userId);
        }

        private List<ToDoListDbo> GetAllLists()
        {
            using var reader = new StreamReader(filePath);
            var ret = new List<ToDoListDbo>();
            while (reader.ReadLine() is { } line)
            {
                ret.Add(parseToDbo(line));
            }
            return ret;
        }

        private ToDoListDbo parseToDbo(string csvLine)
        {
            var elements = csvLine.Split(';');
            return new ToDoListDbo(long.Parse(elements[0]),
                                   long.Parse(elements[1]),
                                   elements[2],
                                   bool.Parse(elements[3]),
                                   bool.Parse(elements[4]));
        }
        #endregion
    }
}
