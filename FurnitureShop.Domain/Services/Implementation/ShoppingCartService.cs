using FurnitureShop.Data.Entities;
using FurnitureShop.Data.Infrastructure;
using FurnitureShop.Domain.Dtos.FurnitureDtos;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Dtos.OrderProductDtos;
using FurnitureShop.Domain.Models;
using FurnitureShop.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Services.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<Furniture> furnitureRepository;
        private readonly IRepository<ShoppingCart> shoppingCartRepository;
        private readonly IRepository<User> userRepository;

        public ShoppingCartService(IRepository<Furniture> furnitureRepository, IRepository<ShoppingCart> shoppingCartRepository,
             IRepository<User> userRepository)
        {
            this.furnitureRepository = furnitureRepository;
            this.shoppingCartRepository = shoppingCartRepository;
            this.userRepository = userRepository;
        }

        public async Task<Result<ShoppingCart>> GetAllItems(string userEmail)
        {
            var user = await userRepository
                 .Query()
                 .Include(u => u.RefreshTokens)
                 .Include(u => u.Orders)
                 .Include(u => u.ShoppingCart)
                 .Where(u => u.Email.Equals(userEmail))
                 .FirstOrDefaultAsync();

            if (user is null)
            {
                return Result.Failed<ShoppingCart>("User not found");
            }

            var result = await shoppingCartRepository
               .Query()
               .Include(x => x.Items)
               .ThenInclude(x => x.Furniture)
               .ThenInclude(x => x.FurnitureDimensions)
               .Where(x => x.UserId == user.Id)
               .FirstOrDefaultAsync();

            return result is null ? Result.Failed<ShoppingCart>("Your shopping cart is empty")
                : Result.Ok(result);
        }

        public async Task<Result<ShoppingCart>> AddItemsAsync(string userEmail, List<ShoppingCartItemDto> itemsDto)
        {
            var user = await userRepository
                .Query()
                .Include(u => u.RefreshTokens)
                .Include(u => u.Orders)
                .Include(u => u.ShoppingCart)
                .Where(u => u.Email.Equals(userEmail))
                .FirstOrDefaultAsync();

            if (user is null)
            {
                return Result.Failed<ShoppingCart> ("User not found");
            }

            var shoppingCart = new ShoppingCart();
            shoppingCart.UserId = user.Id;

            foreach (var itemDto in itemsDto)
            {
                var furniture = await furnitureRepository
                .Query()
                .Include(f => f.FurnitureDimensions)
                .Where(f => f.Id == itemDto.FurnitureId)
                .FirstOrDefaultAsync();

                if (furniture is null)
                {
                    return Result.Failed<ShoppingCart>($"No product with id \"{itemDto.FurnitureId}\" found!");
                }

                shoppingCart.Items.Add(new ShoppingCartItem()
                {
                    Furniture = furniture,
                    Quantity = itemDto.Quantity
                });
            }

            await shoppingCartRepository.AddAsync(shoppingCart);
            await shoppingCartRepository.SaveChangesAsync();

            return Result.Ok(shoppingCart);
        }

        public async Task<Result<ShoppingCart>> RemoveItemAsync(string userEmail, int itemId)
        {
            var user = await userRepository
                 .Query()
                 .Include(u => u.RefreshTokens)
                 .Include(u => u.Orders)
                 .Include(u => u.ShoppingCart)
                 .Where(u => u.Email.Equals(userEmail))
                 .FirstOrDefaultAsync();

            if (user is null)
            {
                return Result.Failed<ShoppingCart>("User not found");
            }

            var shoppingCartToUpdate = await shoppingCartRepository
               .Query()
               .Include(x => x.Items)
               .ThenInclude(x => x.Furniture)
               .ThenInclude(x => x.FurnitureDimensions)
               .Where(x => x.UserId.Equals(user.Id))
               .FirstOrDefaultAsync();

            if (shoppingCartToUpdate is null)
            {
                return Result.Failed<ShoppingCart>("Your shopping cart is empty");
            }

            var itemToRemove = shoppingCartToUpdate
                    .Items
                    .FirstOrDefault(p => p.Id == itemId);

            if (itemToRemove is null)
            {
                return Result.Failed<ShoppingCart>($"No product with id \"{itemId}\" found!");
            }

            shoppingCartToUpdate.Items.Remove(itemToRemove);
            await shoppingCartRepository.UpdateAsync(shoppingCartToUpdate);
            await shoppingCartRepository.SaveChangesAsync();

            return Result.Ok(shoppingCartToUpdate);
        }
    }
}
