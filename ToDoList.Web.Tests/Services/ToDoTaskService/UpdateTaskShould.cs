using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Services;

namespace ToDoList.Web.Tests.Services.ToDoTaskService
{
    [TestFixture]
    public class UpdateTaskShould
    {
        [Test]
        public void Throw_WhenTaskIsNull()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act & Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                taskService.UpdateTask(null);
            });
        }
        [Test]
        public void CallRepositorysUpdate_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedTask = new Mock<ToDoListTask>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act
            taskService.UpdateTask(mockedTask.Object);

            //Assert
            mockedToDoListTaskRepository.Verify(r => r.Update(It.IsAny<ToDoListTask>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorksCommit_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedTask = new Mock<ToDoListTask>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act
            taskService.UpdateTask(mockedTask.Object);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }
    }
}
