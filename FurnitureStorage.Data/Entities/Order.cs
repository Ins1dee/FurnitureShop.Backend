using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureStorage.Data.Entities
{
    public class Order : IEntity
    {
        
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<OrderProduct> Products { get; set; } = new List<OrderProduct>();

        public string CustomerName { get; set; } = string.Empty;

        public string CustomerNumber { get; set; } = string.Empty;

        public string CustomerAddress { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.Now;

        public double TotalAmount { get; set; }

        public bool IsCompleted { get; set; } = false; 
    }
}
