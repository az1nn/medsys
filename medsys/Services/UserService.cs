﻿using medsys.Data;
using medsys.Entities;
using medsys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace medsys.Services
{
    public interface IUserService
    {
        Task<IActionResult> RegisterUser(UserRegisterDTO request);
        Task<IActionResult> LoginUser(UserLoginDTO request);
        Task<List<User>> GetAll();
        Task<IActionResult> GetUserById(string id);
    }
    public class UserService: IUserService
    {
        private readonly UserContext _context;
        private readonly ITokenGeneratorService _tokenGeneratorService;
        public UserService(UserContext context, ITokenGeneratorService tokenGeneratorService)
        {
            _context = context;
            _tokenGeneratorService = tokenGeneratorService;

        }
        public static User user = new User();
        public async Task<IActionResult> RegisterUser(UserRegisterDTO request)
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

        public async Task<IActionResult> LoginUser(UserLoginDTO request)
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

            string role = user.IsDoctor ? "Doctor" : "Patient";

            var token = _tokenGeneratorService.GenerateToken(user.LoginEmail, role);

            return new OkObjectResult(new { token = token, message = "Success" });
        }

        public async Task<List<User>> GetAll() 
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return new NotFoundObjectResult("User Not Found");
            }
            return new OkObjectResult(user);
        }

    }
}
