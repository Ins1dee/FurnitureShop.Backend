using AutoMapper;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Dtos.TokensDtos;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> userRepository;
        private readonly IPasswordHasherService passwordHasherService;
        private readonly IAccessTokenService accessTokenService;
        private readonly IRefreshTokenService refreshTokenService;
        private readonly IMapper mapper;

        public AuthenticationService(IRepository<User> userRepository, IPasswordHasherService passwordHasherService,
            IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.passwordHasherService = passwordHasherService;
            this.accessTokenService = accessTokenService;
            this.refreshTokenService = refreshTokenService;
            this.mapper = mapper;
        }
        public async Task<Result<UserForRegistrationDto>> CreateAsync(UserForRegistrationDto newUser)
        {
            if (await userRepository.Query().AnyAsync(u => u.Email.Equals(newUser.Email)))
            {
                return Result.Failed<UserForRegistrationDto>("Such a user email is already registered");
            }

            passwordHasherService.Hash(newUser.Password, out string passwordHash, out string passwordSalt);

            var user = new User();
            user.Email = newUser.Email;
            user.Name = newUser.Name;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return Result.Ok(newUser);
        }

        public async Task<Result<TokensDto>> LoginAsync(UserForLoginDto loggingUser)
        {
            var user = await userRepository
                .Query()
                .Where(u => u.Email.Equals(loggingUser.Email))
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return Result.Failed<TokensDto>("User not found");
            }

            if (!passwordHasherService.Verify(loggingUser.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Result.Failed<TokensDto>("Wrong password");
            }

            var result = new Result<TokensDto>(new TokensDto(), true, string.Empty);
            result.Data.AccessToken = await accessTokenService.CreateAccessTokenAsync(user);
            result.Data.RefreshToken = await refreshTokenService.CreateRefreshTokenAsync(user.Id);

            return result;
        }

        public async Task<Result<TokensDto>> RefreshTokenAsync(string token, string userEmail)
        {
            Result<TokensDto> result = new Result<TokensDto>(new TokensDto(), true, string.Empty);

            var user = await userRepository
                .Query()
                .Include(u => u.RefreshTokens)
                .Where(u => u.Email.Equals(userEmail))
                .FirstOrDefaultAsync();

            if(user is null)
            {
                return Result.Failed<TokensDto>("User not found");
            }

            var updateResult = await refreshTokenService.UpdateRefreshTokenAsync(token, user);

            if (!updateResult.Succeeded)
            {
                return Result.Failed<TokensDto>(updateResult.ErrorMessage);
            }

            result.Data.RefreshToken = updateResult.Data;

            result.Data.AccessToken = await accessTokenService.CreateAccessTokenAsync(user);

            return result;
        }
    }
}
