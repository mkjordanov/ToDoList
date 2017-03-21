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

namespace ToDoList.Web.Tests.Controllers.ToDoListControllerTests
{
    [TestFixture]
    public class EditShould
    {
        [Test]
        public void Throw_WhenIdIsEmpty()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentException>(() => { controller.Edit(string.Empty); });
        }
        [Test]
        public void Throw_WhenIdIsNull()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act&Assert
            Assert.Throws<ArgumentNullException>(() => { controller.Edit(null); });
        }

        [Test]
        public void CallToDoListModelServiceMethodGetListById_OnlyOnce()
        {
            //Arrange
            var mokcedToDoListModelService = new Mock<IToDoListModelService>();
            var mokcedUserService = new Mock<IUserService>();

            var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

            //Act
            controller.Edit(Guid.NewGuid().ToString());

            //Assert
            mokcedToDoListModelService.Verify(u => u.GetListById(It.IsAny<Guid>()), Times.Once);
        }

        //[Test]
        //public void RedirectToListsAndTasks()
        //{
        //    //Arrange
        //    var mokcedToDoListModelService = new Mock<IToDoListModelService>();
        //    var mokcedUserService = new Mock<IUserService>();

        //    var controller = new ToDoListController(mokcedToDoListModelService.Object, mokcedUserService.Object);

        //    //Act&Assert
            
        //}
    }
}
