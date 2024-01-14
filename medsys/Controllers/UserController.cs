using Microsoft.AspNetCore.Mvc;
using medsys.Models;
using medsys.Entities;
using medsys.Services;
using medsys.Auth;
using Microsoft.AspNetCore.Http.HttpResults;

namespace medsys.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class userController : ControllerBase
    {
        private IUserService _userService;
        
        public userController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var result = await _userService.GetAll();
            if(result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return await _userService.GetUserById(id);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDTO request)
        {
            if (request.LoginEmail == null || request.Password == null || request.FullName == null)
            {
                return BadRequest("Empty fields, please fill them");
            }
            return await _userService.RegisterUser(request);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDTO request)
        {
            return await _userService.LoginUser(request); 
        }

    }
}
