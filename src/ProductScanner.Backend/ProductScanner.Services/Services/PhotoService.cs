using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Repository;
using ProductScanner.Services.Interfaces;
using ProductScanner.Services.Services.Base;
using ProductScanner.ViewModels.Photo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProductScanner.Services.Services
{
    public class PhotoService : ServiceBase<PhotoViewModel, Photo>, IPhotoService
    {
        private const string imageDirectoryName = "images";
        private readonly string _webRootPath;

        public PhotoService(
            IRepository<Photo> photoRepository,
            IMapper mapper,
            IHostingEnvironment environment) : base(photoRepository, mapper)
        {
            _webRootPath = environment.WebRootPath;
        }
        public override async Task<PhotoViewModel> Get(int id)
        {
            var entity = await Repository.Get(id);
            return Mapper.Map<PhotoViewModel>(entity);
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

        public async Task<string> GetPathById(int id)
        {
            var photo = await Repository.Get(id);
            return photo == null
                ? string.Empty
                : photo.Path;
        }

        public async Task UpdateAnalysedPath(int id, string path)
        {
            var photo = await Repository.Get(id);
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

        public Task<IEnumerable<PhotoViewModel>> Get(int page, int limit)
        {
            var entities = Repository.Get()
                .OrderBy(n => n.UploadDate)
                .Skip(page * limit)
                .Take(limit)
                .ToArray();
            var result = Mapper.Map<IEnumerable<PhotoViewModel>>(entities);
            return Task.FromResult(result);
        }
    }
}
