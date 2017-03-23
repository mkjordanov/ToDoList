using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class GetListByIdShould
    {
        [Test]
        public void Thrown_WhenIdIsNull()
        {

            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            //Act & Assert

            Assert.Throws<ArgumentNullException>(() =>
            {
                toDoListModelService.GetListById(null);
            });

        }

        [Test]
        public void CallRepositortGetById_OnlyOnce()
        {

            //Arrange
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            var toDoListModelService = new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, mockedUnitOfWork.Object);

            var obj = "sample object";
            //Act
            toDoListModelService.GetListById(obj);
            //Assert
            mockedToDoListModelRepository.Verify(r => r.GetById(It.IsAny<object>()), Times.Once);



        }
    }
}
