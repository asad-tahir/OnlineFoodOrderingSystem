using OnlineFoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult Index()
        {
            return View();
        }

        // Items/AddItem
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult AddItem()
        {
            return View();
        }

        // Items/Menu
        [Authorize(Roles = UserRole.Customer)]
        public ActionResult Menu()
        {
            return View();
        }

        // POST: /Items/AddItem
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(ItemViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            ItemViewModel.AddUpdateItem(viewModel);

            return RedirectToAction("Index", "Items");
        }

        // POST: /Items/UpdateItem
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateItem(ItemViewModel viewModel)
        {
            ItemViewModel.AddUpdateItem(viewModel);

            return RedirectToAction("Index", "Items");
        }
        // GET: /Items/GetItems
        [HttpGet]
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult GetItems()
        {
            var items = ItemViewModel.GetItems();
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        // GET: /Items/GetITemsByCategory
        [Authorize(Roles = UserRole.Customer)]
        [HttpGet]
        public ActionResult GetItemsByCategory(int id)
        {

            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var category = _context.Categories.Include(c => c.Items).SingleOrDefault(c => c.Id == id);

                var items = new List<CategoryItemsViewModel>();

                foreach (var itm in category.Items.Where(i => i.IsAvailable).ToList())
                {
                    var categoryItemsViewModel = new CategoryItemsViewModel(itm);
                    items.Add(categoryItemsViewModel);
                }

                return Json(items, JsonRequestBehavior.AllowGet);
            }

        }
        // GET: /Items/Item
        [Authorize]
        [HttpGet]
        public ActionResult Item(int id)
        {
            var item = ItemViewModel.GetItems(id);

            return Json(item, JsonRequestBehavior.AllowGet);
        }
        // Get: /Item/DeleteItem
        [Authorize(Roles = UserRole.Admin)]
        public ActionResult DeleteItem(int id)
        {
            using (ApplicationDbContext _context = new ApplicationDbContext())
            {
                var item = _context.Items.SingleOrDefault(i => i.Id == id);
                if (item != null)
                {
                    _context.Items.Remove(item);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Items");
        }
    }
}