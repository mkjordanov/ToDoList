using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.Admin.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.UserControllerTests
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void CallUserServicesGetAllUsers_OnlyOnce()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act
            controller.Index();

            //Asert
            mockedUserService.Verify(s => s.GetAllUsers(), Times.Once);

        }

        [Test]
        public void RenderDefaultView()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultView_WithCorrectModel()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            var user = new ApplicationUser() { FirstName = "Name" };
            var expectedList = new List<ApplicationUser>() { user };

            mockedUserService.Setup(s => s.GetAllUsers()).Returns(expectedList);

            //Act&Assert
            controller
                .WithCallTo(c => c.Index())
                .ShouldRenderDefaultView()
                .WithModel<List<UserViewModel>>(actual =>
                {
                    Assert.AreEqual(actual[0].FirstName, expectedList[0].FirstName);
                });
        }
    }
}
