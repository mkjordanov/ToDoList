using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class UserViewModel
    {
        private ICollection<ToDoListViewModel> list;
        public UserViewModel() {}
        public UserViewModel(ApplicationUser dbUser)
        {
            this.FirstName = dbUser.FirstName;
            this.LastName = dbUser.LastName;
            this.UserName = dbUser.UserName;
            this.Email = dbUser.Email;
            this.Lists = dbUser.ToDoLists.Select(lists => new ToDoListViewModel(lists)).ToList();
        }
        public string Id { get; set;}

        [MaxLength(10)]
        public string FirstName { get; set; }
        [MaxLength(10)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(12)]
        public string UserName { get; set; }

        public ICollection<ToDoListViewModel> Lists
        {
            get { return this.list; }
            set { this.list = value; }
        }
    }
}