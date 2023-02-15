using FluentValidation;
using FluentValidation.AspNetCore;
using FurnitureShop.Data.Context;
using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Dtos.UserDtos;
using FurnitureShop.Domain.FluentValidation;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Implementation;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FurnitureShop.WebApi
{
    public static class DependencyInjection
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        public static void ConfigureDbServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FurnitureStorageContext>(options =>
                  options.UseSqlServer(configuration.GetConnectionString("sqlConnection"),
                  x => x.MigrationsAssembly("FurnitureShop.Data")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IFurnitureService, FurnitureService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddScoped<IRepository<Furniture>, Repository<Furniture>>();
            services.AddScoped<IRepository<Order>, Repository<Order>>();
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<RefreshToken>, Repository<RefreshToken>>();
            services.AddScoped<IRepository<ShoppingCart>, Repository<ShoppingCart>>();
        }

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.AccessTokenKey)),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        public static void ConfigureFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidation();

            services.AddScoped<IValidator<UserForRegistrationDto>, UserForRegistrationDtoValidator>();
            services.AddScoped<IValidator<UserForLoginDto>, UserForLoginDtoValidator>();
        }
    }
}
