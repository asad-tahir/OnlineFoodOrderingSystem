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
    }
}