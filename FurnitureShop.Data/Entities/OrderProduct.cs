namespace FurnitureShop.Data.Entities
{
    public class OrderProduct
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        public Furniture Furniture { get; set; }

        public int Quantity { get; set; }
    }
}
