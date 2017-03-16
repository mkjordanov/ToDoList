using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Models.Enums;

namespace ToDoList.Services.Contracts
{
    public interface IToDoListModelService
    {
        IEnumerable<ToDoListModel> GetAll();

        IEnumerable<ToDoListModel> GetAllByUser(object id);

        void CreateToDoList(object userId, string name, bool isPublic, CategoryTypes category);

        void DeleteToDoList(object toDoListId);

        void UpdateToDoList(ToDoListModel toDoList);


    }
}
