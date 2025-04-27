using TDC.Backend.IDataRepository;
using TDC.Backend.IDataRepository.Models;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Domain
{
    public class ToDoListHandler : IToDoListHandler
    {
        internal readonly IListRepository _listRepository;
        internal readonly IListItemRepository _listItemRepository;
        internal readonly IListMemberRepository _listMemberRepository;

        public ToDoListHandler(
            IListRepository listRepository, 
            IListItemRepository listItemRepository, 
            IListMemberRepository listMemberRepository)
        {
            _listRepository = listRepository;
            _listItemRepository = listItemRepository;
            _listMemberRepository = listMemberRepository;
        }

        public Task CreateList(string creator, ToDoListDto newList)
        {
            var listDbo = new ToDoListDbo(newList.ListId, newList.Name, newList.IsCollaborative, false);
            var listId = _listRepository.CreateList(listDbo);
            _listMemberRepository.AddListMember(listId, creator, true);
            return Task.CompletedTask;
        }

        public Task AddUserToList(long listId, string username)
        {
            _listMemberRepository.AddListMember(listId, username, false);
            return Task.CompletedTask;
        }

        public Task RemoveUserFromList(long listId, string username)
        {
            if (_listMemberRepository.UserIsCreator(listId, username)) {
                return Task.CompletedTask;
            }
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
            if(!_listMemberRepository.UserIsCreator(listId, sender)) { return Task.CompletedTask; }
            _listRepository.DeleteList(listId);
            return Task.CompletedTask;
        }

        public Task FinishList(long listId, string sender)
        {
            if(!_listMemberRepository.UserIsCreator(listId, sender)) { return Task.CompletedTask; }
            _listRepository.FinishList(listId);
            return Task.CompletedTask;
        }

        public List<ToDoListDto> GetListsForUser(string username)
        {
            var listIds = _listMemberRepository.GetListsForUser(username);
            var listDbos = new List<ToDoListDbo>();

            foreach (var listId in listIds) {
                listDbos.Add(_listRepository.GetById(listId)!);
            }

            var listDtos = new List<ToDoListDto>();
            foreach (var listDbo in listDbos) {
                var itemDbos = _listItemRepository.GetItemsForList(listDbo.ListId);
                var listMembers = _listMemberRepository.GetListMembers(listDbo.ListId);
                var itemDtos = new List<ToDoListItemLoadingDto>();

                foreach (var itemDbo in itemDbos) {
                    itemDtos.Add(ParseItemDboToDto(itemDbo, username, listMembers));
                }
                listDtos.Add(new ToDoListDto(listDbo.ListId, listDbo.Name, itemDtos, listMembers, listDbo.IsCollaborative));
            }
            return listDtos;
        }

        public Task AddItemToList(long listId, string itemDescription, uint itemEffort)
        {
            _listItemRepository.AddItemToList(listId, new ToDoListItemDbo(0, itemDescription, itemEffort));
            return Task.CompletedTask;
        }

        public Task DeleteItem(long itemId)
        {
            _listItemRepository.RemoveItemFromList(itemId);
            var listId = _listItemRepository.GetListIdFromItem(itemId);
            var listMembers = _listMemberRepository.GetListMembers(listId);
            foreach (var member in listMembers) {
                _listItemRepository.DeleteItemStatus(itemId, member);
            }
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
            var finishedMembers = new List<string>();
            foreach (var member in listMembers) {
                if ((_listItemRepository.GetItemStatus(dbo.ItemId, member) == true) && !member.Equals(currentUser)) {
                    finishedMembers.Add(member);
                }
            }
            return new ToDoListItemLoadingDto(dbo.ItemId, dbo.Description, isDone, finishedMembers, dbo.Effort);
        }
        #endregion
    }
}
