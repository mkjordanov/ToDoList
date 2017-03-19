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
        private readonly IEFGenericRepository<ApplicationUser> userRepository;
        private readonly IUnitOfWork unitOfWork;

        public ToDoListModelService(IEFGenericRepository<ToDoListModel> toDoListModelRepository, IEFGenericRepository<ApplicationUser> userRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(toDoListModelRepository, "ToDoListModelRepository").IsNull().Throw();
            Guard.WhenArgument(userRepository, "userRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of Work").IsNull().Throw();

            this.toDoListModelRepository = toDoListModelRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateToDoList(ApplicationUser user, string name, bool isPublic, CategoryTypes category = CategoryTypes.General)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();
            Guard.WhenArgument(name, "name").IsNullOrEmpty().Throw();

            var listToBeAdded = new ToDoListModel()
            {
                Name = name,
                IsPublic = isPublic,
                Category = category,
                Date = DateTime.Now
            };

            user.ToDoLists.Add(listToBeAdded);
            unitOfWork.Commit();
        }

        public ToDoListModel GetListById(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();

            return this.toDoListModelRepository.GetById(id);
        }
        public void DeleteToDoList(ToDoListModel ToDoList)
        {
            Guard.WhenArgument(ToDoList, "ToDoList").IsNull().Throw();

            this.toDoListModelRepository.Delete(ToDoList);
            unitOfWork.Commit();
        }

        public IEnumerable<ToDoListModel> GetAll()
        {
            return this.toDoListModelRepository.All;
        }

        public IEnumerable<ToDoListModel> GetAllByUserId(object id)
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
