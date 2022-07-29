using FurnitureStorage.Data.Entities;
using FurnitureStorage.Domain.Dtos.DimensionsDtos;
using FurnitureStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Domain.Dtos.Furniture
{
    public class UpdateFurnitureDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DimensionsDto FurnitureDimensions { get; set; } = new DimensionsDto();

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double Price { get; set; }
    }
}
