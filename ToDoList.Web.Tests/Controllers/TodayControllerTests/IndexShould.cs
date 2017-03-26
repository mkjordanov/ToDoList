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
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Tests.Controllers.TodayControllerTests
{
    [TestFixture]
    public class IndexShould
    {
        [Test]
        public void CallToDoListModelServiceMethodGetListById_OnlyOnce()
        {
            //Arrange
            var mockedToDoListTaskService = new Mock<IToDoListTaskService>();

            var listId = Guid.NewGuid();

            var controller = new TodayController(mockedToDoListTaskService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act
            controller.Index();

            //Assert
            mockedToDoListTaskService.Verify(u => u.GetAllByUserId(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void RenderDefaultView()
        {
            //Arrange
            var mockedToDoListTaskService = new Mock<IToDoListTaskService>();

            var listId = Guid.NewGuid();
            var tasks = new List<ToDoListTask>();

            mockedToDoListTaskService.Setup(s => s.GetAllByUserId(It.IsAny<string>())).Returns(tasks);
            var controller = new TodayController(mockedToDoListTaskService.Object);

            var controllerContext = new Mock<ControllerContext>();
            var principal = new Mock<IPrincipal>();
            principal.SetupGet(x => x.Identity.Name).Returns("Username");
            controllerContext.SetupGet(x => x.HttpContext.User).Returns(principal.Object);
            controller.ControllerContext = controllerContext.Object;

            //Act&Assert
            controller.WithCallTo(c => c.Index()).ShouldRenderDefaultView();

        }
    }
}
