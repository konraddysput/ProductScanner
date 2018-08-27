using ProductScanner.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoObjectService
    {
        Task<PhotoObject> Add(PhotoObject @object);
        Task<IEnumerable<PhotoObject>>  Add(IEnumerable<PhotoObject> objects);
        Task SaveChanges();
    }
}
