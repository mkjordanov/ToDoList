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
        [MaxLength(100, ErrorMessage = "Task must not be longer than 100 characters")]
        public string Task { get; set; }

        [Required]
        public CategoryTypes Category { get; set; }
        [Required]
        public PriorityTypes Priority { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
    }
}