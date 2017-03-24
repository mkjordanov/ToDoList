using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.Admin.Controllers;

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
            var controller = new UsersController(mockedUserService.Object);

            //Act
            controller.Edit(Guid.NewGuid().ToString());

            //Assert
            mockedUserService.Verify(u => u.GetUserById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RenderDefulatView()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var controller = new UsersController(mockedUserService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Edit(Guid.NewGuid().ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefulatViewWithCorrectModel()
        {
            //Arrange
            var mockedUserService = new Mock<IUserService>();
            var mockedUser = new Mock<ApplicationUser>();

            var id = Guid.NewGuid();
            mockedUser.Object.Id = id.ToString();

            var controller = new UsersController(mockedUserService.Object);

            mockedUserService.Setup(s => s.GetUserById(id.ToString())).Returns(mockedUser.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Edit(id.ToString())).ShouldRenderDefaultView().WithModel(mockedUser.Object);
        }
    }
}
