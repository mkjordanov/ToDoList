using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Services.Contracts;
using Microsoft.AspNet.Identity;
using ToDoList.Models.Enums;

namespace ToDoList.Web.Controllers
{
    [Authorize]
    public class ToDoListController : Controller
    {
        private readonly IToDoListModelService toDoListModelService;
        private readonly IUserService userService;
        public ToDoListController(IToDoListModelService toDoListModelService, IUserService userService)
        {
            Guard.WhenArgument(toDoListModelService, "To-Do List service").IsNull().Throw();
            Guard.WhenArgument(userService, "User service").IsNull().Throw();

            this.toDoListModelService = toDoListModelService;
            this.userService = userService;
        }
        [HttpGet]
        public ActionResult Create()
        {
            var categoryList = new List<SelectListItem>()
            {
                new SelectListItem { Text = "General", Value = "General", Selected=true},
                new SelectListItem { Text = "Personal", Value = "Personal"},
                new SelectListItem { Text = "Shopping", Value = "Shopping"},
                new SelectListItem { Text = "Work", Value = "Work"},
                new SelectListItem { Text = "Errands", Value = "Errands"},
                new SelectListItem { Text = "Entertainment", Value = "Entertainment"},
                new SelectListItem { Text = "Hobbies", Value = "Hobbies"}
            };
           
            return View(categoryList);
        }
        [HttpPost]
        public ActionResult Create(string name, bool isPublic, string category )
        {
            var selectedCategory = Enum.Parse(typeof(CategoryTypes), category);
            var userId = User.Identity.GetUserId();

            this.toDoListModelService.CreateToDoList(userId, name, isPublic,(CategoryTypes)selectedCategory);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult ListsAndTasks()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.userService.GetUserById(userId);
            //var currentUserLists = this.toDoListModelService.GetAllByUser(userId).ToList();
            return this.View(currentUser.ToDoLists);
        }

        //[HttpGet]
        //public ActionResult Tasks(string id)
        //{
        //    var selectedList=this.toDoListModelService.GetListById(Guid.Parse(id));
        //    var selectedListTasks = selectedList.Tasks;
        //    return this.View(selectedListTasks);
        //}
    }
}