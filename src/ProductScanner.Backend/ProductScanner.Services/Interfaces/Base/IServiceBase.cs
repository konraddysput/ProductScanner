using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductScanner.Services.Interfaces.Base
{
    public interface IServiceBase<VM, M>
    {
        Task<VM> Get(int id);
        IEnumerable<VM> Get();
        Task Update(VM viewModel);
        Task Delete(int id);
        Task<VM> Create(VM viewModel);
        Task<bool> Any(Expression<Func<M, bool>> predicate);
        Task<bool> Any(int id);

        Task SaveChanges();
    }
}
