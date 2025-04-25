using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class ToDoListHandler : IToDoListHandler
    {
        public long AddItemToList(long listId, string itemDescription, uint itemEffort)
        {
            throw new NotImplementedException();
        }

        public void AddUserToList(long listId, string username)
        {
            throw new NotImplementedException();
        }

        public long CreateList(ToDoListDto newList)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(long itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(long listId, long userId)
        {
            throw new NotImplementedException();
        }

        public void FinishList(long listId, long userId)
        {
            throw new NotImplementedException();
        }

        public List<ToDoListDto> GetListsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromList(long listId, string username)
        {
            throw new NotImplementedException();
        }

        public void SetItemStatus(long itemId, string updateForUser, bool isDone)
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

        public void UpdateListTitle(long listId, string newTitle)
        {
            throw new NotImplementedException();
        }
    }
}
