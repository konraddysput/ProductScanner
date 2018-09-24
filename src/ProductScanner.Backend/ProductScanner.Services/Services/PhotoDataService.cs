using AutoMapper;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services.Base;
using ProductScanner.ViewModels.PhotoData;

namespace ProductScanner.Services.Services
{
    public class PhotoDataService : ServiceBase<PhotoDataViewModel, PhotoData>, IPhotoDataService
    {
        public PhotoDataService(
            IRepository<PhotoData> repository,
            IMapper mapper) : base(repository, mapper)
        { }
    }
}
