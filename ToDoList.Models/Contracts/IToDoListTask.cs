using System;
using ToDoList.Models.Enums;

namespace ToDoList.Models.Contracts
{
    public interface IToDoListTask
    {
        Guid Id { get; set; }
        string Task { get; set; }
        ToDoListModel ToDoList { get; set; }
        CategoryTypes Category{ get; set; }
        PriorityTypes Priority { get; set; }
        bool IsDone { get; set; }
        DateTime ExpirationDate { get; set; }
    }
}
