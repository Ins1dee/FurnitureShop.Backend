using FurnitureShop.Data.Entities;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IAccessTokenService
    {
        public Task<string> CreateAccessTokenAsync(User user);
    }
}
