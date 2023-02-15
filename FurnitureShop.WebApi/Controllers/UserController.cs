using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.TokenDtos;
using FurnitureShop.Domain.Dtos.TokensDtos;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurnitureShop.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserForRegistrationDto newUser)
        {
            return Ok(await authenticationService.CreateAsync(newUser));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserForLoginDto user)
        {
            var result = await authenticationService.LoginAsync(user);
            if (result.Succeeded is false)
            {
                return NotFound(result);
            }

            SetRefreshToken(result.Data);

            return Ok(result);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            var refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken is null)
            {
                return BadRequest("Refresh token is null");
            }

            var result = await authenticationService.RefreshTokenAsync(refreshToken, refreshTokenDto.Email);

            if (result.Succeeded)
            {
                SetRefreshToken(result.Data);

                return Ok(new { result.Data.AccessToken, refreshToken = result.Data.RefreshToken.Token });
            }

            DeleteRefheshToken();

            return Unauthorized(result.ErrorMessage);
        }

        private void SetRefreshToken(TokensDto result)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = result.RefreshToken.Expires
            };

            Response.Cookies.Append("refreshToken", result.RefreshToken.Token, cookieOptions);
        }

        private void DeleteRefheshToken()
        {
            Response.Cookies.Delete("refreshToken");
        }
    }
}
