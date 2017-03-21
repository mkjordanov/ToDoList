using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
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
        public void Throw_WhenCategoryIsEmppty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = string.Empty;
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Create(mockedToDoListViewModel.Object); });
        }
        [Test]
        public void Throw_WhenCategoryIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = null;
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Create(mockedToDoListViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNameIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = "3";
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = null;

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Create(mockedToDoListViewModel.Object); });
        }

        [Test]
        public void Throw_WhenNameIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = "3";
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = string.Empty;

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Create(mockedToDoListViewModel.Object); });
        }

        [Test]
        public void Redirect_WithCorrectParameters()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = "3";
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = "Name";

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;


            //Act&Assert
            controller.WithCallTo(c => c.Create(mockedToDoListViewModel.Object))
                .ShouldRedirectTo(r => r.ListsAndTasks());
        }

        [Test]
        public void UserServiceMethodGetById_IsCalledOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var mockedToDoListViewModel = new Mock<ToDoListViewModel>();
            mockedToDoListViewModel.Object.category = "3";
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = "Name";

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
            mockedToDoListViewModel.Object.category = "3";
            mockedToDoListViewModel.Object.isPublic = true;
            mockedToDoListViewModel.Object.name = "Name";

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
