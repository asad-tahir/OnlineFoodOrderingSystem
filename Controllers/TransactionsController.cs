using OnlineFoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineFoodOrderingSystem.ViewModels;

namespace OnlineFoodOrderingSystem.Controllers
{
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = UserRole.Admin)]
        [HttpGet]
        public ActionResult GetTransactions()
        {
            using (var _context = new ApplicationDbContext())
            {
                var transactions = _context.Transactions.Include(t => t.Customer).ToList();
                var transactionsViewModelList = new List<TransactionsViewModel>();
                foreach (var transaction in transactions)
                {
                    var transactionViewModel = new TransactionsViewModel
                    {
                        Amount = transaction.Amount,
                        CustomerName = transaction.Customer.Name,
                        Date = transaction.TransactionDate.ToString()
                    };
                    transactionsViewModelList.Add(transactionViewModel);
                }
                return Json(transactionsViewModelList, JsonRequestBehavior.AllowGet);
            }
        }
        [Authorize(Roles = UserRole.Admin)]
        [HttpPost]
        public ActionResult GetTransactionsByDate(GetTransactionsByDateViewModel getTransactionsByDateViewModel)
        {
            using (var _context = new ApplicationDbContext())
            {
                var dateTime = Convert.ToDateTime(getTransactionsByDateViewModel.date);
                var transactions = _context.Transactions.Include(t => t.Customer).ToList().Where(t => DateTime.Compare(t.TransactionDate.Date, dateTime.Date) == 0);
                var transactionsViewModelList = new List<TransactionsViewModel>();
                foreach (var transaction in transactions)
                {
                    var transactionViewModel = new TransactionsViewModel
                    {
                        Amount = transaction.Amount,
                        CustomerName = transaction.Customer.Name,
                        Date = transaction.TransactionDate.ToString()
                    };
                    transactionsViewModelList.Add(transactionViewModel);
                }
                return Json(transactionsViewModelList, JsonRequestBehavior.AllowGet);
            }
        }
    }
    public class GetTransactionsByDateViewModel
    {
        public string date { get; set; }
    }
}