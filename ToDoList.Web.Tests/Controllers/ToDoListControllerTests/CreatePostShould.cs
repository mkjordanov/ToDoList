using Moq;
using NUnit.Framework;
using System;
using System.Security.Principal;
using System.Web.Mvc;
using TestStack.FluentMVCTesting;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class CreatePostShould
    {
       
        [Test]
        public void Redirect_WithCorrectParameters()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedToDoListViewModel.Object.IsPublic = true;
            mockedToDoListViewModel.Object.Name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;


            //Act&Assert
            controller.WithCallTo(c => c.Create(mockedToDoListViewModel.Object))
                .ShouldRedirectTo(r => r.ListsAndTasks(It.IsAny<string>()));
        }

        [Test]
        public void UserServiceMethodGetById_IsCalledOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedToDoListViewModel.Object.IsPublic = true;
            mockedToDoListViewModel.Object.Name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act
            controller.Create(mockedToDoListViewModel.Object);

            //Assert
            mokcedUserService.Verify(u => u.GetUserById(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallToDoListModelServiceMethodCreateToDoList_IsCalledOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.Category = CategoryTypes.Entertainment;
            mockedToDoListViewModel.Object.IsPublic = true;
            mockedToDoListViewModel.Object.Name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act
            controller.Create(mockedToDoListViewModel.Object);

            //Assert
            mokcedToDoListModelService.Verify(u => u.CreateToDoList(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<CategoryTypes>()), Times.Once);
        }
    }
}
