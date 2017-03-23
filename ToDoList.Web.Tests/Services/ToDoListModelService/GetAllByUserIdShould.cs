using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class GetAllByUserIdShould
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
                toDoListModelService.GetAllByUserId(null);
            });
        }
        [Test]
        public void ReturnCorrectResult_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var mockedUserOne = new Mock<ApplicationUser>();
            mockedUserOne.Setup(s => s.Id).Returns("1");

            var mockedUserTwo = new Mock<ApplicationUser>();
            mockedUserOne.Setup(s => s.Id).Returns("2");

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            var expectedCollection = new List<ToDoListModel>()
            {
                new ToDoListModel() {Name="Sample List", ApplicationUserId= mockedUserOne.Object },
                new ToDoListModel() {Name="Sample List 1", ApplicationUserId= mockedUserOne.Object },
                new ToDoListModel() {Name="Sample List 2", ApplicationUserId= mockedUserTwo.Object },
                new ToDoListModel() {Name="Sample List 3", ApplicationUserId= mockedUserTwo.Object },
                new ToDoListModel() {Name="Sample List 4", ApplicationUserId= mockedUserTwo.Object }
            };

            mockedToDoListModelRepository.Setup(r => r.All).Returns(() =>
            {
                return expectedCollection.Where(l=>l.ApplicationUserId== mockedUserOne.Object).AsQueryable();
            });

            //Act
            var actualResult = toDoListModelService.GetAllByUserId(mockedUserOne.Object);

            //Assert
            Assert.That(actualResult.Count(), Is.EqualTo(2));

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
            var actualResult = toDoListModelService.GetAllByUserId(It.IsAny<Guid>());

            //Assert
            Assert.That(actualResult, Is.InstanceOf<IEnumerable<ToDoListModel>>());

        }
    }
}
