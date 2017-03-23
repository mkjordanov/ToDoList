using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    public class DeleteTaskShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.DeleteTask(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.DeleteTask(null); });
        }

        [Test]
        public void CallTaskServiceFindTaskById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.DeleteTask(Guid.NewGuid().ToString());

            //Assert
            mokcedTaskService.Verify(u => u.FindTaskById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallTaskServiceDeleteTask_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.DeleteTask(Guid.NewGuid().ToString());

            //Assert
            mokcedTaskService.Verify(u => u.DeleteTask(It.IsAny<ToDoListTask>()), Times.Once);
        }

        [Test]
        public void RedirectToIndex()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert

            controller.WithCallTo(c => c.DeleteTask(Guid.NewGuid().ToString())).ShouldRedirectTo(r=>r.Index(It.IsAny<string>()));


        }
    }
}
