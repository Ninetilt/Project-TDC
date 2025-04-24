using System.Collections.Generic;
using TDC.IRepository;
using TDC.Models;

namespace TDC.Repositories
{
    public class ListRepository : IListRepository
    {
        private List<ToDoList> lists;

        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;

        #region constructors
        public ListRepository()
        {
            filePath = Path.Combine(projectPath, "Data/lists.csv");

            #if ANDROID
            var directoryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // create directory if not doesn't exist already
            }

            filePath = Path.Combine(directoryPath, "lists.csv");
            #endif
            lists = new List<ToDoList>();
            LoadAllListsFromFile();
        }
        #endregion

        #region publics
        public string CreateList(ToDoList list)
        {
            lists.Add(list);
            SaveListsToFile();
            return list.ListID; //TO-DO: Once database is implemented, add logic to return new id
        }

        public void UpdateList(ToDoList newList, string listId, long userId)
        {
            for (var i = 0; i < lists.Count; i++)
            {
                if (lists[i].ListID.Equals(listId) && lists[i].UserId.Equals(userId))
                {
                    lists[i] = newList;
                }
            }
            SaveListsToFile();
        }

        public void DeleteList(string listId, long userId)
        {
            var list = lists.FirstOrDefault(list => list.ListID.Equals(listId) && list.UserId.Equals(userId));
            if (list != null)
            {
                lists.Remove(list);
                SaveListsToFile();
            }
        }

        public ToDoList? GetListById(string listId, long userId)
        {
            //To-Do fix for android --> nicht möglich aktuell listen zu öffnen weil nicht die dummy listen returnt und druchsucht werden sondern die der csv und die gibt es in android nd
            return lists.FirstOrDefault(list => list.ListID.Equals(listId) && list.UserId.Equals(userId));
        }

        public List<ToDoList> GetAllListsForUser(long userId)
        {
            #if ANDROID
                return GetDummyLists(userId);
            #else
                LoadAllListsFromFile();
                return lists.FindAll(list => list.UserId.Equals(userId));
            #endif
        }

        #endregion

        #region privates
        //TO-DO: ONLY FOR ANDROID TESTING, REMOVE ONCE DATA BASE WORKS
        private List<ToDoList> GetDummyLists(long userId)
        {
            var listDummy = new List<ToDoList>();
            var list1 = new ToDoList("first list", 1);
            if (list1.UserId == userId)
            {
                list1.AddItem(new ListItem("item 1", true, [], 1));
                list1.AddItem(new ListItem("item 2", false, [], 2));
                list1.AddItem(new ListItem("item 3", true, [], 1));
                list1.AddItem(new ListItem("item 4", false, [], 3));
                list1.AddItem(new ListItem("item 5", true, [], 1));
                list1.AddItem(new ListItem("item 6", false, [], 5));
                listDummy.Add(list1);
            }

            var list2 = new ToDoList("second list", 2);
            if (list2.UserId == userId)
            {
                list2.AddItem(new ListItem("first", false, [], 2));
                list2.AddItem(new ListItem("sec", true, [], 1));
                list2.AddItem(new ListItem("third", true, [], 3));
                listDummy.Add(list2);
            }

            // Corrected: list3 should have a unique UserId
            var list3 = new ToDoList("third list", 3);
            // The following lines were adding items to list2 instead of list3, which seems like a bug.
            // Assuming you intended to add items to list3 if its UserId matches.
            if (list3.UserId == userId)
            {
                list3.AddItem(new ListItem("first", false, [], 2));
                list3.AddItem(new ListItem("sec", true, [], 1));
                list3.AddItem(new ListItem("third", true, [], 3));
                listDummy.Add(list3);
            }

            return listDummy;
        }
        private void SaveListsToFile()
        {
            using var writer = new StreamWriter(filePath);
            writer.WriteLine("ListID;UserID;ListName;ItemDescription;Effort;Done;Members"); // header

            foreach (var todoList in lists)
            {
                foreach (var item in todoList.GetItems())
                {
                    writer.WriteLine($"{todoList.ListID};{todoList.UserId};{todoList.Name};{item.GetDescription()};{item.GetEffort()};{item.IsDone()}");
                }
            }
        }

        private void LoadAllListsFromFile()
        {
            if (!File.Exists(filePath)) return;
            var lines = File.ReadAllLines(filePath).Skip(1);

            var listDict = new Dictionary<string, ToDoList>(); //dict: ID -> List

            foreach (var line in lines)
            {
                var values = line.Split(';');

                var listId = values[0];
                var userId = long.Parse(values[1]);
                var listName = values[2];
                var itemDescription = values[3];
                var itemEffort = int.Parse(values[4]);
                var itemDone = bool.Parse(values[5]);

                if (!listDict.ContainsKey(listId))
                {
                    var todoList = new ToDoList(listName, listId, userId); 
                    listDict[listId] = todoList;
                }

                var todoItem = new ListItem(itemDescription, itemDone, [], itemEffort);
                listDict[listId].AddItem(todoItem);
            }

            lists = listDict.Values.ToList();
        }
        #endregion
    }
}
