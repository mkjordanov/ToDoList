using Moq;
using NUnit.Framework;
using System;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    public class CreatePostShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Create(string.Empty,It.IsAny<TaskViewModel>()); });
        }

        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Create(null, It.IsAny<TaskViewModel>()); });
        }

       

        //[Test]
        //public void Throw_WhenNewTaskExpirationDateIsEmpty()
        //{
        //    //Arrange
        //    var mokcedToDoListModelService = new Mock<IToDoListModelService>();
        //    var mokcedTaskService = new Mock<IToDoListTaskService>();
        //    var mockedTaskViewModel = new Mock<TaskViewModel>();

        //    mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
        //    mockedTaskViewModel.Object.ExpirationDate = string.Empty;
        //    mockedTaskViewModel.Object.Priority = PriorityTypes.High;
        //    mockedTaskViewModel.Object.Task = "tasks";

        //    var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
        //    var id = Guid.NewGuid().ToString();


        //    //Act&Assert
        //    Assert.Throws<ArgumentException>(() => { controller.Create(id, mockedTaskViewModel.Object); });
        //}

        //[Test]
        //public void Throw_WhenNewTaskExpirationDateIsNull()
        //{
        //    //Arrange
        //    var mokcedToDoListModelService = new Mock<IToDoListModelService>();
        //    var mokcedTaskService = new Mock<IToDoListTaskService>();
        //    var mockedTaskViewModel = new Mock<TaskViewModel>();

        //    mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
        //    mockedTaskViewModel.Object.ExpirationDate = null;
        //    mockedTaskViewModel.Object.Priority = PriorityTypes.High;
        //    mockedTaskViewModel.Object.Task = "tasks";

        //    var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
        //    var id = Guid.NewGuid().ToString();
        //    //Act&Assert
        //    Assert.Throws<ArgumentNullException>(() => { controller.Create(null, It.IsAny<TaskViewModel>()); });
        //}


        

        [Test]
        public void CallToDoListModelServiceMethodGetListById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
            mockedTaskViewModel.Object.Priority = PriorityTypes.High;
            mockedTaskViewModel.Object.Task = "Task";

            //Act
            controller.Create(Guid.NewGuid().ToString(), mockedTaskViewModel.Object);

            //Assert
            mokcedToDoListModelService.Verify(u => u.GetListById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void CallToDoListModelServiceCreateTask_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();
            var mockedTaskViewModel = new Mock<TaskViewModel>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);

            mockedTaskViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedTaskViewModel.Object.ExpirationDate = DateTime.Now.AddDays(1);
            mockedTaskViewModel.Object.Priority = PriorityTypes.High;
            mockedTaskViewModel.Object.Task = "Task";

            //Act
            controller.Create(Guid.NewGuid().ToString(), mockedTaskViewModel.Object);

            //Assert
            mokcedTaskService.Verify(u => u.CreateTask(It.IsAny<ToDoListModel>(), It.IsAny<CategoryTypes>(), It.IsAny<PriorityTypes>(), It.IsAny<DateTime>(), It.IsAny<string>()), Times.Once);
        }

    }

}
