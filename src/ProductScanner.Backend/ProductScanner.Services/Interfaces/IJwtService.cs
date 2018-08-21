using ProductScanner.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductScanner.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(ApplicationUser user);
    }
}
