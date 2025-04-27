using Microsoft.AspNetCore.Mvc;
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

        #region helper classes
        public class StringHelper
        {
            public string Text { get; set; }
        }

        public class ItemStatusHelper
        {
            public string UpdateForUser { get; set; }
            public bool IsDone { get; set; }
        }
        #endregion

        #region To-Do-List
        [HttpPut("createList/{username}")]
        public async Task CreateToDoList([FromRoute] string username, [FromBody] ToDoListDto listDto)
        {
           await _listHandler.CreateList(username, listDto);
        }

        [HttpPost("updateListTitle/{listId}")]
        public async Task UpdateToDoListTitle([FromRoute] long listId, [FromBody] StringHelper newTitle)
        {
            await _listHandler.UpdateListTitle(listId, newTitle.Text);
        }

        [HttpDelete("deleteList/{listId}")]
        public async Task DeleteToDoList([FromRoute] long listId, [FromBody] StringHelper sender)
        {
            await _listHandler.DeleteList(listId, sender.Text);
        }

        [HttpPost("finishList/{listId}")]
        public async Task FinishToDoList([FromRoute] long listId, [FromRoute] StringHelper sender)
        {
            await _listHandler.FinishList(listId, sender.Text);
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
        public async Task UpdateItemDescription([FromRoute] long itemId, [FromBody] StringHelper newText)
        {
            await _listHandler.UpdateItemDescription(itemId, newText.Text);
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
