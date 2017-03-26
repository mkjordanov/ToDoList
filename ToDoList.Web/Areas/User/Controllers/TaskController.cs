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
            var mappedList = new ToDoListViewModel(selectedList);
            return this.View(mappedList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public ActionResult Create(string id, TaskViewModel newTask)
        {
            if (!ModelState.IsValid)
            {
                 return this.View(newTask);
            }
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();
            Guard.WhenArgument(newTask.ExpirationDate, "newTask.expirationDate").IsLessThan(DateTime.Today).Throw();

            var usedList = this.toDoListModelService.GetListById(Guid.Parse(id));

            this.taskService.CreateTask(usedList, newTask.Category, newTask.Priority, newTask.ExpirationDate, newTask.Task);
            return RedirectToAction("Index", "Task", new { id = id });
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var task = this.taskService.FindTaskById(Guid.Parse(id));
            var mappedTask = new TaskViewModel(task);
            return this.View(mappedTask);
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
            var mappedTask = new TaskViewModel(task);
            return this.View(mappedTask);
        }

        [HttpPost]
        [ActionName("Edit")]
        public ActionResult EditTask(string id, TaskViewModel editTask)
        {
            if (!ModelState.IsValid)
            {
                return this.View(editTask);
            }
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();
            Guard.WhenArgument(editTask.ExpirationDate, "editTask.expirationDate").IsLessThan(DateTime.Today).Throw();

            var listId = TempData["ListId"];
            var taskToBeEdited = this.taskService.FindTaskById(Guid.Parse(id));

            taskToBeEdited.Task = editTask.Task;
            taskToBeEdited.Category = editTask.Category;
            taskToBeEdited.Priority =  editTask.Priority;
            taskToBeEdited.ExpirationDate= editTask.ExpirationDate;

            this.taskService.UpdateTask(taskToBeEdited);

            return RedirectToAction("Index", "Task", new { id = listId });
        }

        [HttpPost]
        public ActionResult Done(string id)
        {
            Guard.WhenArgument(id, "id").IsNullOrEmpty().Throw();

            var listId = TempData["ListId"];

            var task = this.taskService.FindTaskById(Guid.Parse(id));
            task.IsDone = !task.IsDone;
            this.taskService.UpdateTask(task);

            return RedirectToAction("Index", "Task", new { id = listId });

        }
    }
}