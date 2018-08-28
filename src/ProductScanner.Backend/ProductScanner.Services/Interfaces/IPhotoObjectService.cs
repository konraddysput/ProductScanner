using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces.Base;
using ProductScanner.ViewModels.PhotoObject;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoObjectService : IServiceBase<PhotoObjectViewModel, PhotoObject>
    {
    }
}
