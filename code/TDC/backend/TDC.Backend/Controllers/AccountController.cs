using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        internal readonly IAccountHandler _accountHandler;
        public AccountController(IAccountHandler accountHandler) {
            _accountHandler = accountHandler;
        }

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
        public bool RegisterUser([FromBody] AccountSavingDto accountData)
        {
            return _accountHandler.RegisterUser(accountData);
        }

        [HttpPost("updateUsername/{username}/{newUsername}")]
        public bool UpdateUsername([FromRoute] string username, [FromRoute] string newUsername)
        {
            return _accountHandler.UpdateUsername(username, newUsername);
        }

        [HttpPost("updateUserDescription/{username}")]
        public async Task UpdateUserDescription([FromRoute] string username, [FromBody] StringHelper description)
        {
            await _accountHandler.UpdateUserDescription(username, description.Text);
        }

        [HttpPost("updateEmail/{username}")]
        public bool UpdateEmail([FromRoute] string username, [FromBody] StringHelper email)
        {
            return _accountHandler.UpdateEmail(username, email.Text);
        }

        [HttpPost("updatePassword/{username}")]
        public bool UpdatePassword([FromRoute] string username, [FromBody] StringHelper password)
        {
            return _accountHandler.UpdatePassword(username, password.Text);
        }

        [HttpPost("logInWithMail")]
        public bool LoginWithMail([FromBody] MailLoginHelper loginData)
        {
            return _accountHandler.LoginWithMail(loginData.Email, loginData.Password);
        }

        [HttpPost("logInWithUsername")]
        public bool LoginWithUsername([FromBody] UsernameLoginHelper loginData)
        {
            return _accountHandler.LoginWithUsername(loginData.Username, loginData.Password);
        }

        [HttpGet("getAccountData/{username}")]
        public AccountLoadingDto GetAccountByUsername([FromRoute] string username)
        {
            return _accountHandler.GetAccountByUsername(username);
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
