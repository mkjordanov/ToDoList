using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Models.Enums;
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

        //[Test]
        //public void Throw_WhenExpirationDateIsOlderThanCurrentDay()
        //{
        //    //Arrange
        //    var mokcedToDoListModelService = new Mock<IToDoListModelService>();
        //    var mokcedTaskService = new Mock<IToDoListTaskService>();
        //    var mockedTaskViewModel = new Mock<TaskViewModel>();

        //    mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
        //    mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
        //    mockedTaskViewModel.Object.Priority = PriorityTypes.High;
        //    mockedTaskViewModel.Object.Task =null;

        //    var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
        //    var id = Guid.NewGuid().ToString();
        //    controller.ModelState.
        //    //Act&Assert
        //    Assert.Throws<ArgumentNullException>(() => { controller.EditTask(id, mockedTaskViewModel.Object); });
        //}

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

            mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
            mockedTaskViewModel.Object.Priority = PriorityTypes.High;
            mockedTaskViewModel.Object.Task = "task";

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

            mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
            mockedTaskViewModel.Object.Priority = PriorityTypes.High;
            mockedTaskViewModel.Object.Task = "task";

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

            mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
            mockedTaskViewModel.Object.Priority = PriorityTypes.High;
            mockedTaskViewModel.Object.Task = "task";

            mokcedTaskService.Setup(s => s.FindTaskById(id)).Returns(mockedToDoListTask.Object);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert

            controller.WithCallTo(c => c.EditTask(id.ToString(), mockedTaskViewModel.Object)).ShouldRedirectTo(r => r.Index(It.IsAny<string>()));


        }

    }
}
