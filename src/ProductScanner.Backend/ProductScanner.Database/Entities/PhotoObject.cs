using ProductScanner.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProductScanner.Database.Entities
{
    public class PhotoObject: EntityBase
    {
        public double Score { get; set; }
        public string Category { get; set; }

        //box positions
        public double PositionXL { get; set; }
        public double PositionYL { get; set; }
        public double PositionXR { get; set; }
        public double PositionYR { get; set; }

        [ForeignKey(nameof(Photo))]
        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }
    }
}
