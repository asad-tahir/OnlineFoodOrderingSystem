using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models
{
    public class Order
    {
        public int Id { get; set; }
        // CustomerId
        // IsDelivered
        public DateTime OrderDate { get; set; }
        public Transaction Transaction { get; set; }
        public int TransactionId { get; set; }
    }
}