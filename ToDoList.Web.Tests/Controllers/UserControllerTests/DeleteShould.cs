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
    public class DeleteShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Delete(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Delete(null); });
        }

        [Test]
        public void CallUserServiceGetUserById_OnlyOnce()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists };

            mokcedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);
            //Act
            controller.Delete(userId.ToString());

            //Assert
            mokcedUserService.Verify(u => u.GetUserById(userId.ToString()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists };

            mokcedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);

            var controller = new UsersController(mokcedUserService.Object);
            //Act&Assert
            controller.WithCallTo(c => c.Delete(userId.ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultView_WithCorrectModel()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists, FirstName = "Sample name" };

            mokcedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);

            var controller = new UsersController(mokcedUserService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Delete(userId.ToString()))
                .ShouldRenderDefaultView()
                .WithModel<UserViewModel>(actualUser =>
                {
                    Assert.AreSame(user.Id, actualUser.Id);
                    Assert.AreSame(user.FirstName, actualUser.FirstName);
                });
        }
    }
}
