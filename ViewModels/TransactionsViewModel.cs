using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.ViewModels
{
    public class TransactionsViewModel
    {
        public string CustomerName { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
    }
}