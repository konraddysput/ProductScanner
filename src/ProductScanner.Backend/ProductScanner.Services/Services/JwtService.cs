using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductScanner.Database.Entities;
using ProductScanner.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductScanner.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(ApplicationUser user)
        {
            string jwtKey = _configuration["Jwt:Key"];
            byte[] keyBytes = Encoding.UTF8.GetBytes(jwtKey);

            var key = new SymmetricSecurityKey(keyBytes);
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName)
            };

            int expirationTime = int.Parse(_configuration["Jwt:ExpirationTime"]);
            var token = new JwtSecurityToken(
              _configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(expirationTime),
              claims: claims,
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
