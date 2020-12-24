using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models
{
    public class OrderItem
    {
        
        public Order Order { get; set; }
        [Key]
        [Column(Order = 1)]
        public int OrderId { get; set; }

        public Item Item { get; set; }
        [Key]
        [Column(Order = 2)]
        public int ItemId { get; set; }
        
        public byte Qty { get; set; }
        public decimal Price { get; set; }
    }
}