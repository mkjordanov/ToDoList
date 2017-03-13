using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
