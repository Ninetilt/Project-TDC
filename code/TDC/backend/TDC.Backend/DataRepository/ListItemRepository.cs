using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListItemRepository: IListItemRepository
    {
        private readonly string projectPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\.."));
        private readonly string filePath;
        private readonly string statusFilePath;

        public ListItemRepository()
        {
            filePath = Path.Combine(projectPath, "Data/items.csv");
            statusFilePath = Path.Combine(projectPath, "Data/item-status.csv");
        }

        public List<ToDoListItemDbo> GetItemsForList(long listId)
        {
            return GetItemsForListFromFile(listId);
        }

        public long AddItemToList(long listId, ToDoListItemDbo item)
        {
            item.ItemId = GetNewId();
            AddItemToFile(new ListItemCsvHelper(item.ItemId, listId, item.Description, item.Effort));
            AddDefaultItemStatus(item.ItemId, listId);
            return item.ItemId;
        }
        public void RemoveItemFromList(long itemId)
        {
            RemoveItemFromFile(itemId);
        }
        public void UpdateItemDescription(long itemId, string description)
        {
            UpdateItemDescriptionInFile(itemId, description);
        }
        public void UpdateItemEffort(long itemId, uint effort)
        {
            UpdateItemEffortInFile(itemId, effort);
        }
        public void SetItemStatus(long itemId, string userId, bool status)
        {
            UpdateItemStatusInFile(itemId, userId, status);
        }

        public bool GetItemStatus(long itemId, string userId)
        {
            return GetItemStatusFromFile(itemId, userId);
        }

        public void AddItemStatusForNewMember(long listId, string userId)
        {
            AddItemStatusForNewMemberInFile(listId, userId);
        }

        #region privates

        private void AddItemStatusForNewMemberInFile(long listId, string userId)
        {
            var items = GetItemsForList(listId);
            var existingStatusItems = GetAllStatusItems();
            existingStatusItems.AddRange(items.Select(item => new ToDoItemStatusDbo(item.ItemId, userId, false)));
            SaveAllStatusItems(existingStatusItems);
        }

        private bool GetItemStatusFromFile(long itemId, string userId)
        {
            var items = GetAllStatusItems();
            return (from item in items where item.ItemId == itemId && item.UserId.Equals(userId) select item.IsDone).FirstOrDefault();
        }

        private void UpdateItemStatusInFile(long itemId, string userId, bool status)
        {
            var items = GetAllStatusItems();
            foreach (var item in items.Where(item => item.ItemId == itemId && item.UserId.Equals(userId)))
            {
                item.IsDone = status;
            }
            SaveAllStatusItems(items);
        }

        private void AddDefaultItemStatus(long itemId, long listId)
        {
            //NOTE: always has to be called when new item is added -> for each user who is part of the list
            var statusItems = GetAllStatusItems();
            var listMemberRepos = new ListMemberRepository();
            var members = listMemberRepos.GetListMembers(listId);
            statusItems.AddRange(members.Select(member => new ToDoItemStatusDbo(itemId, member, false)));
            SaveAllStatusItems(statusItems);
        }

        private void SaveAllStatusItems(List<ToDoItemStatusDbo> statusItems)
        {
            var writer = new StreamWriter(statusFilePath);
            foreach (var item in statusItems)
            {
                writer.WriteLine(StatusDboToCsvString(item));
            }
        }

        private List<ToDoItemStatusDbo> GetAllStatusItems()
        {
            var reader = new StreamReader(statusFilePath);
            var ret = new List<ToDoItemStatusDbo>();
            while (reader.ReadLine() is { } line)
            {
                ret.Add(ParseToStatusDbo(line));
            }
            return ret;
        }

        private static ToDoItemStatusDbo ParseToStatusDbo(string line)
        {
            var elements = line.Split(';');
            return new ToDoItemStatusDbo(long.Parse(elements[0]), elements[1], bool.Parse(elements[2]));
        }

        private static string StatusDboToCsvString(ToDoItemStatusDbo dbo)
        {
            return dbo.ItemId + ";" + dbo.UserId + ";" + dbo.IsDone;
        }

        private void UpdateItemEffortInFile(long itemId, uint effort)
        {
            var items = GetAllItemHelpersFromFile();
            foreach (var item in items.Where(item => item.ItemId == itemId))
            {
                item.Effort = effort;
            }
            SaveAllItems(items);
        }

        private void UpdateItemDescriptionInFile(long itemId, string description)
        {
            var items = GetAllItemHelpersFromFile();
            foreach (var item in items.Where(item => item.ItemId == itemId))
            {
                item.Description = description;
            }
            SaveAllItems(items);
        }

        private void RemoveItemFromFile(long itemId)
        {
            var items = GetAllItemHelpersFromFile();
            var newItems = items.Where(item => item.ItemId != itemId).ToList();
            SaveAllItems(newItems);
        }

        private void AddItemToFile(ListItemCsvHelper item)
        {
            var items = GetAllItemHelpersFromFile();
            items.Add(item);
            SaveAllItems(items);
        }

        private long GetNewId()
        {
            //TO-DO: remove once sql is used 
            var existingItems = GetAllItemHelpersFromFile();
            var max = existingItems.Select(item => item.ItemId).Prepend(-1).Max();
            return max + 1;
        }

        private void SaveAllItems(List<ListItemCsvHelper> items)
        {
            using var writer = new StreamWriter(filePath);
            foreach (var item in items)
            {
                writer.WriteLine(ParseToCsvLine(item));
            }
        }

        private List<ToDoListItemDbo> GetItemsForListFromFile(long listId)
        {
            var helpers = GetAllItemHelpersFromFile();
            return (from item in helpers where item.ListId == listId select new ToDoListItemDbo(item.ItemId, item.Description, item.Effort)).ToList();
        }

        private List<ListItemCsvHelper> GetAllItemHelpersFromFile()
        {
            using var reader = new StreamReader(filePath);
            var ret = new List<ListItemCsvHelper>();
            while (reader.ReadLine() is { } line)
            {
                ret.Add(ParseToHelper(line));
            }
            return ret;
        }

        private static ListItemCsvHelper ParseToHelper(string csvLine)
        {
            var elements = csvLine.Split(";");
            return new ListItemCsvHelper(long.Parse(elements[0]), long.Parse(elements[1]), elements[2], uint.Parse(elements[3]));
        }

        private static string ParseToCsvLine(ListItemCsvHelper helper)
        {
            return helper.ItemId + ";" + helper.ListId + ";" + helper.Description + ";" + helper.Effort;
        }
        #endregion
    }
}
