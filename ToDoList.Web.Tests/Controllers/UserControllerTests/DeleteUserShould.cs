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

namespace ToDoList.Web.Tests.Controllers.UserControllerTests
{
    [TestFixture]
    public class DeleteUserShould
    {

        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);
            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.DeleteUser(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.DeleteUser(null); });
        }

        [Test]
        public void CallTaskServiceGetUserById_OnlyOnce()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);
            var id = Guid.NewGuid();
            //Act
            controller.DeleteUser(id.ToString());

            //Assert
            mokcedUserService.Verify(u => u.GetUserById(id.ToString()), Times.Once);
        }

        [Test]
        public void CallTaskServiceDeleteTask_OnlyOnce()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);

            //Act
            controller.DeleteUser(Guid.NewGuid().ToString());

            //Assert
            mokcedUserService.Verify(u => u.DeleteUser(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public void RedirectToIndex()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);

            //Act&Assert

            controller.WithCallTo(c => c.DeleteUser(Guid.NewGuid().ToString())).ShouldRedirectTo(r => r.Index());

        }
    }
}
