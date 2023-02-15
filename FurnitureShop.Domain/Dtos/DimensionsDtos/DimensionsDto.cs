using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Domain.Dtos.DimensionsDtos
{
    public class DimensionsDto
    {
        [Range(0, double.MaxValue)]
        public double Width { get; set; }

        [Range(0, double.MaxValue)]
        public double Height { get; set; }

        [Range(0, double.MaxValue)]
        public double Length { get; set; }
    }
}
