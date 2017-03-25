using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Web.Areas.User.Models
{
    public class UserViewModel
    {
        [MaxLength(10)]
        public string FirstName { get; set; }
        [MaxLength(10)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(12)]
        public string UserName{ get; set; }
    }
}