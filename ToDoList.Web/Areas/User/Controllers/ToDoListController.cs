using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using ToDoList.Services.Contracts;
using Microsoft.AspNet.Identity;
using ToDoList.Models.Enums;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Areas.User.Controllers
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
            Guard.WhenArgument(newList.category, "Categoty").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newList.name, "Categoty").IsNullOrEmpty().Throw();

            var selectedCategory = Enum.Parse(typeof(CategoryTypes), newList.category);
            var userId = User.Identity.GetUserId();
            var currentUser = this.userService.GetUserById(userId);

            this.toDoListModelService.CreateToDoList(currentUser, newList.name, newList.isPublic, (CategoryTypes)selectedCategory);

            return RedirectToAction("ListsAndTasks", "ToDoList");
        }

        [HttpGet]
        public ActionResult ListsAndTasks(string id)
        {
            string userId = null;

            if (User.IsInRole("Admin"))
            {
                userId = id;
            }
            else
            {
                userId = User.Identity.GetUserId();
            }
            var currentUser = this.userService.GetUserById(userId);
            return this.View(currentUser.ToDoLists);
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var list = this.toDoListModelService.GetListById(Guid.Parse(id));
            return this.View(list);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteList(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var listToBeDeleted = this.toDoListModelService.GetListById(Guid.Parse(id));
            this.toDoListModelService.DeleteToDoList(listToBeDeleted);
            return RedirectToAction("ListsAndTasks", "ToDoList");
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var list = this.toDoListModelService.GetListById(Guid.Parse(id));
            return this.View(list);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditList(string id, ToDoListViewModel editList)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editList, "editList").IsNull().Throw();
            Guard.WhenArgument(editList.name, "editList.name").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editList.category, "editList.category").IsNullOrEmpty().Throw();

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