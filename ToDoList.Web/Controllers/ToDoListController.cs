using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Services.Contracts;
using Microsoft.AspNet.Identity;
using ToDoList.Models.Enums;
using ToDoList.Web.Models.TaskViewModels;

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
            return View();
        }

        [HttpPost]
        public ActionResult Create(ToDoListViewModel newList)
        {
            var selectedCategory = Enum.Parse(typeof(CategoryTypes), newList.category);
            var userId = User.Identity.GetUserId();
            var currentUser = this.userService.GetUserById(userId);

            this.toDoListModelService.CreateToDoList(currentUser, newList.name, newList.isPublic, (CategoryTypes)selectedCategory);

            return RedirectToAction("ListsAndTasks", "ToDoList");
        }

        [HttpGet]
        public ActionResult ListsAndTasks()
        {
            var userId = User.Identity.GetUserId();
            var currentUser = this.userService.GetUserById(userId);
            return this.View(currentUser.ToDoLists);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var list = this.toDoListModelService.GetListById(Guid.Parse(id));
            return this.View(list);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteList(string id)
        {
            var listToBeDeleted = this.toDoListModelService.GetListById(Guid.Parse(id));
            this.toDoListModelService.DeleteToDoList(listToBeDeleted);
            return RedirectToAction("ListsAndTasks", "ToDoList");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            var list = this.toDoListModelService.GetListById(Guid.Parse(id));
            return this.View(list);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditList(string id, ToDoListViewModel editList)
        {
            var listToBeEdited = this.toDoListModelService.GetListById(Guid.Parse(id));

            listToBeEdited.Name = editList.name;
            listToBeEdited.IsPublic = editList.isPublic;
            listToBeEdited.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), editList.category);
            listToBeEdited.Date = DateTime.Now;

            this.toDoListModelService.UpdateToDoList(listToBeEdited);

            return RedirectToAction("ListsAndTasks", "ToDoList");
        }
    }
}