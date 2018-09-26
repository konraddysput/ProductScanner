using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces.Base;
using ProductScanner.ViewModels.PhotoData;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoDataService : IServiceBase<PhotoDataViewModel, PhotoData>
    {
    }
}
