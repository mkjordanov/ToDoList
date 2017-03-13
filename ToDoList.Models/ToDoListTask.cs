using System;
using ToDoList.Models.Contracts;

namespace ToDoList.Models
{
    public class ToDoListTask : IToDoListTask
    {
        public Categories Category { get; set; }

        public DateTime ExpirationDate { get; set; }

        public Guid Id { get; set; }

        public Priorities Priority { get; set; }

        public string Task { get; set; }

        public virtual ToDoListModel ToDoList { get; set; }
    }
}
