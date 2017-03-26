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
        private readonly IEFGenericRepository<ToDoListTask> toDoListTaskRepository;
        private readonly IEFGenericRepository<ApplicationUser> userRepository;
        private readonly IUnitOfWork unitOfWork;
        public ToDoListTaskService(IEFGenericRepository<ToDoListTask> toDoListTaskRepository, IEFGenericRepository<ApplicationUser> userRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(toDoListTaskRepository, "To-Do-List-Task Repository").IsNull().Throw();
            Guard.WhenArgument(userRepository, "userRepository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of work").IsNull().Throw();

            this.toDoListTaskRepository = toDoListTaskRepository;
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public void CreateTask(ToDoListModel toDoList, CategoryTypes category, PriorityTypes priority, DateTime expirationDate, string task)
        {
            Guard.WhenArgument(toDoList, "To-Do List").IsNull().Throw();
            Guard.WhenArgument(expirationDate, "Expiration Date").IsLessThan(DateTime.Now.Date).Throw();
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
            Guard.WhenArgument(task, "task").IsNull().Throw();

            this.toDoListTaskRepository.Delete(task);
            this.unitOfWork.Commit();
        }

        public ToDoListTask FindTaskById(object taskId)
        {
            Guard.WhenArgument(taskId, "Task id").IsNull().Throw();

            return this.toDoListTaskRepository.GetById(taskId);
        }

        public IEnumerable<ToDoListTask> GetAllByUserId(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();

            var userLists = this.userRepository.GetById(id).ToDoLists;
            var tasks = userLists.SelectMany(l => l.Tasks.Where(t => t.ExpirationDate == DateTime.Now.Date));
            return tasks.ToList();
        }

        public IEnumerable<ToDoListTask> GetAllByUserAndCategory(object id, CategoryTypes category)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ToDoListTask> GetAllByUserAndPriority(object id, PriorityTypes priority)
        {
            throw new NotImplementedException();
        }


        public void UpdateTask(ToDoListTask task)
        {
            Guard.WhenArgument(task, "task").IsNull().Throw();

            this.toDoListTaskRepository.Update(task);
            this.unitOfWork.Commit();
        }
    }
}
