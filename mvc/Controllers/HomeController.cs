using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Theme = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Theme = "Your contact page.";

            return View();
        }
    }
}