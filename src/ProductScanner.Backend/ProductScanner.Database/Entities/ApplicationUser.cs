using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ProductScanner.Database.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public virtual ICollection<Photo> Photos { get; set; }
    }
}
