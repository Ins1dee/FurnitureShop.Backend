using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.OrderProductDtos
{
    public class ShoppingCartItemDto
    {
        public int FurnitureId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
