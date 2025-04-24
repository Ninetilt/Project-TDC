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
            list.ListId = GetNewId(); // TO-DO: remove once sql is used
            AddListToFile(list);
            return list.ListId; //TO:DO: with database -> return new id that sql creates
        }

        public ToDoListDbo? GetById(long listId)
        {
            return LoadListById(listId);
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

        public void FinishList(long listId)
        {
            SetIsFinishedInFile(listId);
        }

        #region privates
        private long GetNewId()
        {
            //TO-DO: remove once sql is used 
            var existingItems = GetAllLists();
            var max = existingItems.Select(list => list.ListId).Prepend(-1).Max();
            return max + 1;
        }

        private void SetIsFinishedInFile(long listId)
        {
            var lists = GetAllLists();
            foreach (var list in lists.Where(list => list.ListId == listId))
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
                writer.WriteLine(ParseToCsvLine(list));
            }
        }

        private static string ParseToCsvLine(ToDoListDbo list)
        {
            return $"{list.ListId};{list.Name};{list.IsCollaborative};{list.IsFinished}";
        }

        private ToDoListDbo? LoadListById(long listId)
        {
            return GetAllLists().FirstOrDefault(list => list.ListId == listId);
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
                                   elements[1],
                                   bool.Parse(elements[2]),
                                   bool.Parse(elements[3]));
        }
        #endregion
    }
}
