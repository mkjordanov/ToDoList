using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.UserService
{
    [TestFixture]
    public class GetUserByIdShould
    {
        [Test]
        public void Throw_WhenUserIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                userService.GetUserById(null);
            });
        }

        [Test]
        public void CallRepositoryGetById_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);
            var id = Guid.NewGuid();
            //Act
            userService.GetUserById(id);

            //Assert
            mockedUserRepository.Verify(r => r.GetById(id), Times.Once);
        }

        [Test]
        public void ReturnCorrectResult_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var exprectedUser = new ApplicationUser();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            var id = Guid.NewGuid();
            exprectedUser.Id = id.ToString();

            mockedUserRepository.Setup(r => r.GetById(id)).Returns(exprectedUser);
            //Act
            var actualUser= userService.GetUserById(id);

            //Assert
            Assert.That(exprectedUser, Is.EqualTo(actualUser));
        }

        [Test]
        public void ReturnCorrectResultType_WhenCalled()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var exprectedUser = new ApplicationUser();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            var id = Guid.NewGuid();
            exprectedUser.Id = id.ToString();

            mockedUserRepository.Setup(r => r.GetById(id)).Returns(exprectedUser);
            //Act
            var actualUser = userService.GetUserById(id);

            //Assert
            Assert.That(actualUser, Is.InstanceOf<ApplicationUser>());
        }

    }
}
