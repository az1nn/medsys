using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medsys.Models;
using medsys.Data;
using System.Text.Json;
using medsys.Entities;

namespace medsys.Controllers
{
    [ApiController]
    [Route("api")]
    public class userController : ControllerBase
    {
        private readonly UserContext _context; 
        
        public userController(UserContext context)
        {
            _context = context;
        }

        public static User user = new User();

        [HttpGet("user")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id); 
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost("register")]
        public async Task<IActionResult> register(UserRegisterDTO request)
        {
            if (request.LoginEmail == null || request.Password == null || request.FullName == null)
            {
                return BadRequest("Empty fields, please fill them");
            }
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Id = Guid.NewGuid().ToString();
            user.HashedPassword = passwordHash;
            user.LoginEmail = request.LoginEmail;
            user.FullName = request.FullName;
            user.IsDoctor = request.IsDoctor;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var result = new {
                Message = "User Created with success"
            };
            
            return Ok(JsonSerializer.Serialize(result));
        }

        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.LoginEmail == request.LoginEmail);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            if (BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            {
                return BadRequest("Wrong password, try again");
            }

            var result = new
            {
                Message = "Success"
            };

            return Ok(result);
        }

    }
}
