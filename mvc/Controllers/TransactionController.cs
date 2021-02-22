using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class TransactionController : Controller
    {
        // GET: Transaction
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DisplayTransaction(int day, int month, int year)
        {
            Guests guests = new Guests();
            ViewBag.aalists = guests.TransactionFromAAccess(year, month, day);
            ViewBag.lalists = guests.TransactionFromLAccess(year, month, day);
            return View();
        }
    }
}