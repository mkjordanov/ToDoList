using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class CreateToDoListShould
    {
        [Test]
        public void Throw_WhenUserIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);
            var name = "sample name";
            var isPublic = false;

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                toDoListModelService.CreateToDoList(null, name, isPublic);
            });
        }

        [Test]
        public void Throw_WhenNameIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUser = new Mock<ApplicationUser>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);
            var isPublic = false;

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                toDoListModelService.CreateToDoList(mockedUser.Object, null, isPublic);
            });
        }

        [Test]
        public void Throw_WhenNameIsEmpty()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUser = new Mock<ApplicationUser>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);
            var name = string.Empty;
            var isPublic = false;

            //Act&Assert
            Assert.Throws<ArgumentException>(() =>
            {
                toDoListModelService.CreateToDoList(mockedUser.Object, name, isPublic);
            });
        }

        [Test]
        public void CallUnitOfWorkCommitOnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUser = new Mock<ApplicationUser>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);
            var name = "Sample Name";
            var isPublic = false;

            mockedUser.Setup(x => x.ToDoLists.Add(It.IsAny<ToDoListModel>()));
            //Act
            toDoListModelService.CreateToDoList(mockedUser.Object, name, isPublic);
            
            //Assert
            mockedUnitOfWork.Verify(x => x.Commit(), Times.Once);
        }
    }
}
