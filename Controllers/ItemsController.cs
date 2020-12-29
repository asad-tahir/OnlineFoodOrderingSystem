using OnlineFoodOrderingSystem.Models;
using OnlineFoodOrderingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class ItemsController : Controller
    {
        // GET: Items
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddItem()
        {
            return View();
        }
        public ActionResult Menu()
        {
            return View();
        }
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateItem(ItemViewModel viewModel)
        {
            ItemViewModel.AddUpdateItem(viewModel);

            return RedirectToAction("Index", "Items");
        }

        [HttpGet]
        public ActionResult GetItems()
        {
            var items = ItemViewModel.GetItems();

            return Json(items, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult Item(int id)
        {
            var item = ItemViewModel.GetItems(id);

            return Json(item, JsonRequestBehavior.AllowGet);
        }

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