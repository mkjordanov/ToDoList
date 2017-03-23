using NUnit.Framework;
using System;
using ToDoList.Web.Areas.Admin.Controllers;

namespace ToDoList.Web.Tests.Controllers.UserControllerTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenUserSerivceIsNull()
        {
            //Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new UsersController(null);
            });
        }
    }
}
