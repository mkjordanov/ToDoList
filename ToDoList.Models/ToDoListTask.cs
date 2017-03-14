using System;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models.Contracts;
using ToDoList.Models.Enums;

namespace ToDoList.Models
{
    public class ToDoListTask : IToDoListTask
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Task { get; set; }

        [Required]
        public CategoryTypes Category { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public PriorityTypes Priority { get; set; }

        [Required]
        public bool IsDone { get; set; }

        public virtual ToDoListModel ToDoList { get; set; }
    }
}
