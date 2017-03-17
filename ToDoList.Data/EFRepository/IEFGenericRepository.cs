using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Data.EFRepository
{
    public interface IEFGenericRepository<T> where T:class
    {
        IQueryable<T> All { get; }
        T GetById(object id);

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);
    }
}
