using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterHandler characterHandler): ControllerBase
    {
        private ICharacterHandler _characterHandler = characterHandler;

        [HttpGet("getCharacterBody/{username}")]
        public string GetCharacterBodyForUser([FromRoute] string username)
        {
            return _characterHandler.GetCharacterBodyForUser(username);
        }

        [HttpGet("getCharacterFace/{username}")]
        public string GetCharacterFaceForUser([FromRoute] string username)
        {
            return _characterHandler.GetCharacterFaceForUser(username);
        }

        [HttpGet("getDefaultCharacterImage")]
        public string GetDefaultCharacterImage()
        {
            return _characterHandler.GetDefaultCharacterImage();
        }

        [HttpPost("updateCharacterFace/{username}/{faceId}")]
        public async Task UpdateCharacterFace([FromRoute] string username, [FromRoute] long faceId)
        {
            await _characterHandler.UpdateCharacterFace(username, faceId);
        }

        [HttpPost("updateCharacterColor/{username}/{color}")]
        public async Task UpdateCharacterColor([FromRoute] string username, [FromRoute] string color)
        {
            await _characterHandler.UpdateCharacterColor(username, color);
        }
    }
}
