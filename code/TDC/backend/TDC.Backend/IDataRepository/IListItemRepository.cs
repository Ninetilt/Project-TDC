using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IListItemRepository
    {
        public List<ToDoListItemDbo> GetItemsForList(long listId);
        public long AddItemToList(ToDoListItemDbo item);
        public void DeleteItem(long itemId);
        public void UpdateItemDescription(long itemId, string description);
        public void UpdateItemEffort(long itemId, int effort);
        public void SetItemStatus(long itemId, string userId, bool status);
        public bool GetItemStatus(long itemId, string userId);
        public long GetListIdFromItem(long itemId);
    }
}
