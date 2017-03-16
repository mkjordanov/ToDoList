using Bytes2you.Validation;
using System.Data.Entity;

namespace ToDoList.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext context;
        public UnitOfWork(ToDoListContext context)
        {
            Guard.WhenArgument(context, "context").IsNull().Throw();
            this.context = context;
        }
        public void Commit()
        {
            this.context.SaveChanges();
        }

    }
}
