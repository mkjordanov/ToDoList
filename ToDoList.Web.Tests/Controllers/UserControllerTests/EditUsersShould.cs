using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.Admin.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.UserControllerTests
{
    [TestFixture]
    public class EditUsersShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditUsers(string.Empty, It.IsAny<UserViewModel>()); });
        }
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditUsers(null, It.IsAny<UserViewModel>()); });
        }

        [Test]
        public void CallUsersServiceMethodGetUserById_OnlyOnce()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var mockedUser = new Mock<ApplicationUser>();

            var id = Guid.NewGuid();

            mockedUser.Object.Id = id.ToString();
            mockedUserService.Setup(s => s.GetUserById(id.ToString())).Returns(mockedUser.Object);

            var controller = new UsersController(mockedUserService.Object);
            var userModel = new UserViewModel() { FirstName = "sampleName", LastName = "sampleName", Email = "Emial", UserName ="UserName" };

            //Act
            controller.EditUsers(id.ToString(), userModel);

            //Assert
            mockedUserService.Verify(s => s.GetUserById(id.ToString()), Times.Once);
        }

        public void CallUsersServiceMethodUpdateUser_OnlyOnce()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var mockedUser = new Mock<ApplicationUser>();

            var id = Guid.NewGuid();

            mockedUser.Object.Id = id.ToString();
            mockedUserService.Setup(s => s.GetUserById(id.ToString())).Returns(mockedUser.Object);

            var controller = new UsersController(mockedUserService.Object);
            var userModel = new UserViewModel() { FirstName = "sampleName", LastName = "sampleName", Email = "Emial", UserName = "UserName" };

            //Act
            controller.EditUsers(id.ToString(), userModel);

            //Assert
            mockedUserService.Verify(s => s.UpdateUser(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public void RedirectToIndex()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var mockedUser = new Mock<ApplicationUser>();

            var id = Guid.NewGuid();

            mockedUser.Object.Id = id.ToString();
            mockedUserService.Setup(s => s.GetUserById(id.ToString())).Returns(mockedUser.Object);

            var controller = new UsersController(mockedUserService.Object);
            var userModel = new UserViewModel() { FirstName = "sampleName", LastName = "sampleName", Email = "Emial", UserName = "UserName" };

            //Act&Assert
            controller.WithCallTo(c => c.EditUsers(id.ToString(), userModel)).ShouldRedirectTo(r => r.Index());
        }
    }
}
