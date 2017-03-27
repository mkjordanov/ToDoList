using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Services.Contracts
{
    public interface IToDoListTaskService
    {
        IEnumerable<ToDoListTask> GetAllByUserId(object id);
        void CreateTask(ToDoListModel toDoList, CategoryTypes category, PriorityTypes priority, DateTime expirationDate, string task);
        ToDoListTask FindTaskById(object taskId);
        void DeleteTask(ToDoListTask task);
        void UpdateTask(ToDoListTask task);
        IEnumerable<ToDoListTask> GetTasksByName(string searchTerm, string userId);
    }
}
