using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ListController : ControllerBase
    {
        [HttpPut("createList")]
        public async Task<long> CreateToDoList()
        {
            return 0;
        }

        [HttpPost("updateList")]
        public async Task UpdateToDoList()
        {
            return;
        }

        [HttpGet("test")]
        public async Task Test()
        {
        }
    }
}
