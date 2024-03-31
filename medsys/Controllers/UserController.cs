using Microsoft.AspNetCore.Mvc;
using medsys.Models;
using medsys.Entities;
using medsys.Services;
using medsys.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace medsys.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        private readonly IJwtUtils _jwtUtils;

        public UserController(UserService userService, IJwtUtils jwtUtils)
        {
            _userService = userService;
            _jwtUtils = jwtUtils;
        }

        [HttpGet("users")]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType<User>(StatusCodes.Status204NoContent)]
        public async Task<Results<NoContent, Ok<DefaultResponseDto>>> GetUsers()
        {
            var result = await _userService.GetAll();
            if (result == null)
            {
                return TypedResults.NoContent();
            }
            return TypedResults.Ok(new DefaultResponseDto { Data = result });
        }

        [HttpGet("users/{id}")]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<Results<Ok<DefaultResponseDto>, NotFound>> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            if (result == null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(new DefaultResponseDto { Data = result });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<DefaultResponseDto>, BadRequest<DefaultResponseDto>>> Register(UserRegisterDTO request)
        {
            var id = await _userService.RegisterUser(request);
            if (id.IsNullOrEmpty())
            {
                return TypedResults.BadRequest(new DefaultResponseDto { Status = "fail", Message = "Server error, try again" });
            }

            return TypedResults.Ok(new DefaultResponseDto { Status = "success", Data = new { userId = id } });
        }

        [AllowAnonymous]
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<DefaultResponseDto>, BadRequest<DefaultResponseDto>>> Login(UserLoginDTO request)
        {
            var user = await _userService.LoginUser(request);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.HashedPassword))
            {
                return TypedResults.BadRequest(new DefaultResponseDto { Status = "fail", Message = "Wrong login/password, try again" });
            }
            var token = _jwtUtils.GenerateJwtToken(user);
            return TypedResults.Ok(new DefaultResponseDto { Status = "success", Data = token });
        }
    }
}
