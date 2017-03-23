using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.UserService
{
    [TestFixture]
    public class GetAllUsersShould
    {
        [Test]
        public void CallRepositoryAll_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);
            //Act
            userService.GetAllUsers();

            //Assert
            mockedUserRepository.Verify(r => r.All, Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            var expectedCollection = new List<ApplicationUser>()
            {
                new ApplicationUser() {FirstName="Gosho" }
            };

            mockedUserRepository.Setup(r => r.All).Returns(() =>
            {
                return expectedCollection.AsQueryable();
            });

            //Act
            var actualResult = userService.GetAllUsers();

            //Assert
            Assert.That(actualResult, Is.EqualTo(expectedCollection));

        }

        [Test]
        public void ReturnCorrectType_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            var expectedCollection = new List<ApplicationUser>()
            {
                new ApplicationUser() {FirstName="Gosho" }
            };

            mockedUserRepository.Setup(r => r.All).Returns(() =>
            {
                return expectedCollection.AsQueryable();
            });

            //Act
            var actualResult = userService.GetAllUsers();

            //Assert
            Assert.That(actualResult, Is.InstanceOf<IEnumerable<ApplicationUser>>());

        }

        [Test]
        public void ReturnNull_WhenCalledAndCollectionIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            mockedUserRepository.Setup(r => r.All).Returns(() =>
            {
                return null;
            });

            //Act
            var actualResult = userService.GetAllUsers();

            //Assert
            Assert.That(actualResult, Is.EqualTo(null));

        }
    }
}
