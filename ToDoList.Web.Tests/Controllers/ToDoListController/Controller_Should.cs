using NUnit.Framework;
using Moq;
using ToDoList.Data.UnitOfWork;
using TestStack.FluentMVCTesting;
using ToDoList.Services.Contracts;

namespace ToDoList.Web.Tests.Controllers
{
    [TestFixture]
    public class ToDoListController
    {
       [Test]
       public void Throw_WhenToDoListModelServiceIsNull()
        {
            //var mockedToDoListModelService = new Mock<IToDoListModelService>();
            //var mokcedUserService = new Mock<IUserService>();
            //var controller = new ToDoListController(mockedToDoListModelService.Object, mokcedUserService.Object);
            //controller.WithCallTo()
            //Assert.IsTrue(true);
        }
       
    }
}
