using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;
using dto = mvc.Models;
using System.Data;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace mvc.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            Users users = new Users();
            ViewBag.userlists = users.Select();
            return View();
        }
        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            Users users = new Users();
            ViewBag.userlist = users.GetById(id);
            return View();
        }
        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Users users = new Users();
            users.Delete(id);
            return RedirectToAction("Index");
        }
        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "username,firstname,lastname,password,useremail,userlevel")] Users user)
        {
            if (ModelState.IsValid && IsValid(user.useremail))
            {
                user.Insert(user.username, user.firstname, user.lastname, user.password, user.useremail, user.userlevel);
                return RedirectToAction("Index");
            }

            return View(user);
        }
        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            Users user = new Users();
            ViewBag.useritems = user.GetById(id);
            user.user_id = id;
            user.username = ViewBag.useritems[1];
            user.firstname = ViewBag.useritems[2];
            user.lastname = ViewBag.useritems[3];
            user.password = ViewBag.useritems[4];
            user.useremail = ViewBag.useritems[5];
            user.userlevel = (Level)Enum.Parse(typeof(Level), ViewBag.useritems[6], true);
            return View(user);
        }
        // GET: User/Detail/5
        public ActionResult Detail(int id)
        {
            Users users = new Users();
            ViewBag.userlist = users.GetById(id);
            return View();
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "user_id,username,firstname,lastname,password,useremail,userlevel")] Users user, int id)
        {
            if (ModelState.IsValid && IsValid(user.useremail))
            {
                user.Update(user.username, user.firstname, user.lastname, user.password, user.useremail, user.userlevel, id);
                return RedirectToAction("Index");
            }

            return View(user);
        }
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }


    }
}