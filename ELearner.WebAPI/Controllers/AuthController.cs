using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
using ELearner.Core.Entity.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace ELearner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<UserBO> Register([FromBody]UserRegisterDto userDto)
        {
            //userDto.Username = userDto.Username.ToLower();

            /* if (_authService.UserExists(userDto.Username))
            {
                return BadRequest("Username already exists");
            }*/

            var createdUser = _authService.Register(userDto);

            return StatusCode(201);
        }
    }
}