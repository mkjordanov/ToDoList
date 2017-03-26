using Bytes2you.Validation;
using System;
using System.Linq;
using System.Web.Mvc;
using ToDoList.Services.Contracts;
using ToDoList.Web.Models.TaskViewModels;

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

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var user = this.userSerivce.GetUserById(id);
            return this.View(user);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditUsers(string id, UserViewModel editUser)
        {
            if (!ModelState.IsValid)
            {
                return this.View(editUser);
            }
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var userToBeEdited = this.userSerivce.GetUserById(id);

            userToBeEdited.FirstName = editUser.FirstName;
            userToBeEdited.LastName= editUser.LastName;
            userToBeEdited.Email= editUser.Email;
            userToBeEdited.UserName= editUser.UserName;

            this.userSerivce.UpdateUser(userToBeEdited);

            return RedirectToAction("Index", "Users");
        }
    }
}