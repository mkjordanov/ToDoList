using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class ToDoListViewModel
    {
        private ICollection<TaskViewModel> tasks;

        public ToDoListViewModel() { }
        public ToDoListViewModel(ToDoListModel dbLists)
        {
            this.Id = dbLists.Id;
            this.Name = dbLists.Name;
            this.Date = dbLists.Date;
            this.Category = dbLists.Category;
            this.IsPublic = dbLists.IsPublic;
            this.ApplicationUserId = dbLists.ApplicationUserId.Id;
            this.Tasks = dbLists.Tasks.Select(tasks => new TaskViewModel(tasks)).ToList();
        }
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public CategoryTypes Category { get; set; }
        public DateTime Date { get;  set; }
        public string ApplicationUserId { get;  set; }
        public ICollection<TaskViewModel> Tasks
        {
            get { return this.tasks; }
            set { this.tasks = value; }
        }
    }
}