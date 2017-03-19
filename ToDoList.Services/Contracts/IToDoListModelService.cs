using System.Collections.Generic;
using System.Linq;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Services.Contracts
{
    public interface IToDoListModelService
    {
        IEnumerable<ToDoListModel> GetAll();

        IEnumerable<ToDoListModel> GetAllByUserId(object id);
        ToDoListModel GetListById(object id);
       
        void CreateToDoList(ApplicationUser user, string name, bool isPublic, CategoryTypes category = CategoryTypes.General);

        void DeleteToDoList(ToDoListModel toDoList);

        void UpdateToDoList(ToDoListModel toDoList);


    }
}
