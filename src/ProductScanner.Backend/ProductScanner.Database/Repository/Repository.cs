using Microsoft.EntityFrameworkCore;
using ProductScanner.Database.Entities.Base;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductScanner.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ProductScannerDbContext _productScannerDbContext;
        public Repository(ProductScannerDbContext productScannerDbContext)
        {
            _productScannerDbContext = productScannerDbContext;
        }

        public async Task Add(T entity)
        {
            await _productScannerDbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _productScannerDbContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _productScannerDbContext.Set<T>();
        }

        public T Get(int id)
        {
            return _productScannerDbContext.Set<T>().FirstOrDefault(n => n.Id == id);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _productScannerDbContext.Set<T>().Where(predicate);
        }

        public void Update(T entity)
        {
            _productScannerDbContext.Entry(entity).State = EntityState.Modified;
            _productScannerDbContext.Set<T>().Update(entity);
        }
    }

}
