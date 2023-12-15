using medsys.Data;
using medsys.Entities;
using medsys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medsys.Services
{
    public interface IUserService
    {
        Task<IActionResult> registerUser(UserRegisterDTO request);
        Task<IActionResult> loginUser(UserLoginDTO request);
        Task<List<User>> GetAll();
    }
    public class UserService: IUserService
    {
        private readonly UserContext _context;
        public UserService(UserContext context)
        {
            _context = context;
        }
        public static User user = new User();
        public async Task<IActionResult> registerUser(UserRegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Id = Guid.NewGuid().ToString();
            user.HashedPassword = passwordHash;
            user.LoginEmail = request.LoginEmail;
            user.FullName = request.FullName;
            user.IsDoctor = request.IsDoctor;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new CreatedResult(String.Empty, new { message = "created" }); 
        }

        public async Task<IActionResult> loginUser(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.LoginEmail == request.LoginEmail);
            if (user == null)
            {
                return new NotFoundObjectResult("User Not Found");

            }
            if (BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            {
                return new BadRequestObjectResult("Wrong password, try again"); 
            }

            return new OkObjectResult(new { userId = user.Id, message = "Success" });
        }

        public async Task<List<User>> GetAll() 
        {
            return await _context.Users.ToListAsync();
        }

    }
}
