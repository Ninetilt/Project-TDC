using Microsoft.AspNetCore.Mvc;
using TDC.Backend.IDomain;

namespace TDC.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController(ICharacterHandler characterHandler): ControllerBase
    {
        private ICharacterHandler _characterHandler = characterHandler;
    }
}
