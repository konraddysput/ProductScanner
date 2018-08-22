using ProductScanner.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductScanner.Database.Entities
{
    public class Photo: EntityBase
    {
        public string Path { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
