using System;
using ToDoList.Models;
using ToDoList.Models.Enums;
using System.Linq;

namespace ToDoList.Services.Models
{
    public class TaskModel
    {
        public TaskModel(ToDoListTask dbTask)
        {
            this.Id = dbTask.Id;
            this.Task = dbTask.Task;
            this.Category = dbTask.Category;
            this.ExpirationDate = dbTask.ExpirationDate;
            this.Priority = dbTask.Priority;
            this.IsDone = dbTask.IsDone;
            this.listId = dbTask.ToDoList.Id;

        }
        public Guid Id { get; set; }

        public string Task { get; set; }

        public CategoryTypes Category { get; set; }

        public DateTime ExpirationDate { get; set; }

        public PriorityTypes Priority { get; set; }

        public bool IsDone { get; set; }

        public Guid listId{ get; set; }
    }
}