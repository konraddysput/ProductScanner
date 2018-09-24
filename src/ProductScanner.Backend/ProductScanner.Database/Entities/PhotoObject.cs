using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductScanner.Database.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductScanner.Database.Entities
{
    public class PhotoObject : EntityBase
    {
        public PhotoObject() { }
        public PhotoObject(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        public double Score { get; set; }
        public string Category { get; set; }

        //box positions
        public double PositionYMin { get; set; }
        public double PositionXMin { get; set; }
        public double PositionYMax { get; set; }
        public double PositionXMax { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        private Photo _photo;
        public virtual Photo Photo
        {
            get => LazyLoader.Load(this, ref _photo);

            set => _photo = value;
        }

        public virtual ICollection<PhotoType> PhotoTypes { get; set; }
        public virtual ICollection<PhotoData> PhotoData { get; set; }

        private ILazyLoader LazyLoader { get; set; }
    }
}
