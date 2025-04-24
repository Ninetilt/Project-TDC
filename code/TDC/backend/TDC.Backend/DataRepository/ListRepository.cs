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
            AddListToFile(list);
            return list.ListId; //TO:DO: with database -> return new id that sql creates
        }

        public ToDoListDbo? GetById(long listId, long userId)
        {
            return LoadListById(listId, userId);
            //TO-DO: should not return nullable later, will throw sql exception in error case
        }

        public void UpdateListTitle(long listId, string name)
        {
            UpdateListTitleInFile(listId, name);
        }
        public void DeleteList(long listId)
        {
            DeleteListInFile(listId);
        }
        public void FinishList(long listId, long userId)
        {
            SetIsFinishedInFile(listId, userId);
        }

        #region privates

        private void SetIsFinishedInFile(long listId, long userId)
        {
            var lists = GetAllLists();
            foreach (var list in lists.Where(list => list.ListId == listId && list.UserId == userId))
            {
                list.IsFinished = true;
            }
            SaveAllLists(lists);
        }

        private void DeleteListInFile(long listId)
        {
            var originalLists = GetAllLists();
            var newLists = originalLists.Where(list => list.ListId != listId).ToList();
            SaveAllLists(newLists);
        }

        private void UpdateListTitleInFile(long listId, string newName)
        {
            var lists = GetAllLists();
            foreach (var list in lists.Where(list => list.ListId == listId))
            {
                list.Name = newName;
            }
            SaveAllLists(lists);
        }

        private void AddListToFile(ToDoListDbo list)
        {
            var lists = GetAllLists();
            lists.Add(list);
            SaveAllLists(lists.ToList());
        }

        private void SaveAllLists(List<ToDoListDbo> lists)
        {
            using var writer = new StreamWriter(filePath, false);
            foreach (var list in lists)
            {
                writer.WriteLine(DboToCsvFile(list));
            }
        }

        private static string DboToCsvFile(ToDoListDbo list)
        {
            return $"{list.ListId};{list.UserId};{list.Name};{list.IsCollaborative};{list.IsFinished}";
        }

        private ToDoListDbo? LoadListById(long listId, long userId)
        {
            return GetAllLists().FirstOrDefault(list => list.ListId == listId && list.UserId == userId);
        }

        private List<ToDoListDbo> GetAllLists()
        {
            using var reader = new StreamReader(filePath);
            var ret = new List<ToDoListDbo>();
            while (reader.ReadLine() is { } line)
            {
                ret.Add(ParseToDbo(line));
            }
            return ret;
        }

        private static ToDoListDbo ParseToDbo(string csvLine)
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
