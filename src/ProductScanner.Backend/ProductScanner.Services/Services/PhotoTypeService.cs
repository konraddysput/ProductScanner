using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services.Base;
using ProductScanner.ViewModels.PhotoType;

namespace ProductScanner.Services.Services
{
    public class PhotoTypeService : ServiceBase<PhotoTypeViewModel, PhotoType>, IPhotoTypeService
    {
        public PhotoTypeService(
            IRepository<PhotoType> repository,
            IMapper mapper) : base(repository, mapper)
        { }
    }
}
