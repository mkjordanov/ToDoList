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
        public void CallTaskServiceGetUserById_OnlyOnce()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);

            var id = Guid.NewGuid().ToString();
            //Act
            controller.Delete(id.ToString());

            //Assert
            mokcedUserService.Verify(u => u.GetUserById(id.ToString()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            var controller = new UsersController(mokcedUserService.Object);
            //Act&Assert
            controller.WithCallTo(c => c.Delete(Guid.NewGuid().ToString())).ShouldRenderDefaultView();
        }

        //[Test]
        //public void ReturnDefaultView_WithCorrectModel()
        //{
        //    //Arrange
        //    var mokcedUserService = new Mock<IUserService>();
        //    var mockedUser = new Mock<ApplicationUser>();

        //    var id = Guid.NewGuid();
        //    mockedUser.Object.Id = id.ToString();

        //    mokcedUserService.Setup(s => s.GetUserById(id)).Returns(mockedUser.Object);

        //    var controller = new UsersController(mokcedUserService.Object);

        //    //Act&Assert
        //    controller.WithCallTo(c => c.Delete(id.ToString())).ShouldRenderDefaultView().WithModel(mockedUser.Object);
        //}
    }
}
