using medsys.Auth;
using medsys.Data;
using medsys.Entities;
using medsys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medsys.Services
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegisterDTO request);
        Task<IActionResult> LoginUser(UserLoginDTO request);
        Task<List<User>> GetAll();
        Task<User> GetUserById(string id);
        Task<User> GetUserByIdAuth(string id);
    }
    public class UserService: IUserService
    {
        private readonly UserContext _context;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        private readonly IJwtUtils _jwtUtils; 
        public UserService(UserContext context, ITokenGeneratorService tokenGeneratorService, IJwtUtils jwtUtils)
        {
            _context = context;
            _tokenGeneratorService = tokenGeneratorService;
            _jwtUtils = jwtUtils;

        }
        public static User user = new User();
        public async Task<bool> RegisterUser(UserRegisterDTO request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var newUser = new User()
            {
                Id = Guid.NewGuid().ToString(),
                HashedPassword = passwordHash,
                LoginEmail = request.LoginEmail,
                FullName = request.FullName,
                IsDoctor = request.IsDoctor
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IActionResult> LoginUser(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.LoginEmail == request.LoginEmail);
            if (user == null)
            {
                return new NotFoundObjectResult("User Not Found");

            }
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            {
                return new BadRequestObjectResult("Wrong password, try again"); 
            }

            //string role = user.IsDoctor ? "Doctor" : "Patient";

            var token = _jwtUtils.GenerateJwtToken(user);

            return new OkObjectResult(new { token = token, message = "Success" });
        }

        public async Task<List<User>> GetAll() 
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
            return user;
        }

        public async Task<User?> GetUserByIdAuth(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
            return user;
        }

    }
}
