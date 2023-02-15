using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Data.Entities
{
    public class Dimensions
    {
        public int FurnitureId { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public Furniture Furniture { get; set; }
    }
}
