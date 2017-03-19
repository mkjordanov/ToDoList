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
    }
}