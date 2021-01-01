using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Web;

namespace OnlineFoodOrderingSystem.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public bool IsAvailable { get; set; } 
        public IEnumerable<int> CategoryIds { get; set; }
        public HttpPostedFileBase Image { get; set; }
        public static void AddUpdateItem(ItemViewModel viewModel)
        {

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var zz = _context.Items.SingleOrDefault(i => i.Id == viewModel.Id);
                if (zz == null)
                {
                    // Add image ...
                    // viewModel.Image = HttpPostedFileBase ...
                    string url = "Images/";
                    if (viewModel.Image != null)
                    {
                        var fileName = viewModel.Name + "_";
                        fileName += DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture);
                        fileName += Path.GetExtension(viewModel.Image.FileName);
                        
                        string path = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), fileName);
                        viewModel.Image.SaveAs(path);

                        url += fileName;
                    }
                    viewModel.ImageUrl = url;
                    var categories = new List<Category>();
                    foreach(var categoryId in viewModel.CategoryIds)
                    {
                        var category = _context.Categories.Find(categoryId);
                        categories.Add(category);
                    }

                    var item = new Item {
                        Categories = categories,
                        Description = viewModel.Description,
                        ImageUrl = viewModel.ImageUrl,
                        IsAvailable = viewModel.IsAvailable,
                        Name = viewModel.Name,
                        Price = viewModel.Price
                    };
                    _context.Items.Add(item);
                    _context.SaveChanges();
                    
                }
                else
                {
                    UpdateItem(viewModel);
                }
            }
        }
        public static void UpdateItem(ItemViewModel viewModel)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var item = _context.Items.SingleOrDefault(i => i.Id == viewModel.Id);
                if(item != null)
                {
                    item.Price = viewModel.Price;
                    item.IsAvailable = viewModel.IsAvailable;
                    _context.SaveChanges();
                }
            }
        }
        public static List<Item> GetItems()
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var items = _context.Items.ToList();
                return items;
            }
        }
        
        public static Item GetItems(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var item = _context.Items.SingleOrDefault(i => i.Id == id);
                return item;
            }
        }
    }
}