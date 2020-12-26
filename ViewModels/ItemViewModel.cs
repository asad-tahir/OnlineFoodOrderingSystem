using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.ViewModels
{
    public class ItemViewModel
    {
    }
    public class AddItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public IEnumerable<int> CategoryIds { get; set; }
        public HttpPostedFileBase Image { get; set; }
/*        public static void AddItem()
        {
            
        }*/
    }
}