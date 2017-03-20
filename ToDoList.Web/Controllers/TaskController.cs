using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;

namespace ToDoList.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly IToDoListModelService toDoListModelService;
        private readonly IToDoListTaskService taskService;
        public TaskController(IToDoListModelService toDoListModelService, IToDoListTaskService taskService)
        {
            Guard.WhenArgument(toDoListModelService, "To-Do ListModel Service").IsNull().Throw();
            Guard.WhenArgument(taskService, "Task Service").IsNull().Throw();

            this.toDoListModelService = toDoListModelService;
            this.taskService = taskService;
        }

        [HttpGet]
        public ActionResult Index(string id)
        {
            TempData["ListId"] = id;
            var selectedList = this.toDoListModelService.GetListById(Guid.Parse(id));
            return this.View(selectedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(string id, string task, string category, string priority, string expirationDate)
        {
            var usedList = this.toDoListModelService.GetListById(Guid.Parse(id));

            var taskCategory = Enum.Parse(typeof(CategoryTypes), category);
            var taskPriority = Enum.Parse(typeof(PriorityTypes), priority);
            var taskExpirationDate = DateTime.Parse(expirationDate);

            this.taskService.CreateTask(usedList, (CategoryTypes)taskCategory, (PriorityTypes)taskPriority, taskExpirationDate, task);
            return RedirectToAction("Index", "Task", new { id = id });
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            var task = this.taskService.FindTaskById(Guid.Parse(id));

            return this.View(task);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteTask(string id)
        {
            var listId = TempData["ListId"];
            var taskToBeDeleted = this.taskService.FindTaskById(Guid.Parse(id));
            this.taskService.DeleteTask(taskToBeDeleted);

            return RedirectToAction("Index", "Task", new { id = listId });
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            var task = this.taskService.FindTaskById(Guid.Parse(id));
            return this.View(task);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditTask(string id, string task, string category, string priority, string expirationDate)
        {
            var listId = TempData["ListId"];
            var taskToBeEdited = this.taskService.FindTaskById(Guid.Parse(id));

            taskToBeEdited.Task = task;
            taskToBeEdited.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), category);
            taskToBeEdited.Priority = (PriorityTypes)Enum.Parse(typeof(PriorityTypes), priority);
            taskToBeEdited.ExpirationDate= DateTime.Parse(expirationDate);

            this.taskService.UpdateTask(taskToBeEdited);

            return RedirectToAction("Index", "Task", new { id = listId });
        }

        [HttpPost]
        public ActionResult Done(string id)
        {
            var listId = TempData["ListId"];

            var task = this.taskService.FindTaskById(Guid.Parse(id));
            task.IsDone = !task.IsDone;
            this.taskService.UpdateTask(task);

            return RedirectToAction("Index", "Task", new { id = listId });

        }
    }
}