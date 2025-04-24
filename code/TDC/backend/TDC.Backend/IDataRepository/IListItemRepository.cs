using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.IDataRepository
{
    public interface IListItemRepository
    {
        public void AddItemToList(long listId, ToDoListItemDbo item);
        public void RemoveItemFromList(long itemId);
        public void UpdateItemDescription(long itemId, string description);
        public void UpdateItemEffort(long itemId, uint effort);
        public void SetItemStatus(long itemId, long userId, bool status);
    }
}
