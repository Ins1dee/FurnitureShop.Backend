using FurnitureShop.Domain.Dtos.DimensionsDtos;
using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.FurnitureDtos
{
    public class AddFurnitureDto
    {
        public string Name { get; set; } = string.Empty;

        public DimensionsDto FurnitureDimensions { get; set; } = new DimensionsDto();

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public double Price { get; set; }
    }
}
