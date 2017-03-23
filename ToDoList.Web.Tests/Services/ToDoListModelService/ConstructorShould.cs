using Moq;
using NUnit.Framework;
using System;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;

namespace ToDoList.Web.Tests.Services.ToDoListModelService
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenToDoListModelRepositoryIsNull()
        {
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            Assert.Throws<ArgumentNullException>(() => { new ToDoList.Services.ToDoListModelService(null, mockedUserRepository.Object, mockedUnitOfWork.Object); });
        }

        [Test]
        public void Throw_WhenUnitOfWorkIsNull()
        {
            var mockedUserRepository = new Mock<IEFGenericRepository<ApplicationUser>>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
            new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, mockedUserRepository.Object, null);
            });
        }
        [Test]
        public void Throw_WhenUserRepositoryIsNull()
        {
            var mockedUnitOfWork = new Mock<IUnitOfWork>();
            var mockedToDoListModelRepository = new Mock<IEFGenericRepository<ToDoListModel>>();

            Assert.Throws<ArgumentNullException>(() =>
            {
                new ToDoList.Services.ToDoListModelService(mockedToDoListModelRepository.Object, null, mockedUnitOfWork.Object);
            });
        }
    }
}
