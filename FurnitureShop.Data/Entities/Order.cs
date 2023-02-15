namespace FurnitureShop.Data.Entities
{
    public class Order : IEntity
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<ShoppingCartItem> Products { get; set; } = new List<ShoppingCartItem>();

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerNumber { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public double TotalAmount { get; set; }

        public bool IsCompleted { get; set; } = false;

        public User User { get; set; }
    }
}
