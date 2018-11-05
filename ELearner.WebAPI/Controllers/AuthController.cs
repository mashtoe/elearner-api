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
            var createdUser = _authService.Register(userDto);
            if (createdUser == null)
            {
                return BadRequest("Username already exists");
            }
            return StatusCode(201);
        }

        [HttpPost("login")]
        public ActionResult<UserBO> Login([FromBody]UserLoginDto userDto ) {

            var userToLogin = _authService.Login(userDto);
            if (userToLogin == null)
            {
                return Unauthorized();
            }
            return userToLogin;
        }
    }
}