using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services.Base;
using ProductScanner.ViewModels.Photo;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductScanner.Services.Services
{
    public class PhotoService : ServiceBase<PhotoViewModel,Photo>, IPhotoService
    {
        private const string imageDirectoryName = "images";
        private readonly string _webRootPath;

        public PhotoService(
            IRepository<Photo> photoRepository,
            IMapper mapper,
            IHostingEnvironment environment): base(photoRepository, mapper)
        {
            _webRootPath = environment.WebRootPath;
        }

        public async Task<Photo> Create(IFormFile file, int userId)
        {
            var filePath = await SavePhoto(file);

            var result = await Repository.Add(new Photo()
            {
                Path = filePath,
                UserId = userId
            });
            return result;
        }

        public string GetPathById(int id)
        {
            var photo = Repository.Get(id);
            return photo == null
                ? string.Empty
                : photo.Path;
        }

        public void UpdateAnalysedPath(int id, string path)
        {
            var photo = Repository.Get(id);
            if (photo == null)
            {
                return;
            }
            photo.AnalysedFilePath = path;
            Repository.Update(photo);
        }

        private async Task<string> SavePhoto(IFormFile file)
        {
            var imagePath = Path.Combine(_webRootPath, imageDirectoryName);
            var filePath = Path.Combine(imagePath, Guid.NewGuid() + Path.GetExtension(file.FileName));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }
    }
}
