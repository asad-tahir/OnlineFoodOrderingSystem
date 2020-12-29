using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using OnlineFoodOrderingSystem.ViewModels;

namespace OnlineFoodOrderingSystem.Controllers
{
    [Authorize(Roles = UserRole.Admin)]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            using(ApplicationDbContext _context = new ApplicationDbContext())
            {
                var AdminId = User.Identity.GetUserId();
                var users = _context.Users.Where(u => u.Id != AdminId).ToList();

                var usersViewModelList = new List<UsersViewModel>();
                foreach(var user in users)
                {
                    var userViewModel = new UsersViewModel {
                        Email = user.Email,
                        Name = user.Name,
                        Id = user.Id
                    };
                    usersViewModelList.Add(userViewModel);
                }

                return Json(usersViewModelList, JsonRequestBehavior.AllowGet);
            }
        }
    }
}