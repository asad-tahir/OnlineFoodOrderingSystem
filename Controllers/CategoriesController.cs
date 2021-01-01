using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext _context;
        public CategoriesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize(Roles =UserRole.Admin)]
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = UserRole.Admin)]
        public ActionResult AddCategory()
        {
            return View();
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public ActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles =UserRole.Admin)]
        public ActionResult DeleteCategory(int id)
        {

            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Categories");
        }
    }
}