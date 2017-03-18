using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;

namespace ToDoList.Services
{
    public class ToDoListModelService : IToDoListModelService
    {
        private readonly IEFGenericRepository<ToDoListModel> toDoListModelRepository;
        private readonly IEFGenericRepository<ApplicationUser> userService;
        private readonly IUnitOfWork unitOfWork;

        public ToDoListModelService(IEFGenericRepository<ToDoListModel> toDoListModelService, IEFGenericRepository<ApplicationUser> userService, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(toDoListModelService, "ToDoListModelService").IsNull().Throw();
            Guard.WhenArgument(userService, "userService").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of Work").IsNull().Throw();

            this.toDoListModelRepository = toDoListModelService;
            this.userService = userService;
            this.unitOfWork = unitOfWork;
        }
        public void CreateToDoList(object userId, string name, bool isPublic, CategoryTypes category=CategoryTypes.General)
        {
            Guard.WhenArgument(userId, "userId").IsNull().Throw();
            Guard.WhenArgument(name, "name").IsNullOrEmpty().Throw();

            var listToBeAdded = new ToDoListModel()
            {
                Name = name,
                IsPublic = isPublic,
                Category = category,
                Date = DateTime.Now
            };

            var user = userService.GetById(userId);
            user.ToDoLists.Add(listToBeAdded);
            unitOfWork.Commit();
        }

        public ToDoListModel GetListById(object id)
        {
           return this.toDoListModelRepository.GetById(id);
        }
        public void DeleteToDoList(object ToDoListId)
        {
            Guard.WhenArgument(ToDoListId, "ToDoListId").IsNull().Throw();

            var listToBeDeleted=this.toDoListModelRepository.GetById(ToDoListId);
            this.toDoListModelRepository.Delete(listToBeDeleted);
            unitOfWork.Commit();
        }

        public IQueryable<ToDoListModel> GetAll()
        {
            return this.toDoListModelRepository.All;
        }

        public IQueryable<ToDoListModel> GetAllByUser(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();
            return this.GetAll().Where(l => l.ApplicationUserId == id);
        }

        public void UpdateToDoList(ToDoListModel toDoList)
        {
            this.toDoListModelRepository.Update(toDoList);
            unitOfWork.Commit();
        }
    }
}
