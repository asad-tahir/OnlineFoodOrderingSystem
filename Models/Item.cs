using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The Name cannot exceed 30 characters. ")]
        public string Name { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "The Description cannot exceed 60 characters. ")]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;
        public ICollection<Category> Categories { get; set; }
    }
}