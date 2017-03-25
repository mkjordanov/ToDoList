using Moq;
using NUnit.Framework;
using System;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class EditListSould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.EditList(string.Empty,It.IsAny<ToDoListViewModel>()); });
        }
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.EditList(null, It.IsAny<ToDoListViewModel>()); });
        }
      
        [Test]
        public void CallToDoListModelServiceMethodGetListById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedList = new Mock<ToDoListModel>();
            
            var id = Guid.NewGuid();

            mockedList.Object.Id = id;
            mokcedToDoListModelService.Setup(s => s.GetListById(id)).Returns(mockedList.Object);

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);
            var listmodel = new ToDoListViewModel() { Name = "name", IsPublic = true, Category = CategoryTypes.Entertainment};
            
            //Act
            controller.EditList(id.ToString(), listmodel);
            
            //Assert
            mokcedToDoListModelService.Verify(s => s.GetListById(id), Times.Once);
        }
        [Test]
        public void CallToDoListModelServiceMethodUpdateToDoList_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedList = new Mock<ToDoListModel>();

            var id = Guid.NewGuid();

            mockedList.Object.Id = id;
            mokcedToDoListModelService.Setup(s => s.GetListById(id)).Returns(mockedList.Object);

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);
            var listmodel = new ToDoListViewModel() { Name = "name", IsPublic = true, Category = CategoryTypes.Entertainment };

            //Act&Assert
            controller.WithCallTo(c => c.EditList(id.ToString(), listmodel)).ShouldRedirectTo(r => r.ListsAndTasks(It.IsAny<string>()));
        }


    }
}
