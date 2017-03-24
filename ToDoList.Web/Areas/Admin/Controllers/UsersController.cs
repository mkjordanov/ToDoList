using Bytes2you.Validation;
using System;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Services.Contracts;

namespace ToDoList.Web.Areas.Admin.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService userSerivce;
        public UsersController(IUserService userSerivce)
        {
            Guard.WhenArgument(userSerivce, "UserService").IsNull().Throw();

            this.userSerivce = userSerivce;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var users = this.userSerivce.GetAllUsers().ToList();
            return View(users);
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var user = this.userSerivce.GetUserById(id);

            return this.View(user);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteUser(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var userToBeDeleted = this.userSerivce.GetUserById(id);
            this.userSerivce.DeleteUser(userToBeDeleted);

            return RedirectToAction("Index", "Users");
        }
    }
}