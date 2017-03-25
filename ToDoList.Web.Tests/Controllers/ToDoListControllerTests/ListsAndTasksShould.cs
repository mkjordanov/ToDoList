using Moq;
using NUnit.Framework;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class ListsAndTasksShould
    {
        [Test]
        public void UserServiceMethodGetById_IsCalledOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var user = new ApplicationUser();

            mokcedUserService.Setup(s => s.GetUserById(It.IsAny<string>())).Returns(user);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act
            controller.ListsAndTasks(It.IsAny<string>());

            //Assert
            mokcedUserService.Verify(u => u.GetUserById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RenderDefaultView()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var user = new ApplicationUser();

            mokcedUserService.Setup(s => s.GetUserById(It.IsAny<string>())).Returns(user);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller.WithCallTo(c => c.ListsAndTasks(It.IsAny<string>())).ShouldRenderDefaultView();
        }

        [Test]
        public void RenderDefaultView_WithCorrectModel()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var user = new ApplicationUser();

            mokcedUserService.Setup(s => s.GetUserById(It.IsAny<string>())).Returns(user);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act & Assert
            controller.WithCallTo(c => c.ListsAndTasks(It.IsAny<string>())).ShouldRenderDefaultView().WithModel(user.ToDoLists);
        }
        //[Test]
        //public void GetUserIdFromUrl_WhenLoggedAsAdmin()
        //{
        //    //Arrange
        //    var mokcedToDoListModelService = new Mock<IToDoListModelService>();
        //    var mokcedUserService = new Mock<IUserService>();

        //    var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

        //    var id = Guid.NewGuid();
        //    var user = new ApplicationUser();
        //    user.Id = id.ToString();
        //    mokcedUserService.Setup(s => s.GetUserById(id.ToString())).Returns(user);

        //    var controllerContext = new Mock<ControllerContext>();

        //    var principal = new Mock<IPrincipal>();
        //    principal.Setup(p => p.IsInRole("Admin")).Returns(true);

        //    controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);

        //    controller.ControllerContext = controllerContext.Object;

        //    //Act
        //    //controller.ListsAndTasks(id.ToString());

        //    //Act & Assert
        //    controller.WithCallTo(c => c.ListsAndTasks(id.ToString()))
        //        .ShouldRenderDefaultView()
        //        .WithModel<ToDoListModel>(actual =>
        //        {
        //            Assert.AreSame(user.ToDoLists, actual);
        //        });
        //}
    }
}
