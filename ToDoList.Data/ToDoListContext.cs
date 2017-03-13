using System.Data.Entity;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoListContext : DbContext, IToDoListContext
    {
        public ToDoListContext() : base("ToDoListContext")
        {
        }
        public virtual IDbSet<ToDoListTask> Tasks { get; set; }

        public virtual IDbSet<ToDoListModel> ToDoLists { get; set; }
    }
}
