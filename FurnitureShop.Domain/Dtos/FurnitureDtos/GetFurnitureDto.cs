using FurnitureShop.Domain.Dtos.DimensionsDtos;

namespace FurnitureShop.Domain.Dtos.FurnitureDtos
{
    public class GetFurnitureDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DimensionsDto FurnitureDimensions { get; set; } = new DimensionsDto();

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double Price { get; set; }
    }
}
