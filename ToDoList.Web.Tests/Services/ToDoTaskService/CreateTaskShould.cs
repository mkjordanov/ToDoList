using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services;

namespace ToDoList.Web.Tests.Services.ToDoTaskService
{
    [TestFixture]
    public class CreateTaskShould
    {
        [Test]
        public void Throw_WhenToDoListIsNull()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            var category = CategoryTypes.Errands;
            var priority = PriorityTypes.Low;
            var expirationDate = DateTime.Now.AddDays(1);
            var task = "sample task";

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                taskService.CreateTask(null, category, priority, expirationDate, task);
            });
        }

        [Test]
        public void Throw_WhenExpirationDateIsInThePast()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoListModel = new Mock<ToDoListModel>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            mockedToDoListModel.Setup(l => l.Tasks.Add(It.IsAny<ToDoListTask>()));

            var category = CategoryTypes.Errands;
            var priority = PriorityTypes.Low;
            var expirationDate = DateTime.Parse("10/10/1992");
            var task = "sample task";

            //Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                taskService.CreateTask(mockedToDoListModel.Object, category, priority, expirationDate, task);
            });
        }

        [Test]
        public void CallUnitOfWorkCommit_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoListModel = new Mock<ToDoListModel>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            mockedToDoListModel.Setup(l => l.Tasks.Add(It.IsAny<ToDoListTask>()));

            var category = CategoryTypes.Errands;
            var priority = PriorityTypes.Low;
            var expirationDate = DateTime.Now.AddDays(1);
            var task = "sample task";

            //Act 
            taskService.CreateTask(mockedToDoListModel.Object, category, priority, expirationDate, task);

            // Assert
            mockedUnitOfWork.Verify(u => u.Commit(), Times.Once);
        }
    }
}
