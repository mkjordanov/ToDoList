using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Services.Contracts;
using ToDoList.Web.Areas.User.Controllers;

namespace ToDoList.Web.Tests.Controllers.TodayControllerTests
{
    [TestFixture]
    public class ConstructorShould
    {
        [Test]
        public void Throw_WhenToDoListTaskServiceIsNull()
        {
            //Arrange&Act&Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new TodayController(null);
            });
        }
    }
}
