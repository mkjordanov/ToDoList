using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    public class EditTaskShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditTask(string.Empty, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(null, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenNewTaskIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(id, null); });
        }

        [Test]
        public void Throw_WhenNewTaskCategoryIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = string.Empty;
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "1";
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();


            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditTask(id, mockedTaskViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNewTaskCategoryIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = null;
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "1";
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(null, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenNewTaskExpirationDateIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = string.Empty;
            mockedTaskViewModel.Object.priority = "1";
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();


            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditTask(id, mockedTaskViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNewTaskExpirationDateIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = null;
            mockedTaskViewModel.Object.priority = "1";
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(null, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenNewTaskPriorityIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = string.Empty;
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();


            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditTask(id, mockedTaskViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNewTaskPriorityIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = null;
            mockedTaskViewModel.Object.task = "tasks";

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(null, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenNewTasksTaskIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "2";
            mockedTaskViewModel.Object.task = string.Empty;

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();


            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditTask(id, mockedTaskViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNewTasksTaskIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "2";
            mockedTaskViewModel.Object.task = null;

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            var id = Guid.NewGuid().ToString();
            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditTask(null, It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void CallTaskServiceFindTaskById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "2";
            mockedTaskViewModel.Object.task = "task";

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            //Act
            controller.EditTask(id.ToString(), mockedTaskViewModel.Object);

            //Assert
            mokcedTaskService.Verify(u => u.FindTaskById(id), Times.Once);
        }

        [Test]
        public void CallTaskServiceUpdateTask_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "2";
            mockedTaskViewModel.Object.task = "task";

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.EditTask(id.ToString(), mockedTaskViewModel.Object);

            //Assert
            mokcedTaskService.Verify(u => u.UpdateTask(mockedToDoListTask.Object), Times.Once);
        }

        [Test]
        public void RedirectToIndex()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();
            var mockedToDoListTask = new Mock<ToDoListTask>();

            var id = Guid.NewGuid();
            mockedToDoListTask.Object.Id = id;

            mockedTaskViewModel.Object.category = "2";
            mockedTaskViewModel.Object.expirationDate = "20/05/1993";
            mockedTaskViewModel.Object.priority = "2";
            mockedTaskViewModel.Object.task = "task";

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert

            controller.WithCallTo(c => c.EditTask(id.ToString(), mockedTaskViewModel.Object)).ShouldRedirectTo(r => r.Index(It.IsAny<string>()));


        }

    }
}
