﻿using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Services.Models;

namespace ToDoList.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<ApplicationUser> GetAllUsers();
        ApplicationUser GetUserById(object id);
        void DeleteUser(ApplicationUser user);
        void UpdateUser(ApplicationUser user);
    }
}
