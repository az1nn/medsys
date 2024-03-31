using medsys.Auth;
using medsys.Controllers;
using medsys.Entities;
using medsys.Models;
using medsys.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace medsys_test
{
    public class UserControllerTests
    {
        private readonly ITestOutputHelper _output;
        private readonly IJwtUtils _jwtUtils;
        public UserControllerTests(ITestOutputHelper output, IJwtUtils jwtUtils)
        {
            _output = output;
            _jwtUtils = jwtUtils;
        }

        // [Fact]
        // async public void GetUsers_ReturnsListOfUsers()
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     _userService.GetAll().Returns(GetAllUsersMock());

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.GetUsers();

        //     Assert.NotNull(result.Result);
        //     Assert.IsAssignableFrom<Ok<DefaultResponseDto>>(result.Result);
        // }

        // [Fact]
        // async public void GetUsers_ReturnNoContent()
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     _userService.GetAll().ReturnsNull();

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.GetUsers();

        //     Assert.NotNull(result.Result);
        //     Assert.IsAssignableFrom<NoContent>(result.Result);
        // }

        // [Theory]
        // [InlineData("id")]
        // async public void GetUsersById_ReturnsUser(string id)
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     var users = await GetAllUsersMock();
        //     var usersEnum = Task.FromResult(users[0]);

        //     _userService.GetUserById(id).Returns(usersEnum);

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.GetUserById(id);

        //     Assert.NotNull(result.Result);
        //     Assert.IsAssignableFrom<Ok<DefaultResponseDto>>(result.Result);
        // }

        // [Theory]
        // [InlineData("")]
        // async public void GetUsersById_ReturnsNotFound(string id)
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     var users = await GetAllUsersMock();
        //     var usersEnum = Task.FromResult(users[0]);

        //     _userService.GetUserById(id).ReturnsNull();

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.GetUserById(id);

        //     Assert.IsAssignableFrom<NotFound>(result.Result);
        // }

        // [Fact]
        // public async void Register_ReturnBadRequest()
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.Register(new UserRegisterDTO()
        //     {
        //         FullName = null,
        //         IsDoctor = false,
        //         LoginEmail = null,
        //         Password = null
        //     });

        //     Assert.IsAssignableFrom<BadRequest<DefaultResponseDto>>(result.Result);
        // }

        // [Fact]
        // public async void Register_ReturnCreated()
        // {
        //     UserService _userService = Substitute.For<UserService>();

        //     var RequestParams = new UserRegisterDTO()
        //     {
        //         FullName = "fulano",
        //         IsDoctor = false,
        //         LoginEmail = "sa22a@aaaa.com",
        //         Password = "password123"
        //     };

        //     _userService.RegisterUser(RequestParams).Returns<bool>(true);

        //     var controller = new UserController(_userService, _jwtUtils);
        //     var result = await controller.Register(RequestParams);

        //     _output.WriteLine(result.Result.ToString());
        //     Assert.IsAssignableFrom<Ok<DefaultResponseDto>>(result.Result);
        // }

        // private Task<List<User>> GetAllUsersMock()
        // {
        //     var users = new List<User>();
        //     users.Add(new User
        //     {
        //         Id = "id",
        //         FullName = "name",
        //         HashedPassword = "password",
        //         IsDoctor = true,
        //         LoginEmail = "email",
        //     });
        //     users.Add(new User
        //     {
        //         Id = "id2",
        //         FullName = "name2",
        //         HashedPassword = "password2",
        //         IsDoctor = false,
        //         LoginEmail = "email2",
        //     });
        //     return Task.FromResult(users);
        // }
    }
}
