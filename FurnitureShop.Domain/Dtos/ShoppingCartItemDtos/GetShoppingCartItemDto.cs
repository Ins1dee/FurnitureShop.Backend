using FurnitureShop.Domain.Dtos.FurnitureDtos;

namespace FurnitureShop.Domain.Dtos.OrderProductDtos
{
    public class GetShoppingCartItemDto
    {
        public GetFurnitureDto Furniture { get; set; } = new GetFurnitureDto();

        public int Quantity { get; set; }
    }
}
