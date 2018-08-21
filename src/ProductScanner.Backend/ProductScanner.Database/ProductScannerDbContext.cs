using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProductScanner.Database.Entities;
using ProductScanner.Database.Identity;

namespace ProductScanner.Database
{
    public class ProductScannerDbContext : IdentityDbContext<ApplicationUser, ApplicationIdentityRole, int>
    {
        public ProductScannerDbContext(DbContextOptions<ProductScannerDbContext> options)
          : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }


    }
}
