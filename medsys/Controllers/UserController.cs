using Microsoft.AspNetCore.Mvc;
using medsys.Models;
using medsys.Entities;
using medsys.Services;

namespace medsys.Controllers
{
    [ApiController]
    [Route("api")]
    public class userController : ControllerBase
    {
        private IUserService _userService;
        
        public userController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDTO request)
        {
            if (request.LoginEmail == null || request.Password == null || request.FullName == null)
            {
                return BadRequest("Empty fields, please fill them");
            }
            return await _userService.RegisterUser(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDTO request)
        {
            return await _userService.LoginUser(request); 
        }

    }
}
