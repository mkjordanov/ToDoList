using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class ToDoListViewModel
    {

        [Required]
        [MaxLength(100, ErrorMessage = "List's name must be less than 100 characters")]
        public string name { get; set; }
        [Required]
        public bool isPublic { get; set; }
        [Required]
        public string category { get; set; }
    }
}