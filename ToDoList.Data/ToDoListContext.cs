using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ToDoList.Models;

namespace ToDoList.Data
{
    public class ToDoListContext : IdentityDbContext<ApplicationUser>, IToDoListContext
    {
        public ToDoListContext() : base("ToDoListContext")
        {
        }
        public virtual IDbSet<ToDoListTask> Tasks { get; set; }

        public virtual IDbSet<ToDoListModel> ToDoLists { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
