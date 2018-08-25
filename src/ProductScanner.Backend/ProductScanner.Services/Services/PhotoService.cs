using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProductScanner.Services.Services
{
    public class PhotoService : IPhotoService
    {
        private const string imageDirectoryName = "images";
        private readonly IHostingEnvironment _environment;
        private readonly IRepository<Photo> _photoRepository;

        public PhotoService(
            IRepository<Photo> photoRepository,
            IHostingEnvironment environment)
        {
            _environment = environment;
            _photoRepository = photoRepository;
        }

        public async Task<Photo> Create(IFormFile file, int userId)
        {
            var filePath = await SavePhoto(file);

            var result = await _photoRepository.Add(new Photo()
            {
                Path = filePath,
                UserId = userId
            });
            return result;
        }

        public string GetById(int id)
        {
            var photo = _photoRepository.Get(id);
            return photo == null
                ? string.Empty
                : photo.Path;
        }

        public async Task SaveChanges()
        {
            await _photoRepository.SaveChanges();
        }

        private async Task<string> SavePhoto(IFormFile file)
        {
            var imagePath = Path.Combine(_environment.WebRootPath, imageDirectoryName);
            var filePath = Path.Combine(imagePath, Guid.NewGuid() + Path.GetExtension(file.FileName));
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return filePath;
        }
    }
}
