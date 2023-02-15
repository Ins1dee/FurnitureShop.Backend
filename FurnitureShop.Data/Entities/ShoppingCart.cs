using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureShop.Data.Entities
{
    public class ShoppingCart : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
    }
}
