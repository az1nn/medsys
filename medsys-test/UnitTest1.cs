using medsys.Controllers;
using medsys.Entities;
using medsys.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace medsys_test
{
    public class UserControllerTests
    {
        [Fact]
        async public void GetUsersTest()
        {
            var MockService = new Mock<IUserService>();
            MockService.Setup(service => service.GetAll())
                .Returns(GetAllUsersMock);
            var controller = new userController(MockService.Object);


            var result = await controller.GetUsers();

            Assert.NotNull(result);
            Assert.IsAssignableFrom<ActionResult<List<User>>>(result);
        }
        [Fact]
        async public void GetUsersTest_NoContent()
        {
            var MockService = new Mock<IUserService>();
            MockService.Setup(service => service.GetAll())
                .Returns(Task.FromResult(new List<User>()));
            var controller = new userController(MockService.Object);


            var result = await controller.GetUsers();


            Assert.Null(result.Value);
            Assert.IsAssignableFrom<ActionResult<List<User>>>(result);
        }
        private Task<List<User>> GetAllUsersMock()
        {
            var users = new List<User>();
            users.Add(new User
            {
                Id = "id",
                FullName = "name",
                HashedPassword = "password",
                IsDoctor = true,
                LoginEmail = "email",
            });
            users.Add(new User
            {
                Id = "id2",
                FullName = "name2",
                HashedPassword = "password2",
                IsDoctor = false,
                LoginEmail = "email2",
            });
            return Task.FromResult(users);
        }
    }
}