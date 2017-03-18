using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Models.Contracts;
using ToDoList.Models.Enums;
using ToDoList.Services.Contracts;

namespace ToDoList.Services
{
    public class ToDoListTaskService : IToDoListTaskService
    {
        private readonly IEFGenericRepository<IToDoListTask> toDoListTaskRepository;
        private readonly IUnitOfWork unitOfWork;
        public ToDoListTaskService(IEFGenericRepository<IToDoListTask> toDoListTaskRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(toDoListTaskRepository, "To-Do ListTask Repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of work").IsNull().Throw();

            this.toDoListTaskRepository = toDoListTaskRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateTask(ToDoListModel toDoList, CategoryTypes category, PriorityTypes priority, DateTime expirationDate, string task)
        {
            Guard.WhenArgument(toDoList, "To-Do List").IsNull().Throw();
            Guard.WhenArgument(expirationDate, "Expiration Date").IsLessThan(DateTime.Now).Throw();
            Guard.WhenArgument(task, "Task").IsNullOrEmpty().Throw();


            var taskToBeAdded = new ToDoListTask()
            {
                Task = task,
                ExpirationDate = expirationDate,
                Category = category,
                Priority = priority
            };

            toDoList.Tasks.Add(taskToBeAdded);

            unitOfWork.Commit();
        }

        public void DeleteTask(ToDoListTask task)
        {
            throw new NotImplementedException();
        }

        public ToDoListTask FindTaskById(object taskId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ToDoListTask> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<ToDoListTask> GetAllByUserAndCategory(object id, CategoryTypes category)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ToDoListTask> GetAllByUserAndPriority(object id, PriorityTypes priority)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ToDoListTask> GetAllByUserId(object id)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(ToDoListTask task)
        {
            throw new NotImplementedException();
        }
    }
}
