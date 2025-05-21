using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TDC.Backend.Helpers;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListController(IToDoListHandler listHandler) : ControllerBase
    {
        internal readonly IToDoListHandler _listHandler = listHandler;

        #region To-Do-List
        [HttpPut("createList/{sender}")]
        public long CreateToDoList([FromRoute] string sender, [FromBody] ToDoListSavingDto listLoadingDto)
        {
           return _listHandler.CreateList(sender, listLoadingDto);
        }

        [HttpPost("updateListTitle/{listId}")]
        public async Task UpdateToDoListTitle([FromRoute] long listId, [FromBody] ListTitleHelper newTitle)
        {
            await _listHandler.UpdateListTitle(listId, newTitle.ListTitle);
        }

        [HttpPost("deleteList/{listId}")]
        public async Task DeleteToDoList([FromRoute] long listId, [FromBody] UsernameHelper sender)
        {
            await _listHandler.DeleteList(listId, sender.Username);
        }

        [HttpPost("finishList/{listId}")]
        public async Task FinishToDoList([FromRoute] long listId, [FromBody] UsernameHelper sender)
        {
            await _listHandler.FinishList(listId, sender.Username);
        }

        [HttpPut("removeUserFromList/{listId}/{username}")]
        public async Task RemoveUserFromList([FromRoute] long listId, [FromRoute] string username)
        {
            await _listHandler.RemoveUserFromList(listId, username);
        }

        [HttpGet("getListsForUser/{username}")]
        public List<ToDoListLoadingDto> GetListsForUser([FromRoute] string username)
        {
            return _listHandler.GetListsForUser(username);
        }

        [HttpGet("getListById/{listId}")]
        public ToDoListLoadingDto GetListById([FromRoute] long listId)
        {
            return _listHandler.GetListById(listId);
        }
        #endregion

        #region List-Items
        [HttpPut("addItemToList/{listId}")]
        public long AddItemToList([FromRoute] long listId, [FromBody] ToDoListItemSavingDto itemData)
        {
            return _listHandler.AddItemToList(listId, itemData.Description, itemData.Effort);
        }

        [HttpPost("deleteItem/{itemId}")]
        public async Task DeleteItem([FromRoute] long itemId)
        {
            await _listHandler.DeleteItem(itemId);
        }

        [HttpPost("updateItemDescription/{itemId}")]
        public async Task UpdateItemDescription([FromRoute] long itemId, [FromBody] DescriptionHelper description)
        {
            await _listHandler.UpdateItemDescription(itemId, description.Description);
        }

        [HttpPost("updateItemEffort/{itemId}/{newEffort}")]
        public async Task UpdateItemEffort([FromRoute] long itemId, [FromRoute] int newEffort)
        {
            await _listHandler.UpdateItemEffort(itemId, newEffort);
        }

        [HttpPost("setItemStatus/{itemId}")]
        public async Task SetItemStatusDone([FromRoute] long itemId, [FromBody] ItemStatusHelper itemStatus)
        {
            await _listHandler.SetItemStatus(itemId, itemStatus.UpdateForUser, itemStatus.IsDone);
        }

        [HttpGet("getItemsForList/{listId}/{username}")]
        public List<ToDoListItemLoadingDto> GetItemsForList([FromRoute] long listId, [FromRoute] string username)
        {
            return _listHandler.GetItemsForList(listId, username);
        }
        #endregion

        #region List-Invitations

        [HttpPut("sendListInvitaion/{listId}/{forUser}/{fromUser}")]
        public async Task SendListInvitation([FromRoute] long listId, [FromRoute] string forUser, [FromRoute] string fromUser)
        {
            await _listHandler.SendListInvitation(listId, forUser, fromUser);
        }

        [HttpDelete("cancelListInvitaion/{listId}/{forUser}/{fromUser}")]
        public async Task CancelListInvitation([FromRoute] long listId, [FromRoute] string forUser, [FromRoute] string fromUser)
        {
            await _listHandler.CancelListInvitation(listId, forUser, fromUser);
        }

        [HttpPost("acceptListInvitaion/{listId}/{forUser}")]
        public async Task AcceptListInvitation([FromRoute] long listId, [FromRoute] string forUser)
        {
            await _listHandler.AcceptListInvitation(listId, forUser);
        }

        [HttpPost("denyListInvitaion/{listId}/{forUser}")]
        public async Task DenyListInvitation([FromRoute] long listId, [FromRoute] string forUser)
        {
            await _listHandler.DenyListInvitation(listId, forUser);
        }

        [HttpGet("getListInvitationsForUser/{username}")]
        public List<ListInvitationDto> GetListInvitationsForUser([FromRoute] string username) {
            return _listHandler.LoadListInvitationsForUser(username);
        }
        #endregion

        #region rewarding
        [HttpGet("getRewardingById/{username}/{listId}")]
        public RewardingMessageDto GetRewardingById([FromRoute] string username, long listId)
        {
            return _listHandler.GetRewardingById(username, listId);
        }

        [HttpGet("getOpenRewardsForUser/{username}")]
        public List<RewardingMessageDto> GetOpenRewardsForUser([FromRoute] string username)
        {
            return _listHandler.GetOpenRewardsForUser(username);
        }

        [HttpPost("removeSeenRewarding/{username}/{listId}")]
        public void RemoveSeenRewardingForUser([FromRoute] string username, [FromRoute] long listId)
        {
            _listHandler.RemoveSeenRewardingForUser(username, listId);
        }
        #endregion
    }
}
