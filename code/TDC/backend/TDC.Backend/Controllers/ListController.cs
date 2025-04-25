using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListController : ControllerBase
    {
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
        public Task<long> CreateToDoList([FromRoute] string username, [FromBody] ToDoListDto listDto)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateListTitle/{listId}")]
        public Task UpdateToDoList([FromRoute] long listId, [FromBody] StringHelper newTitle)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("deleteList/{listId}")]
        public Task DeleteToDoList([FromRoute] long listId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("finishList/{listId}")]
        public Task FinishToDoList([FromRoute] long listId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("addUserToList/{listId}/{username}")]
        public Task AddUserToList([FromRoute] long listId, [FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpPut("removeUserFromList/{listId}/{username}")]
        public Task RemoveUserFromList([FromRoute] long listId, [FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getListsForUser/{username}")]
        public Task<List<ToDoListDto>> GetListsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region List-Items

        [HttpPut("addItemToList/{listId}")]
        public Task<long> AddItemToList([FromRoute] long listId, [FromBody] ToDoListItemSavingDto itemData)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("deleteItem/{itemId}")]
        public Task DeleteItem([FromRoute] long itemId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemDescription/{itemId}")]
        public Task UpdateItemDescription([FromRoute] long itemId, [FromBody] StringHelper newText)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemEffort/{itemId}")]
        public Task UpdateItemEffort([FromRoute] long itemId, [FromBody] long newEffort)
        {
            throw new NotImplementedException();
        }

        [HttpPost("setItemStatus/{itemId}")]
        public Task SetItemStatusDone([FromRoute] long itemId, [FromBody] ItemStatusHelper)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
