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

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class GetAllShould
    {
        [Test]
        public void CallRepositoryAll_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            toDoListModelService.GetAll();

            //Assert
            mockedToDoListModelRepository.Verify(x => x.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            var expectedCollection = new List<ToDoListModel>()
            {
                new ToDoListModel() {Name="Sample List" }
            };

            mockedToDoListModelRepository.Setup(r => r.All).Returns(() =>
            {
                return expectedCollection.AsQueryable();
            });
            //Act
            var actualResult=toDoListModelService.GetAll();

            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedCollection));

        }

        [Test]
        public void ReturnCorrectType_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            var expectedCollection = new List<ToDoListModel>()
            {
                new ToDoListModel() {Name="Sample List" }
            };

            mockedToDoListModelRepository.Setup(r => r.All).Returns(() =>
            {
                return expectedCollection.AsQueryable();
            });
            //Act
            var actualResult = toDoListModelService.GetAll();

            //Assert
            Assert.That(actualResult, Is.InstanceOf<IEnumerable<ToDoListModel>>());

        }

        [Test]
        public void ReturnNull_WhenCalledAndCollectionIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            mockedToDoListModelRepository.Setup(r => r.All).Returns(() =>
            {
                return null;
            });
            //Act
            var actualResult = toDoListModelService.GetAll();

            //Assert
            Assert.IsNull(actualResult);

        }
    }
}
