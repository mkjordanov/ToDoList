﻿using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.UserService
{
    [TestFixture]
    public class UpdateUserShould
    {
        [Test]
        public void Throw_WhenTaskIsNull()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act & Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                userService.UpdateUser(null);
            });
        }
        [Test]
        public void CallRepositorysUpdate_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUser = new Mock<ApplicationUser>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.UpdateUser(mockedUser.Object);

            //Assert
            mockedUserRepository.Verify(r => r.Update(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Test]
        public void CallUnitOfWorksCommit_OnlyOnce()
        {
            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedUser = new Mock<ApplicationUser>();

            var userService = new ToDoList.Services.UserService(mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act
            userService.UpdateUser(mockedUser.Object);

            //Assert
            mockedUnitOfWork.Verify(r => r.Commit(), Times.Once);
        }
    }
}
