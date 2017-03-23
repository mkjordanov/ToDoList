using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class UpdateToDoListShould
    {
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                toDoListModelService.UpdateToDoList(null);
            });
        }

        [Test]
        public void CallUnitOfWorkCommit_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoList = new Mock<ToDoListModel>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            toDoListModelService.UpdateToDoList(mockedToDoList.Object);

            //Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }

        [Test]
        public void CallRepositoryUpdate_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoList = new Mock<ToDoListModel>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            toDoListModelService.UpdateToDoList(mockedToDoList.Object);

            //Assert
            mockedToDoListModelRepository.Verify(x => x.Update(It.IsAny<ToDoListModel>()), Times.Once);
        }
    }
}
