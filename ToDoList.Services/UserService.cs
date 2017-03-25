using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using ToDoList.Data.EFRepository;
using ToDoList.Data.UnitOfWork;
using ToDoList.Models;
using ToDoList.Services.Contracts;
using ToDoList.Services.Models;

namespace ToDoList.Services
{
    public class UserService : IUserService
    {
        private readonly IEFGenericRepository<ApplicationUser> userRepository;
        private readonly IUnitOfWork unitOfWork;
        public UserService(IEFGenericRepository<ApplicationUser> userRepository, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(userRepository, "User repository").IsNull().Throw();
            Guard.WhenArgument(unitOfWork, "Unit of work").IsNull().Throw();

            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
        }
        public void DeleteUser(ApplicationUser user)
        {
            Guard.WhenArgument(user, "User").IsNull().Throw();
            this.userRepository.Delete(user);
            this.unitOfWork.Commit();
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return this.userRepository.All;
        }

        public ApplicationUser GetUserById(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();
            var searchedUser = this.userRepository.GetById(id);
            //var mappedUser = new UserModel(searchedUser);
            return searchedUser;
        }

        public void UpdateUser(ApplicationUser user)
        {
            Guard.WhenArgument(user, "user").IsNull().Throw();

            this.userRepository.Update(user);
            this.unitOfWork.Commit();
        }
    }
}
