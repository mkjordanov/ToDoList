using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ToDoList.Models.Enums;

namespace ToDoList.Web.Models.TaskViewModels
{
    public class ToDoListViewModel
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public bool IsPublic { get; set; }
        [Required]
        public CategoryTypes Category { get; set; }
    }
}