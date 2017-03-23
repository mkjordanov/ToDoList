using Moq;
using NUnit.Framework;
using TestStack.FluentMVCTesting;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class CreateGetShould
    {
        [Test]
        public void Render_DefulatView()
        {
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();
            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            controller.WithCallTo(c => c.Create()).ShouldRenderDefaultView();
        }
    }
}
