using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Data.Entities
{
    public class ShoppingCartItem : IEntity
    {
        public int Id { get; set; }

        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public Furniture Furniture { get; set; }

        public int Quantity { get; set; }
    }
}
