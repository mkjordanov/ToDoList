using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.Admin.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.UserControllerTests
{
    [TestFixture]
    public class EditShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Edit(string.Empty); });
        }
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Edit(null); });
        }

        [Test]
        public void CallUserServiceMethodGetUserById_OnlyOnce()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists };

            mockedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);

            var controller = new UsersController(mockedUserService.Object);

            //Act
            controller.Edit(userId.ToString());

            //Assert
            mockedUserService.Verify(u => u.GetUserById(userId.ToString()), Times.Once);
        }

        [Test]
        public void RenderDefulatView()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists };

            mockedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);

            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Edit(userId.ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefulatViewWithCorrectModel()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();

            var userId = Guid.NewGuid();
            var lists = new List<ToDoListModel>();
            var user = new ApplicationUser() { Id = userId.ToString(), ToDoLists = lists };

            mockedUserService.Setup(x => x.GetUserById(userId.ToString())).Returns(user);

            var controller = new UsersController(mockedUserService.Object);
            
            //Act&Assert
            controller.WithCallTo(c => c.Edit(userId.ToString()))
                .ShouldRenderDefaultView()
                 .WithModel<UserViewModel>(actualUser =>
                 {
                     Assert.AreSame(user.Id, actualUser.Id);
                     Assert.AreSame(user.FirstName, actualUser.FirstName);
                 });
        }
    }
}
