using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        #region helper classes
        public class MailLoginHelper
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UsernameLoginHelper
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class StringHelper
        {
            public string Text { get; set; }
        }
        #endregion

        [HttpPut("registerUser")]
        public Task<long> RegisterUser([FromBody] AccountSavingDto accountData)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateUsername/{username}/{newUsername}")]
        public Task<bool> UpdateUsername([FromRoute] string username, [FromRoute] string newUsername)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateUserDescription/{username}")]
        public Task<bool> UpdateUsername([FromRoute] string username, [FromBody] StringHelper description)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateEmail/{username}")]
        public Task<bool> UpdateEmail([FromRoute] string username, [FromBody] StringHelper email)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updatePassword/{username}")]
        public Task<bool> UpdatePassword([FromRoute] string username, [FromBody] StringHelper password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logInWithMail")]
        public Task<bool> LoginWithMail([FromBody] MailLoginHelper loginData)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logInWithUsername")]
        public Task<bool> LoginWithUsername([FromBody] UsernameLoginHelper loginData)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getAccountData/{username}")]
        public Task<AccountLoadingDto> GetAccountById([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        #region Friend Management

        [HttpGet("getFriendsForUser/{username}")]
        public Task<List<string>> GetFriendsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getFriendRequestsForUser/{username}")]
        public Task<List<string>> GetFriendRequestsForUser([FromRoute] string username)
        {
            throw new NotImplementedException();
        }

        [HttpPost("acceptFriendRequest/{username}/{requestName}")]
        public Task<bool> AcceptFriendRequest([FromRoute] string username, [FromRoute] string requestName)
        {
            throw new NotImplementedException();
        }

        [HttpPost("denyFriendRequest/{username}/{requestName}")]
        public Task<bool> DenyFriendRequest([FromRoute] string senderName, [FromRoute] string requestName)
        {
            throw new NotImplementedException();
        }

        [HttpPut("sendFriendRequest/{senderName}/{receiverName}")]
        public Task<bool> SendFriendRequest([FromRoute] string senderName, [FromRoute] long receiverName)
        {
            throw new NotImplementedException();
        }

        [HttpPut("cancelFriendRequest/{senderName}/{receiverName}")]
        public Task<bool> CancelFriendRequest([FromRoute] string senderName, [FromRoute] long receiverName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
