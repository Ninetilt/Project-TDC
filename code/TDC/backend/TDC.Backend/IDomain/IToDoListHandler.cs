using TDC.Backend.IDomain.Models;

namespace TDC.Backend.IDomain
{
    public interface IToDoListHandler
    {
        public long CreateList(string creator, ToDoListSavingDto newListLoading);
        public Task UpdateListTitle(long listId, string newTitle);
        public Task DeleteList(long listId, string sender);
        public Task FinishList(long listId, string sender);
        public Task AddUserToList(long listId, string username);
        public Task RemoveUserFromList(long listId, string username);
        public Task SendListInvitation(long listId, string fromUser, string ForUser);
        public Task CancelListInvitation(long listId, string fromUser, string forUser);
        public Task DenyListInvitation(long listId, string decliningUser);
        public Task AcceptListInvitation(long listId, string newUser);
        public List<ListInvitationDto> LoadListInvitationsForUser(string username);
        public List<ToDoListLoadingDto> GetListsForUser(string username);
        public long AddItemToList(long listId, string itemDescription, int itemEffort);
        public Task DeleteItem(long itemId);
        public Task UpdateItemDescription(long itemId, string description);
        public Task UpdateItemEffort(long itemId, int effort);
        public Task SetItemStatus(long itemId, string updateForUser, bool isDone);
        public List<ToDoListItemLoadingDto> GetItemsForList(long listId, string username);
        public ToDoListLoadingDto? GetListById(long listId);
        public RewardingMessageDto GetRewardingById(string username, long listId);
        public List<RewardingMessageDto> GetOpenRewardsForUser(string username);
        public void RemoveSeenRewardingForUser(string username, long listId);
        
    }
}
