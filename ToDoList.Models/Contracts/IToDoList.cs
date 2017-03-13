using System;
using System.Collections.Generic;

namespace ToDoList.Models.Contracts
{
    public interface IToDoList
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        ICollection<ToDoListTask> Tasks { get; set; }
    }
}
