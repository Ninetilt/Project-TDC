using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        [HttpPut("registerUser")]
        public Task<long> RegisterUser([FromBody] AccountSavingDto accountData)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateUserData/{userId}")]
        public Task UpdateUserData([FromRoute] long userId, [FromBody] AccountSavingDto accountData)
        {
            throw new NotImplementedException();
        }

        public class StringHelper
        {
            public string Text { get; set; }
        }

        [HttpPost("logInWithMail")]
        public Task<long> LoginWithMail([FromBody] StringHelper email, [FromBody] StringHelper password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logInWithUsername")]
        public Task<long> LoginWithUsername([FromBody] StringHelper username, [FromBody] StringHelper password)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getAccountByUsername/{username}")]
        public Task<AccountLoadingDto> GetAccountById([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        #region Friend Management

        [HttpGet("getFriendsForUser/{username}")]
        public Task<List<long>> GetFriendsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getFriendRequestsForUser/{username}")]
        public Task<List<long>> GetFriendRequestsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost("acceptFriendRequest/{senderName}/{requestName}")]
        public Task AcceptFriendRequest([FromRoute] string senderName, [FromRoute] string requestName)
        {
            throw new NotImplementedException();
        }

        [HttpPost("declineFriendRequest/{senderName}/{requestName}")]
        public Task DeclineFriendRequest([FromRoute] string senderName, [FromRoute] string requestName)
        {
            throw new NotImplementedException();
        }

        [HttpPut("sendFriendRequest/{senderName}/{receiverName}")]
        public Task SendFriendRequest([FromRoute] string senderName, [FromRoute] long receiverName)
        {
            throw new NotImplementedException();
        }

        [HttpPut("cancelFriendRequest/{senderName}/{receiverName}")]
        public Task CancelFriendRequest([FromRoute] string senderName, [FromRoute] long receiverName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
