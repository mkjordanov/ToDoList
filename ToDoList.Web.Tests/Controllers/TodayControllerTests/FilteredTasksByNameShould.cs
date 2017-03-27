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
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TodayControllerTests
{
    [TestFixture]
    public class FilteredTasksByNameShould
    {
        //[Test]
        //public void ReturnIndex_WhenSearchThmerIsEmpty()
        //{
        //    //Arrange
        //    var mockedToDoListTaskService = new Mock<IToDoListTaskService>();

        //    var listId = Guid.NewGuid();

        //    var controller = new TodayController(mockedToDoListTaskService.Object);

        //    controller.WithCallTo(c => c.FilteredTasksByName(string.Empty)).ShouldRenderView("Index");

           
        //}
    }
}
