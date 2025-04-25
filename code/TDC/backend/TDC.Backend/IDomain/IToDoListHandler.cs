using TDC.Backend.IDomain.Models;

namespace TDC.Backend.IDomain
{
    public interface IToDoListHandler
    {
        public long CreateList(string creator, ToDoListDto newList);
        public void UpdateListTitle(long listId, string newTitle);
        public void DeleteList(long listId, string sender);
        public void FinishList(long listId, string sender);
        public void AddUserToList(long listId, string username);
        public void RemoveUserFromList(long listId, string username);
        public bool SendListInvitation(long listId, string fromUser, string ForUser);
        public void DeclineListInvitation(long listId, string decliningUser);
        public void AcceptListInvitation(long listId, string newUser);
        public List<ListInvitationDto> LoadListInvitationsForUser(string username);
        public List<ToDoListDto> GetListsForUser(string username);
        public long AddItemToList(long listId, string itemDescription, uint itemEffort);
        public void DeleteItem(long itemId);
        public void UpdateItemDescription(long itemId, string description);
        public void UpdateItemEffort(long itemId, uint effort);
        public void SetItemStatus(long itemId, string updateForUser, bool isDone);
    }
}
