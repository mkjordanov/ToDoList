using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;

namespace ToDoList.Services
{
    public class ToDoListViewModelService : IToDoListModelService
    {
        private readonly IEFGenericRepository<ToDoListModel> toDoListModelService;
        private readonly IEFGenericRepository<ApplicationUser> userService;
        private readonly IUnitOfWork unitOfWork;

        public ToDoListViewModelService(IEFGenericRepository<ToDoListModel> toDoListModelService, IEFGenericRepository<ApplicationUser> userService, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(toDoListModelService, "ToDoListModelService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of Work").IsNull().Throw();

            this.toDoListModelService = toDoListModelService;
            this.userService = userService;
            this.unitOfWork = unitOfWork;
        }
        public void CreateToDoList(object userId, string name, bool isPublic, CategoryTypes category)
        {
            Guard.WhenArgument(userId, "userId").IsNull().Throw();
            Guard.WhenArgument(name, "name").IsNullOrEmpty().Throw();

            var listToBeAdded = new ToDoListModel()
            {
                Name = name,
                IsPublic = isPublic,
                Category = category
            };

            var user = userService.GetById(userId);
            user.ToDoLists.Add(listToBeAdded);
        }

        public void DeleteToDoList(object ToDoListId)
        {
            Guard.WhenArgument(ToDoListId, "ToDoListId").IsNull().Throw();

            var listToBeDeleted=this.toDoListModelService.GetById(ToDoListId);
            this.toDoListModelService.Delete(listToBeDeleted);
        }

        public IEnumerable<ToDoListModel> GetAll()
        {
            return this.toDoListModelService.GetAll();
        }

        public IEnumerable<ToDoListModel> GetAllByUser(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();
            return this.GetAll().Where(l => l.ApplicationUserId == id);
        }

        public void UpdateToDoList(ToDoListModel toDoList)
        {
            this.toDoListModelService.Update(toDoList);
        }
    }
}
