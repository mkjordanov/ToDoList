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
    public  class FindTaskByIdShould
    {
        [Test]
        public void Throw_WheTaskIdIsNull()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act & Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                taskService.FindTaskById(null);
            });
        }

        [Test]
        public void CallRepositoriesGetById_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act
            taskService.FindTaskById(Guid.NewGuid());

            //Assert
            mockedToDoListTaskRepository.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void ReturnsCorrectType_WhenGivenValidParameters()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedToDoListTaskRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(new ToDoListTask());

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act
            var actualResult=taskService.FindTaskById(Guid.NewGuid());

            //Assert
            Assert.That(actualResult, Is.InstanceOf<ToDoListTask>());
        }
        [Test]

        public void Retrun_WheGiveValidParameters()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var expectedTask = new ToDoListTask() {Task="sample task" };

            mockedToDoListTaskRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(expectedTask);

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUnitOfWork.Object);

            //Act
            var actualResult = taskService.FindTaskById(Guid.NewGuid());

            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedTask));

        }
    }
}
