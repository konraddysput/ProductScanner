using Microsoft.AspNetCore.Http;
using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces.Base;
using ProductScanner.ViewModels.Photo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoService: IServiceBase<PhotoViewModel, Photo>
    {
        Task<Photo> Create(IFormFile file, int userId);
        Task<string> GetPathById(int id);
        Task<IEnumerable<PhotoViewModel>> Get(int page, int limit);
    }
}
