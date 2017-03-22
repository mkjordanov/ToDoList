using Bytes2you.Validation;
using System;
using System.Web.Mvc;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Areas.User.Controllers
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
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

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
        public ActionResult Create(string id, TaskViewModel newTask)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newTask, "newTask").IsNull().Throw();
            Guard.WhenArgument(newTask.category, "newTask.category").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newTask.expirationDate, "newTask.expirationDate").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newTask.priority, "newTask.priority").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newTask.task, "newTask.task").IsNullOrEmpty().Throw();

            var usedList = this.toDoListModelService.GetListById(Guid.Parse(id));

            var taskCategory = Enum.Parse(typeof(CategoryTypes), newTask.category);
            var taskPriority = Enum.Parse(typeof(PriorityTypes), newTask.priority);
            var taskExpirationDate = DateTime.Parse(newTask.expirationDate);

            this.taskService.CreateTask(usedList, (CategoryTypes)taskCategory, (PriorityTypes)taskPriority, taskExpirationDate, newTask.task);
            return RedirectToAction("Index", "Task", new { id = id });
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var task = this.taskService.FindTaskById(Guid.Parse(id));

            return this.View(task);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteTask(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var listId = TempData["ListId"];
            var taskToBeDeleted = this.taskService.FindTaskById(Guid.Parse(id));
            this.taskService.DeleteTask(taskToBeDeleted);

            return RedirectToAction("Index", "Task", new { id = listId });
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var task = this.taskService.FindTaskById(Guid.Parse(id));

            return this.View(task);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditTask(string id, TaskViewModel editTask)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editTask, "editTask").IsNull().Throw();
            Guard.WhenArgument(editTask.category, "editTask.category").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editTask.expirationDate, "editTask.expirationDate").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editTask.priority, "editTask.priority").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editTask.task, "editTask.task").IsNullOrEmpty().Throw();

            var listId = TempData["ListId"];
            var taskToBeEdited = this.taskService.FindTaskById(Guid.Parse(id));

            taskToBeEdited.Task = editTask.task;
            taskToBeEdited.Category = (CategoryTypes)Enum.Parse(typeof(CategoryTypes), editTask.category);
            taskToBeEdited.Priority = (PriorityTypes)Enum.Parse(typeof(PriorityTypes), editTask.priority);
            taskToBeEdited.ExpirationDate= DateTime.Parse(editTask.expirationDate);

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