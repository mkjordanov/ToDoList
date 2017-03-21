using NUnit.Framework;
using Moq;
using ToDoList.Data.UnitOfWork;
using TestStack.FluentMVCTesting;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using System;

namespace ToDoList.Web.Tests.Controllers
{
    [TestFixture]
    public class Controller_Should
    {
        [Test]
        public void Throw_WhenToDoListModelServiceIsNull()
        {
            //Arrange
            var mokcedUserService = new Mock<IUserService>();

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListController(null, mokcedUserService.Object);
            });
        }

        [Test]
        public void Throw_WhenUserServiceIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListController(mokcedToDoListModelService.Object, null);
            });
        }

    }
}
