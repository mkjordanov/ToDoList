using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Services.Contracts
{
    public interface IToDoListModelService
    {
        IEnumerable<ToDoListModel> GetAll();

        IEnumerable<ToDoListModel> GetAllByUser(object id);

        void CreateToDoList(object userId, string name, bool isPublic, CategoryTypes category = CategoryTypes.General);

        void DeleteToDoList(object toDoListId);

        void UpdateToDoList(ToDoListModel toDoList);


    }
}
