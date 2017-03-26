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
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenToDoListTaskRepositoryIsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();


            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListTaskService(null,mockedUserRepository.Object, mockedUnitOfWork.Object);
            });
        }

        [Test]
        public void Throw_WhenUnitOfWorkIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListTaskService(mockedToDoListTaskRepository.Object, mockedUserRepository.Object,null);
            });
        }

        [Test]
        public void Throw_WhenUserRepositoryIsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListTaskService(mockedToDoListTaskRepository.Object, null, mockedUnitOfWork.Object);
            });
        }
    }
}
