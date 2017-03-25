using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Services.Models
{
    public class ListModel
    {
        private ICollection<TaskModel> tasks;
        public ListModel(ToDoListModel dbList)
        {
            this.Id = dbList.Id;
            this.Name = dbList.Name;
            this.Date = dbList.Date;
            this.Category = dbList.Category;
            this.IsPublic = dbList.IsPublic;
            this.ApplicationUserId = dbList.ApplicationUserId.Id;
            this.tasks = dbList.Tasks.Select(t=>new TaskModel(t)).ToList();
        }
        
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DateTime Date { get; set; }
        public CategoryTypes Category { get; set; }
        public bool IsPublic { get; set; }

        public string ApplicationUserId { get; set; }

        public virtual ICollection<TaskModel> Tasks
        {
            get { return this.tasks; }
            set { this.tasks = value; }
        }

    }
}
