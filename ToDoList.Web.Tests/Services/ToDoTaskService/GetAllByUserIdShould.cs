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
    public class GetAllByUserIdShould
    {
        [Test]
        public void Throw_WheIdIsNull()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                taskService.GetAllByUserId(null);
            });
        }
        [Test]
        public void CallRepositoriesGetById_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);
            var userId = Guid.NewGuid();

            mockedUserRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(new ApplicationUser());

            //Act
            taskService.GetAllByUserId(userId);

            //Assert
            mockedUserRepository.Verify(r => r.GetById(userId), Times.Once);
        }

        [Test]
        public void ReturnsCorrectType_WhenGivenValidParameters()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userId = Guid.NewGuid();

            mockedUserRepository.Setup(r => r.GetById(userId)).Returns(new ApplicationUser());

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            var actualResult = taskService.GetAllByUserId(userId);

            //Assert
            Assert.That(actualResult, Is.InstanceOf<List<ToDoListTask>>());
        }

        [Test]

        public void Retrun_WhenGiveValidParameters()
        { //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userId = Guid.NewGuid();

            var tasks = new List<ToDoListTask>()
            {
                new ToDoListTask() { Task="task 1", ExpirationDate=DateTime.Now.Date},
                new ToDoListTask() { Task="task 2", ExpirationDate=DateTime.Now.Date},
                new ToDoListTask() { Task="task 3", ExpirationDate=DateTime.Now.Date}
            };

            var list = new ToDoListModel() { Tasks=tasks};

            var lists = new List<ToDoListModel>();
            lists.Add(list);

            var user = new ApplicationUser()
            {
                Id = userId.ToString(),
                ToDoLists = lists
            };

            mockedUserRepository.Setup(r => r.GetById(userId)).Returns(user);

            var taskService = new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            var actualResult = taskService.GetAllByUserId(userId);

            //Assert
            Assert.AreEqual(tasks, actualResult);

        }
    }
}
