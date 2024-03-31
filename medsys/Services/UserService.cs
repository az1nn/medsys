using medsys.Data;
using medsys.Entities;
using medsys.Models;
using Microsoft.EntityFrameworkCore;

namespace medsys.Services
{
    public class UserService(UserContext context)
    {
        private readonly UserContext _context = context;
        public static User user = new();
        public async Task<string> RegisterUser(UserRegisterDTO request)
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
            return newUser.Id;
        }

        public async Task<User?> LoginUser(UserLoginDTO request)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.LoginEmail == request.LoginEmail);
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
            return user ?? null;
        }

        public async Task<User?> GetUserByIdAuth(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(item => item.Id == id);
            return user ?? null;
        }

    }
}
