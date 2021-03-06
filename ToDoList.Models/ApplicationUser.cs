﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoList.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            ToDoLists = new HashSet<ToDoListModel>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<ToDoListModel> ToDoLists { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
