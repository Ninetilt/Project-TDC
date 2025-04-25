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

        public long CreateList(string creator, ToDoListDto newList)
        {
            var listDbo = new ToDoListDbo(newList.ListId, newList.Name, newList.IsCollaborative, false);
            var listId = _listRepository.CreateList(listDbo);
            _listMemberRepository.AddListMember(listId, creator, true);
            return listId;
        }

        public void AddUserToList(long listId, string username)
        {
            _listMemberRepository.AddListMember(listId, username, false);
        }

        public void RemoveUserFromList(long listId, string username)
        {
            if (_listMemberRepository.UserIsCreator(listId, username)) {
                return;
            }
            _listMemberRepository.RemoveListMember(listId, username);
        }

        public bool SendListInvitation(long listId, string fromUser, string ForUser)
        {
            throw new NotImplementedException();
        }

        public void DeclineListInvitation(long listId, string decliningUser)
        {
            throw new NotImplementedException();
        }

        public void AcceptListInvitation(long listId, string newUser)
        {
            throw new NotImplementedException();
        }

        public List<ListInvitationDto> LoadListInvitationsForUser(string username)
        {
            throw new NotImplementedException();
        }

        public void DeleteList(long listId, string sender)
        {
            if(!_listMemberRepository.UserIsCreator(listId, sender)) { return; }
            _listRepository.DeleteList(listId);
        }

        public void FinishList(long listId, string sender)
        {
            if(!_listMemberRepository.UserIsCreator(listId, sender)) { return; }
            _listRepository.FinishList(listId);
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

        public long AddItemToList(long listId, string itemDescription, uint itemEffort)
        {
            return _listItemRepository.AddItemToList(listId, new ToDoListItemDbo(0, itemDescription, itemEffort));
        }

        public void DeleteItem(long itemId)
        {
            _listItemRepository.RemoveItemFromList(itemId);
            var listId = _listItemRepository.GetListIdFromItem(itemId);
            var listMembers = _listMemberRepository.GetListMembers(listId);
            foreach (var member in listMembers) {
                _listItemRepository.DeleteItemStatus(itemId, member);
            }
        }

        public void UpdateItemDescription(long itemId, string description)
        {
            _listItemRepository.UpdateItemDescription(itemId, description);
        }

        public void UpdateItemEffort(long itemId, uint effort)
        {
            _listItemRepository.UpdateItemEffort(itemId, effort);
        }

        public void UpdateListTitle(long listId, string newTitle)
        {
            _listRepository.UpdateListTitle(listId, newTitle);
        }

        public void SetItemStatus(long itemId, string updateForUser, bool isDone)
        {
            _listItemRepository.SetItemStatus(itemId, updateForUser, isDone);
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
