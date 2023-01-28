using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        public Task<RefreshToken> CreateRefreshTokenAsync(int userId);

        public Task<Result<RefreshToken>> UpdateRefreshTokenAsync(string token, User user);

        public Task DeleteRefreshTokenAsync(int refreshSessionId);
    }
}
