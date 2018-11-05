using ELearner.Core.ApplicationService;
using ELearner.Core.Entity.BusinessObjects;
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
        public ActionResult<UserBO> Register([FromBody]UserBO user, string password)
        {
            user.Username = user.Username.ToLower();

           /* if (_authService.UserExists(user.Username))
            {
                return BadRequest("Username already exists");
            }*/

            var userToCreate = new UserBO {
                Username = user.Username
            };

            var createdUser = _authService.Register(userToCreate, password);

            return StatusCode(201);
        }
    }
}