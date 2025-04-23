using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ListController : ControllerBase
    {
        #region To-Do-List
        [HttpPut("createList/{userId}")]
        public async Task<long> CreateToDoList([FromBody] ToDoListDto listDto)
        {
            throw new NotImplementedException();
        }

        public class StringHelper
        {
            public string Text { get; set; }
        }

        [HttpPost("updateListTitle/{listId}/{userId}")]
        public async Task UpdateToDoList([FromRoute] long listId, [FromRoute] long userId, [FromBody] StringHelper newTitle)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("deleteList/{listId}/{userId}")]
        public async Task DeleteToDoList([FromRoute] long listId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("finishList/{listId}/{userId}")]
        public async Task FinishToDoList([FromRoute] long listId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("addUserToList/{listId}/{userId}")]
        public async Task AddUserToList([FromRoute] long listId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getListsForUser/{userId}")]
        public async Task<ToDoListDto> GetListsForUser([FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getListById/{listId}/{userId}")]
        public async Task<ToDoListDto> GetListById([FromRoute] long listId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region List-Items

        [HttpPut("addItemToList/{listId}")]
        public async Task AddItemToList([FromRoute] long listId, [FromBody] StringHelper itemText, [FromBody] long itemEffort)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemText/{itemId}")]
        public async Task UpdateItemText([FromRoute] long itemId, [FromBody] StringHelper newText)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateItemEffort/{itemId}")]
        public async Task UpdateItemEffort([FromRoute] long itemId, [FromBody] long newEffort)
        {
            throw new NotImplementedException();
        }

        [HttpPost("setItemStatusDone/{itemId}/{userId}")]
        public async Task SetItemStatusDone([FromRoute] long itemId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("setItemStatusNotDone/{itemId}/{userId}")]
        public async Task SetItemStatusNotDone([FromRoute] long itemId, [FromRoute] long userId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
