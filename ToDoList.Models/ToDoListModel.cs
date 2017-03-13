using System;
using System.Collections.Generic;
using ToDoList.Models.Contracts;

namespace ToDoList.Models
{
    public class ToDoListModel : IToDoList
    {
        public ToDoListModel()
        {
            Tasks = new HashSet<ToDoListTask>();
        }
        public Guid Id { get; set; }
        public DateTime Date {get; set;}
        public virtual ApplicationUser ApplicationUserId{ get; set; }
        public virtual ICollection<ToDoListTask> Tasks { get; set; }
    }
}
