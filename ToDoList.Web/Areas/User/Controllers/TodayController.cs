using Bytes2you.Validation;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Services.Contracts;
using ToDoList.Web.Models.TaskViewModels;

namespace ToDoList.Web.Areas.User.Controllers
{
    [Authorize]
    public class TodayController : Controller
    {
        private readonly IToDoListTaskService taskService;
        public TodayController(IToDoListTaskService taskService)
        {
            Guard.WhenArgument(taskService, "taskService").IsNull().Throw();
            this.taskService = taskService;
        }
        public ActionResult Index()
        {
            var currentUserId= User.Identity.GetUserId();
            var allTasksOfUser = this.taskService.GetAllByUserId(currentUserId);
            var mappedTasks = allTasksOfUser.Select(t => new TaskViewModel(t));
            return View(mappedTasks);
        }
    }
}