using OnlineFoodOrderingSystem.ViewModels;
using System;
using System.Collections.Generic;
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
        public ActionResult AddItem(AddItemViewModel viewModel)
        {/* 
          
            if (!ModelState.IsValid)
            {
                return View("Form", viewModel);
            }

            viewModel.UserId = User.Identity.GetUserId();

            // Add image ...
            // viewModel.Image = HttpPostedFileBase ...
            string url = "~/Images/";
            if (viewModel.Image != null)
            {
                var fileName = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff", CultureInfo.InvariantCulture) + "_";
                fileName += Path.GetFileName(viewModel.Image.FileName);

                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                viewModel.Image.SaveAs(path);

                url += fileName;
            }
            viewModel.ImageUrl = url;

            // New Item ...
            if (viewModel.Id == null)
            {
                ItemViewModel.AddItem(viewModel);
            }
            else
            {
                ItemViewModel.UpdateItem(viewModel);
            }

            return RedirectToAction("Index", "Item");
          
          */
            return View();
        }
    }
}