using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class DeleteShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Delete(string.Empty); });
        }
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<NullReferenceException>(() => { controller.Create(null); });
        }
        [Test]
        public void CallToDoListModelServiceMethodGetListById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var listId = Guid.NewGuid();
            var user = new ApplicationUser();
            var list = new ToDoListModel() { Id = listId, ApplicationUserId = user };
            mokcedToDoListModelService.Setup(s => s.GetListById(listId)).Returns(list);

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act
            controller.Delete(listId.ToString());

            //Assert
            mokcedToDoListModelService.Verify(u => u.GetListById(listId), Times.Once);
        }

        [Test]
        public void RenderDefaultView()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var listId = Guid.NewGuid();
            var user = new ApplicationUser();
            var list = new ToDoListModel() { Id = listId, ApplicationUserId= user };
            mokcedToDoListModelService.Setup(s => s.GetListById(listId)).Returns(list);

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act & Assert
            controller.WithCallTo(c => c.Delete(listId.ToString())).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultViewWithCorrectModel()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var listId = Guid.NewGuid();
            var user = new ApplicationUser();
            var list = new ToDoListModel()
            {
                Id = listId,
                Category = CategoryTypes.Entertainment,
                Date = DateTime.Now,
                IsPublic = false,
                Name = "SampleName",
                Tasks = new List<ToDoListTask>(),
                ApplicationUserId = user
            };

            mokcedToDoListModelService.Setup(s => s.GetListById(listId)).Returns(list);

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act & Assert
            controller
                .WithCallTo(c => c.Delete(listId.ToString()))
                .ShouldRenderDefaultView()
                .WithModel<ToDoListViewModel>(actualList =>
               {
                   Assert.AreEqual(list.Id, actualList.Id);
                   Assert.AreEqual(list.Category, actualList.Category);
                   Assert.AreEqual(list.Date, actualList.Date);
                   Assert.AreEqual(list.IsPublic, actualList.IsPublic);
                   Assert.AreEqual(list.Name, actualList.Name);
                   Assert.AreEqual(list.Tasks, actualList.Tasks);
               });
        }

    }
}
