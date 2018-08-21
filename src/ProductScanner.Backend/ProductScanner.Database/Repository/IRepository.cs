using ProductScanner.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Database.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        IQueryable<T> Get();
        T Get(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
