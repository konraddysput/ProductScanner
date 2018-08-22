using System.ComponentModel.DataAnnotations;

namespace ProductScanner.Database.Entities.Base
{
    public class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
