using AuthorizeNet.Api.Controllers.Bases;
using OnlineFoodOrderingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Contracts.V1;
using OnlineFoodOrderingSystem.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

namespace OnlineFoodOrderingSystem.Controllers
{

    public class OrdersController : Controller
    {
        // GET: Orders
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        // POST: Orders/PlaceOrder
        [Authorize(Roles = UserRole.Customer)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(PlaceOrderViewModel placeOrderViewModel)
        {
            string CustomerId = User.Identity.GetUserId();
            var response = PlaceOrderViewModel.PlaceOrder(placeOrderViewModel, CustomerId);
            return Json(new { msg = response }, JsonRequestBehavior.AllowGet);
        }


        // GET: Orders/GetOrders
        [Authorize]
        [HttpGet]
        public ActionResult GetOrders()
        {
            var userId = User.Identity.GetUserId();
            using (var _context = new ApplicationDbContext())
            {
                if (User.IsInRole(UserRole.Customer))
                {
                    var orders = _context.Orders.Include(o => o.Transaction).Where(o => o.CustomerId == userId).ToList();
                    var customerOrders = new List<CustomerOrderViewModel>();
                    foreach (var order in orders)
                    {
                        var customerOrder = new CustomerOrderViewModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate.ToString(),
                            Status = order.Status,
                            Amount = order.Transaction.Amount
                        };
                        customerOrders.Add(customerOrder);
                    }
                    return Json(customerOrders, JsonRequestBehavior.AllowGet);
                }
                if (User.IsInRole(UserRole.Admin))
                {
                    var orders = _context.Orders.Include(o => o.Customer).Include(o => o.Transaction).ToList();
                    var customerOrders = new List<CustomerOrderViewModel>();
                    foreach (var order in orders)
                    {
                        var customerOrder = new CustomerOrderViewModel
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate.ToString(),
                            Status = order.Status,
                            Amount = order.Transaction.Amount,
                            Address = order.Customer.Address,
                            Name = order.Customer.Name
                        };
                        customerOrders.Add(customerOrder);
                    }
                    return Json(customerOrders, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "Something went wrong!" }, JsonRequestBehavior.AllowGet);
        }

        // GET: Orders/GetOrders
        [Authorize]
        [HttpGet]
        public ActionResult GetOrder(int id)
        {
            using (var _context = new ApplicationDbContext())
            {
                if (User.IsInRole(UserRole.Admin))
                {
                    var order = _context.Orders.SingleOrDefault(o => o.Id == id);
                    if (order == null)
                    {
                        return Json(new { msg = "Something went wrong!" });

                    }
                    var orderItems = _context.OrderItems.Include(oi => oi.Item).Where(oi => oi.OrderId == order.Id).ToList();
                    var orderItemsList = new List<CartItem>();
                    foreach (var item in orderItems)
                    {
                        var cartItem = new CartItem
                        {
                            Id = item.ItemId,
                            Name = item.Item.Name,
                            Price = item.Price,
                            Qty = item.Qty
                        };
                        orderItemsList.Add(cartItem);
                    }
                    return Json(orderItemsList, JsonRequestBehavior.AllowGet);
                }
                if (User.IsInRole(UserRole.Customer))
                {
                    var order = _context.Orders.SingleOrDefault(o => o.Id == id);
                    if (order == null)
                    {
                        return Json(new { msg = "Something went wrong!" });

                    }
                    if (order.CustomerId != User.Identity.GetUserId())
                    {
                        return Json(new { msg = "Something went wrong!" });
                    }
                    var orderItems = _context.OrderItems.Include(oi => oi.Item).Where(oi => oi.OrderId == order.Id).ToList();
                    var orderItemsList = new List<CartItem>();
                    foreach (var item in orderItems)
                    {
                        var cartItem = new CartItem
                        {
                            Id = item.ItemId,
                            Name = item.Item.Name,
                            Price = item.Price,
                            Qty = item.Qty
                        };
                        orderItemsList.Add(cartItem);
                    }
                    return Json(orderItemsList, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new { msg = "Something went wrong!" });
        }
        // GEt: Orders/changeOrderStatus/1
        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult ChangeOrderStatus(int id)
        {
            using (var _context = new ApplicationDbContext())
            {
                var order = _context.Orders.SingleOrDefault(o => o.Id == id);
                if(order != null)
                {
                    order.Status = OrderStatus.Delivered;
                    _context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }
    }
}