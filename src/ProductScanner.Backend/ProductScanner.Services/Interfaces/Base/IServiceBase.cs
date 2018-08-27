using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductScanner.Services.Interfaces.Base
{
    public interface IServiceBase<VM, M>
    {
        VM Get(int id);
        IEnumerable<VM> Get();
        void Update(VM viewModel);
        void Delete(VM viewModel);
        Task<VM> Create(VM viewModel);
        Task SaveChanges();
    }
}
