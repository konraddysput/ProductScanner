using Microsoft.AspNetCore.Http;
using ProductScanner.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProductScanner.Services.Interfaces
{
    public interface IPhotoService
    {
        Task<Photo> Create(IFormFile file, int userId);
        string GetById(int id);
        Task SaveChanges();
    }
}
