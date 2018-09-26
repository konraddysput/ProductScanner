using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces.Base;
using ProductScanner.ViewModels.PhotoType;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoTypeService : IServiceBase<PhotoTypeViewModel, PhotoType>
    {
    }
}
