using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        // TODO: CustomerId
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}