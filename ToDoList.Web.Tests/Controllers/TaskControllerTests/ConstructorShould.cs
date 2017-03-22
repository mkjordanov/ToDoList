using Moq;
using NUnit.Framework;
using System;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenToDoListModelServiceIsNull()
        {
            //Arrange
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new TaskController(null, mokcedTaskService.Object);
            });
        }

        [Test]
        public void Throw_WhenTaskServiceIsNull()
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
