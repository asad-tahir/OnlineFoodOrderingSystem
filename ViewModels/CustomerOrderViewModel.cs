using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.ViewModels
{
    public class CustomerOrderViewModel
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string OrderDate { get; set; }
        public decimal Amount { get; set; }
    }
}