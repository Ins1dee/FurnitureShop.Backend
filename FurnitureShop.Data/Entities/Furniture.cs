namespace FurnitureShop.Data.Entities
{
    public class Furniture : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Dimensions FurnitureDimensions { get; set; } = new Dimensions();

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double Price { get; set; } = 0;
    }
}
