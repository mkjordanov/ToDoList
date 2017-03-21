using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}