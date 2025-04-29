﻿using TDC.Backend.IDomain.Models;

namespace TDC.Backend.IDomain
{
    public interface IToDoListHandler
    {
        public Task CreateList(string creator, ToDoListSavingDto newListLoading);
        public Task UpdateListTitle(long listId, string newTitle);
        public Task DeleteList(long listId, string sender);
        public Task FinishList(long listId, string sender);
        public Task AddUserToList(long listId, string username);
        public Task RemoveUserFromList(long listId, string username);
        public Task SendListInvitation(long listId, string fromUser, string ForUser);
        public Task DeclineListInvitation(long listId, string decliningUser);
        public Task AcceptListInvitation(long listId, string newUser);
        public List<ListInvitationDto> LoadListInvitationsForUser(string username);
        public List<ToDoListLoadingDto> GetListsForUser(string username);
        public Task AddItemToList(long listId, string itemDescription, uint itemEffort);
        public Task DeleteItem(long itemId);
        public Task UpdateItemDescription(long itemId, string description);
        public Task UpdateItemEffort(long itemId, uint effort);
        public Task SetItemStatus(long itemId, string updateForUser, bool isDone);
    }
}
