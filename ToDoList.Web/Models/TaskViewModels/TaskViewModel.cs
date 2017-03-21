using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models.Enums;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class TaskViewModel
    {
        [Required]
        [MaxLength(100, ErrorMessage ="Task must not be longer than 100 characters")]
        public string task { get; set; }

        [Required]
        public string category { get; set; }
        [Required]
        public string priority { get; set; }

        [Required]
        public string expirationDate { get; set; }
    }
}