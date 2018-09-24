using Microsoft.EntityFrameworkCore.Infrastructure;
using ProductScanner.Database.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductScanner.Database.Entities
{
    public class PhotoData : EntityBase
    {
        public PhotoData() { }
        public PhotoData(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        public string Type { get; set; }
        public string Value { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public virtual PhotoObject Photo { get; set; }


        private ILazyLoader LazyLoader { get; set; }
    }
}
