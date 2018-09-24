using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductScanner.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductScanner.Database.Entities
{
    public class PhotoType : EntityBase
    {
        public PhotoType() { }
        public PhotoType(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }
        public string Type { get; set; }
        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public virtual PhotoObject Photo { get; set; }


        private ILazyLoader LazyLoader { get; set; }
    }
}
