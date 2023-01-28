using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IConfiguration configuration;
        private readonly JwtSettings jwtSettings;
        public AccessTokenService(IConfiguration configuration, JwtSettings jwtSettings)
        {
            this.configuration = configuration;
            this.jwtSettings = jwtSettings;
        }

        public async Task<string> CreateAccessTokenAsync(User user)
        {
            return await Task.Run(() =>
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var securityToken = new JwtSecurityToken(
                    issuer: configuration["Issuer"],
                    audience: configuration["Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(jwtSettings.AccessTokenExpirationTime),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(securityToken);
            });
        }
    }
}
