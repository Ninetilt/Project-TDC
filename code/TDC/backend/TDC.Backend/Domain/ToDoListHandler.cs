using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class ToDoListHandler(
        IListRepository listRepository,
        IListItemRepository listItemRepository,
        IListMemberRepository listMemberRepository)
        : IToDoListHandler
    {
        public readonly IListRepository _listRepository = listRepository;
        public readonly IListItemRepository _listItemRepository = listItemRepository;
        public readonly IListMemberRepository _listMemberRepository = listMemberRepository;

        public Task CreateList(string creator, ToDoListSavingDto newList)
        {
            var listDbo = new ToDoListDbo(0, newList.Name, newList.IsCollaborative, false);
            var listId = _listRepository.CreateList(listDbo);
            _listMemberRepository.AddListMember(listId, creator, true);
            return Task.CompletedTask;
        }

        public Task AddUserToList(long listId, string username)
        {
            if(!ListExists(listId)) { return Task.CompletedTask;}
            if (UserIsListMember(listId, username)) { return Task.CompletedTask; }
            if (!ListIsCollaborative(listId)) { return Task.CompletedTask; }
            _listMemberRepository.AddListMember(listId, username, false);
            return Task.CompletedTask;
        }

        public Task RemoveUserFromList(long listId, string username)
        {
            if (!UserIsListMember(listId,username)) { return Task.CompletedTask; }
            if (_listMemberRepository.UserIsCreator(listId, username)) { return Task.CompletedTask; }
            _listMemberRepository.RemoveListMember(listId, username);
            return Task.CompletedTask;
        }

        public Task SendListInvitation(long listId, string fromUser, string ForUser)
        {
            throw new NotImplementedException();
        }

        public Task DeclineListInvitation(long listId, string decliningUser)
        {
            throw new NotImplementedException();
        }

        public Task AcceptListInvitation(long listId, string newUser)
        {
            throw new NotImplementedException();
        }

        public List<ListInvitationDto> LoadListInvitationsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task DeleteList(long listId, string sender)
        {
            if (!UserIsCreator(listId, sender)) { return Task.CompletedTask; }
            _listRepository.DeleteList(listId);
            return Task.CompletedTask;
        }

        public Task FinishList(long listId, string sender)
        {
            if (!UserIsCreator(listId, sender)) { return Task.CompletedTask; }
            if (!ListCanBeFinished(listId)) { return Task.CompletedTask; }

            // TO-DO: add logic to grant every member rewards
            _listRepository.FinishList(listId);
            return Task.CompletedTask;
        }

        public List<ToDoListLoadingDto> GetListsForUser(string username)
        {
            var listIds = _listMemberRepository.GetListsForUser(username);
            var listDboList = listIds.Select(listId => _listRepository.GetById(listId)).OfType<ToDoListDbo>().ToList();

            var listDtoList = new List<ToDoListLoadingDto>();
            foreach (var listDbo in listDboList) {
                var itemDboList = _listItemRepository.GetItemsForList(listDbo.ListId);
                var listMembers = _listMemberRepository.GetListMembers(listDbo.ListId);
                var itemDtoList = itemDboList.Select(itemDbo => ParseItemDboToDto(itemDbo, username, listMembers)).ToList();

                listDtoList.Add(new ToDoListLoadingDto(listDbo.ListId, listDbo.Name, itemDtoList, listMembers, listDbo.IsCollaborative));
            }
            return listDtoList;
        }

        public Task AddItemToList(long listId, string itemDescription, uint itemEffort)
        {
            if(!ListExists(listId)) {return Task.CompletedTask;}
            _listItemRepository.AddItemToList(new ToDoListItemDbo(0, listId, itemDescription, itemEffort));
            return Task.CompletedTask;
        }

        public Task DeleteItem(long itemId)
        {
            _listItemRepository.DeleteItem(itemId);

            var listId = _listItemRepository.GetListIdFromItem(itemId);
            var listMembers = _listMemberRepository.GetListMembers(listId);
            return Task.CompletedTask;
        }

        public Task UpdateItemDescription(long itemId, string description)
        {
            _listItemRepository.UpdateItemDescription(itemId, description);
            return Task.CompletedTask;
        }

        public Task UpdateItemEffort(long itemId, uint effort)
        {
            _listItemRepository.UpdateItemEffort(itemId, effort);
            return Task.CompletedTask;
        }

        public Task UpdateListTitle(long listId, string newTitle)
        {
            _listRepository.UpdateListTitle(listId, newTitle);
            return Task.CompletedTask;
        }

        public Task SetItemStatus(long itemId, string updateForUser, bool isDone)
        {
            _listItemRepository.SetItemStatus(itemId, updateForUser, isDone);
            return Task.CompletedTask;
        }

        #region privates
        private ToDoListItemLoadingDto ParseItemDboToDto(ToDoListItemDbo dbo, string currentUser, List<string> listMembers)
        {
            var isDone = _listItemRepository.GetItemStatus(dbo.ItemId, currentUser);
            var finishedMembers = listMembers
                .Where(member => _listItemRepository.GetItemStatus(dbo.ItemId, member) && !member.Equals(currentUser))
                .ToList();
            return new ToDoListItemLoadingDto(dbo.ItemId, dbo.Description, isDone, finishedMembers, dbo.Effort);
        }

        private bool ListCanBeFinished(long listId)
        {
            var listItems = _listItemRepository.GetItemsForList(listId);
            return listItems.All(listItem => AnyoneHasFinished(listId, listItem.ItemId));
        }

        private bool AnyoneHasFinished(long listId, long itemId)
        {
            var listMembers = _listMemberRepository.GetListMembers(listId);
            return listMembers.Any(member => _listItemRepository.GetItemStatus(itemId, member) == true);
        }

        private bool UserIsListMember(long listId, string username) {
            var members = _listMemberRepository.GetListMembers(listId);
            return members.Any(member => member.Equals(username));
        }

        private bool ListIsCollaborative(long listId) {
            var list = _listRepository.GetById(listId)!;
            return list.IsCollaborative;
        }

        private bool UserIsCreator(long listId, string username)
        {
            return _listMemberRepository.UserIsCreator(listId, username);
        }

        private bool ListExists(long listId)
        {
            return _listRepository.GetById(listId) != null;
        }
        #endregion
    }
}
