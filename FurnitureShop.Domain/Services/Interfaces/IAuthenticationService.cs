using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.TokensDtos;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.Domain.Models;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<Result<UserForRegistrationDto>> CreateAsync(UserForRegistrationDto newUser);

        public Task<Result<TokensDto>> LoginAsync(UserForLoginDto loggingUser);

        public Task<Result<TokensDto>> RefreshTokenAsync(string token, string userEmail);
    }
}
