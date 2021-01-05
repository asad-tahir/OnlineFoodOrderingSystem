using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineFoodOrderingSystem.Models
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(30, ErrorMessage = "The Category Name cannot exceed 30 characters. ")]
        [Required]
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}


