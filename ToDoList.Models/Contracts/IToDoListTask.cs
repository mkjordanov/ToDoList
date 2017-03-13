using System;

namespace ToDoList.Models.Contracts
{
    public interface IToDoListTask
    {
        Guid Id { get; set; }
        string Task { get; set; }
        ToDoListModel ToDoList { get; set; }
        Categories Category{ get; set; }
        Priorities Priority { get; set; }
        DateTime ExpirationDate { get; set; }
    }
}
