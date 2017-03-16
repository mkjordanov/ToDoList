using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Services.Contracts;

namespace ToDoList.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;
        public HomeController(IUserService userService)
        {
            Guard.WhenArgument(userService, "User Service").IsNull().Throw();
            this.userService = userService;
        }
        public ActionResult Index()
        {
            var allUsers=this.userService.GetAllUsers().ToList();
            return View(allUsers);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}