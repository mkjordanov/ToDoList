using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.UserService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenUserRepositoryIsNull()
        {
            //Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoList.Services.UserService(null,mockedUnitOfWork.Object);
            });
        }

        [Test]
        public void Throw_WhenUnitOfWorkIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoList.Services.UserService(mockedUserRepository.Object, null);
            });
        }
    }
}
