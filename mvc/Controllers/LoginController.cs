using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using System.Collections.Specialized;

namespace mvc.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login()
        {
            NameValueCollection nvc = Request.Form;
            string userName = "", password = "";
            if (!string.IsNullOrEmpty(nvc["UserName"]))
            {
                userName = nvc["UserName"];
            }

            if (!string.IsNullOrEmpty(nvc["Password"]))
            {
                password = nvc["Password"];
            }
            Users user = new Users();
            ViewBag.sessiondata = user.GetByEmailAndPassword(userName, password);
            if (ViewBag.sessiondata.Count != 0)
            {
                Session["username"] = ViewBag.sessiondata[1];
                Session["password"] = ViewBag.sessiondata[4];
                Session["useremail"] = ViewBag.sessiondata[5];
                Session["userlevel"] = ViewBag.sessiondata[6];
                Session["created_at"] = DateTime.Now;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        public ActionResult Logout()
        {
            Session["username"] = "";
            Session["password"] = "";
            Session["useremail"] = "";
            Session["userlevel"] = "";
            return RedirectToAction("Index");
        }
    }
}