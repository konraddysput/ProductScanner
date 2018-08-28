using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services.Base;
using ProductScanner.ViewModels.PhotoObject;

namespace ProductScanner.Services.Services
{
    public class PhotoObjectService : ServiceBase<PhotoObjectViewModel, PhotoObject>, IPhotoObjectService
    {
        public PhotoObjectService(
            IRepository<PhotoObject> repository,
            IMapper mapper) : base(repository, mapper)
        { }
    }
}
