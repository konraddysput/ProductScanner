using AutoMapper;
using ProductScanner.Database.Entities.Base;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces.Base;
using ProductScanner.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Services.Services.Base
{
    public class ServiceBase<VM, M> : IServiceBase<VM, M> where M : EntityBase where VM : ViewModelBase
    {
        protected readonly IRepository<M> Repository;
        protected readonly IMapper Mapper;

        public ServiceBase(
            IRepository<M> repository,
            IMapper mapper)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public virtual async Task<VM> Create(VM viewModel)
        {
            var model = Mapper.Map<M>(viewModel);
            var result = await Repository.Add(model);
            return Mapper.Map<VM>(result);
        }

        public virtual async Task Delete(int id)
        {
            var model = await Repository.Get(id);
            Repository.Delete(model);
        }

        public virtual async Task Update(VM viewModel)
        {
            var mappedModel = Mapper.Map<M>(viewModel);
            var entity = await Repository.Get(viewModel.Id);
            Mapper.Map(mappedModel, entity);
            entity.Id = viewModel.Id;
            Repository.Update(entity);
        }

        public virtual IEnumerable<VM> Get()
        {
            var result = Repository.Get().AsEnumerable();
            return Mapper.Map<IEnumerable<VM>>(result);
        }

        public virtual async Task<VM> Get(int id)
        {
            var result = await Repository.Get(id);
            return Mapper.Map<VM>(result);
        }

        public virtual async Task SaveChanges()
        {
            await Repository.SaveChanges();
        }
    }
}
