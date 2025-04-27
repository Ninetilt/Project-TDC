using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TDC.Backend.Helpers;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListController : ControllerBase
    {
        internal readonly IToDoListHandler _listHandler;
        public ListController(IToDoListHandler listHandler) {
            _listHandler = listHandler;
        }

        #region To-Do-List
        [HttpPut("createList/{sender}")]
        public async Task CreateToDoList([FromRoute] string sender, [FromBody] ToDoListDto listDto)
        {
           await _listHandler.CreateList(sender, listDto);
        }

        [HttpPost("updateListTitle/{listId}")]
        public async Task UpdateToDoListTitle([FromRoute] long listId, [FromBody] ListTitleHelper newTitle)
        {
            await _listHandler.UpdateListTitle(listId, newTitle.ListTitle);
        }

        [HttpDelete("deleteList/{listId}")]
        public async Task DeleteToDoList([FromRoute] long listId, [FromBody] UsernameHelper sender)
        {
            await _listHandler.DeleteList(listId, sender.Username);
        }

        [HttpPost("finishList/{listId}")]
        public async Task FinishToDoList([FromRoute] long listId, [FromBody] UsernameHelper sender)
        {
            await _listHandler.FinishList(listId, sender.Username);
        }

        [HttpPut("addUserToList/{listId}/{username}")]
        public async Task AddUserToList([FromRoute] long listId, [FromRoute] string username)
        {
            await _listHandler.AddUserToList(listId, username);
        }

        [HttpPut("removeUserFromList/{listId}/{username}")]
        public async Task RemoveUserFromList([FromRoute] long listId, [FromRoute] string username)
        {
            await _listHandler.RemoveUserFromList(listId, username);
        }

        [HttpGet("getListsForUser/{username}")]
        public List<ToDoListDto> GetListsForUser([FromRoute] string username)
        {
            return _listHandler.GetListsForUser(username);
        }
        #endregion

        #region List-Items
        [HttpPut("addItemToList/{listId}")]
        public async Task AddItemToList([FromRoute] long listId, [FromBody] ToDoListItemSavingDto itemData)
        {
            await _listHandler.AddItemToList(listId, itemData.Description, itemData.Effort);
        }

        [HttpDelete("deleteItem/{itemId}")]
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
        public async Task UpdateItemEffort([FromRoute] long itemId, [FromRoute] uint newEffort)
        {
            await _listHandler.UpdateItemEffort(itemId, newEffort);
        }

        [HttpPost("setItemStatus/{itemId}")]
        public async Task SetItemStatusDone([FromRoute] long itemId, [FromBody] ItemStatusHelper itemStatus)
        {
            await _listHandler.SetItemStatus(itemId, itemStatus.UpdateForUser, itemStatus.IsDone);
        }
        #endregion
    }
}
