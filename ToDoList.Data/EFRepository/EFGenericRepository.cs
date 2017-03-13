using Bytes2you.Validation;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ToDoList.Data.EFRepository
{
    public class EFGenericRepository<T> : IEFGenericRepository<T> where T : class
    {
        public EFGenericRepository(ToDoListContext context)
        {
            Guard.WhenArgument(context, "Context").IsNull().Throw();
            this.Context = context;
            this.DbSet = context.Set<T>();
        }
        protected DbContext Context { get; set; }
        protected DbSet<T> DbSet { get; set; }
        public void Add(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(object id)
        {
            Guard.WhenArgument(id, "id").IsNull().Throw();
            return this.DbSet.Find(id);
        }

        public void Update(T entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            var entry = this.Context.Entry(entity);
            entry.State = EntityState.Modified;
        }
        
    }
}
