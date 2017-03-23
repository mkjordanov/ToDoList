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
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    public class DoneShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Done(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Done(null); });
        }

        [Test]
        public void CallTaskServiceFindTaskById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.Done(id.ToString());

            //Assert
            mokcedTaskService.Verify(u => u.FindTaskById(id), Times.Once);
        }

        [Test]
        public void CallTaskServiceUpdateTask_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.Done(id.ToString());

            //Assert
            mokcedTaskService.Verify(u => u.UpdateTask(mockedToDoListTask.Object), Times.Once);
        }

        [Test]
        public void RedirectToIndex()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert

            controller.WithCallTo(c => c.Done(id.ToString())).ShouldRedirectTo(r => r.Index(It.IsAny<string>()));


        }
    }
}
