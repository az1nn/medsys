using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medsys.Models;
using medsys.Data;
using System.Text.Json;
using medsys.Entities;
using medsys.Services;
using Microsoft.AspNetCore.Http.HttpResults;

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

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _userService.GetAll());
        }

        //[HttpGet("user/{id}")]
        //public async Task<ActionResult<User>> GetUserById(int id)
        //{
        //    var user = await _context.Users.FindAsync(id); 
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDTO request)
        {
            if (request.LoginEmail == null || request.Password == null || request.FullName == null)
            {
                return BadRequest("Empty fields, please fill them");
            }
            return await _userService.registerUser(request);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDTO request)
        {
            return await _userService.loginUser(request); 
        }

    }
}
