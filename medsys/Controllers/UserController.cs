using Microsoft.AspNetCore.Mvc;
using medsys.Models;
using medsys.Entities;
using medsys.Services;
using medsys.Auth;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

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
            return TypedResults.Ok(new DefaultResponseDto { Data = result});
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<Results<Ok<DefaultResponseDto>, BadRequest<DefaultResponseDto>>> Register(UserRegisterDTO request)
        {
            if (request.LoginEmail == null || request.Password == null || request.FullName == null)
            {
                return TypedResults.BadRequest(new DefaultResponseDto { Status = "Empty fields, please fill them" });
            }
            var created = await _userService.RegisterUser(request);
            if (!created)
            {
                return TypedResults.BadRequest(new DefaultResponseDto { Status = "server error" });
            }
            else
            {
                return TypedResults.Ok(new DefaultResponseDto { Status = "created" });
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> login(UserLoginDTO request)
        {
            return await _userService.LoginUser(request);
        }

    }
}
