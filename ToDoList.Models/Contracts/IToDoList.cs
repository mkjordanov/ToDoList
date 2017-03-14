using System;
using System.Collections.Generic;
using ToDoList.Models.Enums;

namespace ToDoList.Models.Contracts
{
    public interface IToDoList
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        string Name { get; set; }
        bool IsPublic { get; set; }
        CategoryTypes Category { get; set; }
        ICollection<ToDoListTask> Tasks { get; set; }
    }
}
