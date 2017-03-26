using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class TaskViewModel
    {
        public TaskViewModel() {}
        public TaskViewModel(ToDoListTask dbTasks)
        {
            this.Id = dbTasks.Id;
            this.Task = dbTasks.Task;
            this.Category = dbTasks.Category;
            this.ExpirationDate = dbTasks.ExpirationDate;
            this.Priority = dbTasks.Priority;
            this.IsDone = dbTasks.IsDone;
            this.listId = dbTasks.ToDoList.Id;
        }

        [Required]
        [MaxLength(100)]
        public string Task { get; set; }

        [Required]
        public CategoryTypes Category { get; set; }
        [Required]
        public PriorityTypes Priority { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
        public Guid Id { get;  set; }
        public bool IsDone { get;  set; }
        public Guid listId { get;  set; }
    }
}