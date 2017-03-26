using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Web.Tests.Services.ToDoTaskService
{
    [TestFixture]
    public class DeleteTaskShould
    {
        [Test]
        public void Throw_WhenTaskIsNull()
        {

            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);


            //Act & Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                taskService.DeleteTask(null);
            });

        }

        [Test]
        public void CallRepositoryDelete_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoTask = new Mock<ToDoListTask>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);


            //Act
            taskService.DeleteTask(mockedToDoTask.Object);

            //Assert
            mockedToDoListTaskRepository.Verify(r => r.Delete(It.IsAny<ToDoListTask>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorkCommit_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoTask = new Mock<ToDoListTask>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            taskService.DeleteTask(mockedToDoTask.Object);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }
    }
}
