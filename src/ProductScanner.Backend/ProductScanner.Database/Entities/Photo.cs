using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductScanner.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductScanner.Database.Entities
{
    public class Photo : EntityBase
    {
        public Photo() { }
        public Photo(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        public string Path { get; set; }
        public string AnalysedFilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        private ICollection<PhotoObject> _photoObjects;
        public virtual ICollection<PhotoObject> PhotoObjects
        {
            get
            {
                return LazyLoader.Load(this, ref _photoObjects);
            }
            set
            {
                _photoObjects = value;
            }
        }

        private ILazyLoader LazyLoader { get; set; }
    }
}
