using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using medsys.Models;
using medsys.Data;
using BCrypt.Net;

namespace medsys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class userController : ControllerBase
    {
        private readonly UserContext _context; 
        
        public userController(UserContext context)
        {
            _context = context;
        }

        public static User user = new User();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id); 
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<User>>> registerUser(UserDTO request)
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
            return Ok(user.Id);
        }
    }
}
