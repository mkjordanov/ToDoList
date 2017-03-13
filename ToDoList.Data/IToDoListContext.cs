using System.Data.Entity;
using ToDoList.Models;

namespace ToDoList.Data
{
    public interface IToDoListContext
    {
        IDbSet<ToDoListModel> ToDoLists { get; set; }
        IDbSet<ToDoListTask> Tasks{ get; set; }
    }
}
