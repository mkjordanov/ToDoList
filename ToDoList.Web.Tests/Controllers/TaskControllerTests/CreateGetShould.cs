using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestStack.FluentMVCTesting;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TaskControllerTests
{
    [TestFixture]
    class CreateGetShould
    {
        [Test]
        public void ReturnDefaultView()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedTaskService = new Mock<IToDoListTaskService>();

            var controller = new TaskController(mokcedToDoListModelService.Object, mokcedTaskService.Object);
            
            //Act&Assert
            controller.WithCallTo(c => c.Create()).ShouldRenderDefaultView();
        }
    }
}
