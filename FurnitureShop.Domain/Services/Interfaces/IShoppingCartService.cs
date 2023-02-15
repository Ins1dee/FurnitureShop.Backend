using FurnitureShop.Data.Entities;
using FurnitureShop.Domain.Dtos.OrderDtos;
using FurnitureShop.Domain.Dtos.OrderProductDtos;
using FurnitureShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Domain.Services.Interfaces
{
    public interface IShoppingCartService
    {
        public Task<Result<ShoppingCart>> GetAllItems(string userEmail);

        public Task<Result<ShoppingCart>> AddItemsAsync(string userEmail, List<ShoppingCartItemDto> itemsDto);

        public Task<Result<ShoppingCart>> RemoveItemAsync(string userEmail, int itemId);
    }
}
