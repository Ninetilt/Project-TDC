using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;

namespace TDC.Backend.DataRepository
{
    public class ListItemRepository: IListItemRepository
    {
        public void AddItemToList(long listId, ToDoListItemDbo item)
        {
            throw new NotImplementedException();
        }
        public void RemoveItemFromList(long itemId)
        {
            throw new NotImplementedException();
        }
        public void UpdateItemDescription(long itemId, string description)
        {
            throw new NotImplementedException();
        }
        public void UpdateItemEffort(long itemId, uint effort)
        {
            throw new NotImplementedException();
        }
        public void SetItemStatus(long itemId, long userId, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
