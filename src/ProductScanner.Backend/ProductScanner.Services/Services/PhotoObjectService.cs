using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductScanner.Services.Services
{
    public class PhotoObjectService : IPhotoObjectService
    {
        private readonly IRepository<PhotoObject> _repository;
        public PhotoObjectService(IRepository<PhotoObject> repository)
        {
            _repository = repository;
        }
        public Task<PhotoObject> Add(PhotoObject @object)
        {
            return _repository.Add(@object);
        }

        public async Task<IEnumerable<PhotoObject>> Add(IEnumerable<PhotoObject> objects)
        {
            var result = new List<PhotoObject>();
            foreach (var @object in objects)
            {
                result.Add(await Add(@object));
            }
            return result;
        }

        public async Task SaveChanges()
        {
            await _repository.SaveChanges();
        }
    }
}
