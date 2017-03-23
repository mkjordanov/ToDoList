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

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListTaskService(null, mockedUnitOfWork.Object);
            });
        }

        [Test]
        public void Throw_WhenUnitOfWorkIsNull()
        {
            //Arrange
            var mockedToDoListTaskRepository = new Mock<IEFGenericRepository<ToDoListTask>>();
            
            //Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoListTaskService(mockedToDoListTaskRepository.Object, null);
            });
        }
    }
}
