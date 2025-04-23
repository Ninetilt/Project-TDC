using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain.Models;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        [HttpPut("registerUser")]
        public async Task<long> RegisterUser([FromBody] UserDto user)
        {
            throw new NotImplementedException();
        }

        [HttpPost("updateUserData/{userId}")]
        public async Task UpdateUserData([FromRoute] long userId, [FromBody] UserDto user)
        {
            throw new NotImplementedException();
        }

        public class StringHelper
        {
            public string Text { get; set; }
        }

        [HttpPost("logInWithMail")]
        public async Task<long> LoginWithMail([FromBody] StringHelper email, [FromBody] StringHelper password)
        {
            throw new NotImplementedException();
        }

        [HttpPost("logInWithUsername")]
        public async Task<long> LoginWithUsername([FromBody] StringHelper username, [FromBody] StringHelper password)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getProfileById/{userId}")]
        public async Task<ProfileDto> GetProfileById([FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        #region Friend Management

        [HttpGet("getFriendsForUser/{userId}")]
        public async Task<List<long>> GetFriendsForUser([FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("getFriendRequestsForUser/{userId}")]
        public async Task<List<long>> GetFriendRequestsForUser([FromRoute] long userId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("acceptFriendRequest/{userId}/{requestId}")]
        public async Task AcceptFriendRequest([FromRoute] long userId, [FromRoute] long requestId)
        {
            throw new NotImplementedException();
        }

        [HttpPost("declineFriendRequest/{userId}/{requestId}")]
        public async Task DeclineFriendRequest([FromRoute] long userId, [FromRoute] long requestId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("sendFriendRequest/{senderId}/{receiverId}")]
        public async Task SendFriendRequest([FromRoute] long senderId, [FromRoute] long receiverId)
        {
            throw new NotImplementedException();
        }

        [HttpPut("cancelFriendRequest/{senderId}/{receiverId}")]
        public async Task cancelFriendRequest([FromRoute] long senderId, [FromRoute] long receiverId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
