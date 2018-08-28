using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using ProductScanner.Database.Entities.Base;
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
        public double PositionXL { get; set; }
        public double PositionYL { get; set; }
        public double PositionXR { get; set; }
        public double PositionYR { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        private Photo _photo;
        public virtual Photo Photo
        {
            get
            {
                return LazyLoader.Load(this, ref _photo);
            }

            set { _photo = value; }
        }

        private ILazyLoader LazyLoader { get; set; }
    }
}
