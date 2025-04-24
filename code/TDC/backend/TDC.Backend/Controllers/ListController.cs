using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListController : ControllerBase
    {
        #region To-Do-List
        [HttpPut("createList/{username}")]
        public Task<long> CreateToDoList([FromRoute] string username, [FromBody] ToDoListDto listDto)
        {
            throw new NotImplementedException();
        }

        public class StringHelper
        {
            public string Text { get; set; }
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
        public Task<ToDoListDto> GetListsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getListById/{listId}/{username}")]
        public Task<ToDoListDto> GetListById([FromRoute] long listId, [FromRoute] string username)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region List-Items

        [HttpPut("addItemToList/{listId}")]
        public Task AddItemToList([FromRoute] long listId, [FromBody] StringHelper itemText, [FromBody] long itemEffort)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("deleteItemFromList/{itemId}")]
        public Task DeleteItemFromList([FromRoute] long itemId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemText/{itemId}")]
        public Task UpdateItemText([FromRoute] long itemId, [FromBody] StringHelper newText)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemEffort/{itemId}")]
        public Task UpdateItemEffort([FromRoute] long itemId, [FromBody] long newEffort)
        {
            throw new NotImplementedException();
        }

        [HttpPost("setItemStatusDone/{itemId}/{username}")]
        public Task SetItemStatusDone([FromRoute] long itemId, [FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost("setItemStatusNotDone/{itemId}/{username}")]
        public Task SetItemStatusNotDone([FromRoute] long itemId, [FromRoute] string username)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
