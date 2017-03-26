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
    public class EditShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Edit(string.Empty); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Edit(null); });
        }

        [Test]
        public void CallTaskServiceFindTaskById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var taskId = Guid.NewGuid();
            var list = new ToDoListModel();
            var task = new ToDoListTask() { Id = taskId, ToDoList = list };
            mokcedTaskService.Setup(s => s.FindTaskById(taskId)).Returns(task);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act
            controller.Edit(taskId.ToString());

            //Assert
            mokcedTaskService.Verify(u => u.FindTaskById(taskId), Times.Once);
        }

        [Test]
        public void ReturnDefaultView()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var taskId = Guid.NewGuid();
            var list = new ToDoListModel();
            var task = new ToDoListTask() { Id = taskId, ToDoList = list };
            mokcedTaskService.Setup(s => s.FindTaskById(taskId)).Returns(task);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Edit(taskId.ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void ReturnDefaultView_WithCorrectModel()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var taskId = Guid.NewGuid();
            var list = new ToDoListModel();
            var task = new ToDoListTask()
            {
                Id = taskId,
                Task = "sample name",
                Category = CategoryTypes.Entertainment,
                ExpirationDate = DateTime.Now.AddDays(1),
                IsDone = false,
                Priority=PriorityTypes.High,
                ToDoList = list
            };
            mokcedTaskService.Setup(s => s.FindTaskById(taskId)).Returns(task);

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            controller.WithCallTo(c => c.Edit(taskId.ToString()))
                .ShouldRenderDefaultView()
                .WithModel<TaskViewModel>(actualTask =>
                {
                    Assert.AreEqual(task.Id, actualTask.Id);
                    Assert.AreEqual(task.Task, actualTask.Task);
                    Assert.AreEqual(task.IsDone, actualTask.IsDone);
                    Assert.AreEqual(task.Priority, actualTask.Priority);
                    Assert.AreEqual(task.ExpirationDate, actualTask.ExpirationDate);
                    Assert.AreEqual(task.Category, actualTask.Category);
                });
        }
    }
}
