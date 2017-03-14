using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ToDoList.Models.Contracts;
using ToDoList.Models.Enums;

namespace ToDoList.Models
{
    public class ToDoListModel : IToDoList
    {
        private ICollection<ToDoListTask> tasks;
      
        public ToDoListModel()
        {
            this.tasks = new HashSet<ToDoListTask>();
        }

        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required]
        public CategoryTypes Category { get; set; }
        [Required]
        public bool IsPublic { get; set; }

        public virtual ApplicationUser ApplicationUserId { get; set; }

        public virtual ICollection<ToDoListTask> Tasks
        {
            get { return this.tasks; }
            set { this.tasks = value; }
        }

        
    }
}
