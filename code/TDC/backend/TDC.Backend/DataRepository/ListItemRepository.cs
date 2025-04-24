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
        public void SetItemStatus(long itemId, long userId, bool status)
        {
            UpdateItemStatusInFile(itemId, userId, status);
        }

        public bool GetItemStatus(long itemId, long userId)
        {
            throw new NotImplementedException();
        }

    #region privates

        private void UpdateItemStatusInFile(long itemId, long userId, bool status)
        {

        }

        private void AddItemStatus()
        {
            //NOTE: always has to be called when new item is added -> for each user who is part of the list
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
            return new ToDoItemStatusDbo(long.Parse(elements[0]), long.Parse(elements[1]), bool.Parse(elements[2]));
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
                writer.WriteLine(HelperToCsvLine(item));
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

        private static string HelperToCsvLine(ListItemCsvHelper helper)
        {
            return helper.ItemId + ";" + helper.ListId + ";" + helper.Description + ";" + helper.Effort;
        }
        #endregion
    }
}
