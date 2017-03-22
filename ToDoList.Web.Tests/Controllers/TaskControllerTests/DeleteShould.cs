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
    class DeleteShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Delete(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Delete(null); });
        }

        [Test]
        public void CallTaskServiceFindTaskById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.Delete(Guid.NewGuid().ToString());

            //Assert
            mokcedTaskService.Verify(u => u.FindTaskById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Delete(Guid.NewGuid().ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultView_WithCorrectModel()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedTask.Object.Id = id;

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Delete(id.ToString())).ShouldRenderDefaultView().WithModel(mockedTask.Object);
        }

    }
}
