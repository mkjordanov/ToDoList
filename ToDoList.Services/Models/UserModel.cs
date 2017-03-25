using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services.Models
{
    public class UserModel
    {
        private ICollection<ListModel> list;
        public UserModel(ApplicationUser dbUser)
        {
            this.FirstName = dbUser.FirstName;
            this.LastName = dbUser.LastName;
            this.UserName = dbUser.UserName;
            this.Email = dbUser.Email;
            this.Lists = dbUser.ToDoLists.Select(l => new ListModel(l)).ToList();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public ICollection<ListModel> Lists
        {
            get { return this.list; }
            set { this.list = value; }
        }
    }
}
