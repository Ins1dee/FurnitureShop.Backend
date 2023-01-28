using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshToken> refreshTokenRepository;
        private readonly JwtSettings jwtSettings;

        public RefreshTokenService(IRepository<RefreshToken> refreshTokenRepository, JwtSettings jwtSettings)
        {
            this.refreshTokenRepository = refreshTokenRepository;
            this.jwtSettings = jwtSettings;
        }

        public async Task<RefreshToken> CreateRefreshTokenAsync(int userId)
        {
            var refreshToken =  new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddMinutes(jwtSettings.RefreshTokenExpirationTime),
                Created = DateTime.Now,
                UserId = userId
            };

            await refreshTokenRepository.AddAsync(refreshToken);
            await refreshTokenRepository.SaveChangesAsync();

            return refreshToken;
        }

        public async Task<Result<RefreshToken>> UpdateRefreshTokenAsync(string token, User user)
        {
            var refreshToken = await refreshTokenRepository
                .Query()
                .Where(t => t.Token.Equals(token))
                .FirstOrDefaultAsync();

            if (refreshToken is null)
            {
                return Result.Failed<RefreshToken>("Refresh token is null");
            }

            refreshTokenRepository.Delete(refreshToken);

            await refreshTokenRepository.SaveChangesAsync();

            if (DateTime.Now > refreshToken.Expires)
            {
                return Result.Failed<RefreshToken>("Refresh token expired");
            }

            if (user.RefreshTokens.Any(t => t.Token.Equals(refreshToken)))
            {
                return Result.Failed<RefreshToken>($"Ivalid Refresh Token / {user.RefreshTokens.First().Token}");
            }

            var data = await CreateRefreshTokenAsync(refreshToken.UserId);

            return new Result<RefreshToken>(data, true, string.Empty);
        }

        public Task DeleteRefreshTokenAsync(int refreshTokenId)
        {
            throw new NotImplementedException();
        }
    }
}
