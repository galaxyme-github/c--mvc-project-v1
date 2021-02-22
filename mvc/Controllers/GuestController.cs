using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;
using System.Net.Mail;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Diagnostics;

namespace mvc.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        public ActionResult Index()
        {
            Guests guests = new Guests();
            ViewBag.guestlists = guests.Select();
            return View();
        }

        public ActionResult LAccess()
        {
            return View();
        }

        public ActionResult LAVIEW()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("LA");
            return View();
        }

        public ActionResult AAVIEW()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("AA");
            return View();
        }

        public ActionResult LADeleteView()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("LA");
            return View();
        }

        public ActionResult LAEditView()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("LA");
            return View();
        }

        public ActionResult AADeleteView()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("AA");
            return View();
        }

        public ActionResult AAEditView()
        {
            Guests guests = new Guests();
            ViewBag.alllists = guests.getByRule("AA");
            return View();
        }

        public ActionResult AAccess()
        {
            return View();
        }

        public ActionResult CheckIn()
        {
            return View();
        }

        public ActionResult ACreate()
        {
             ViewBag.todayfee = LAEventFee();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ACreate([Bind(Include = "firstname,lastname,middlename,birthdate,streetaddress, city, state, driverlicensenumber, expirationdate, height, weight, phonenumber, emailaddress, eyes, sex")] Guests guest)
        {
            if (ModelState.IsValid && IsValid(guest.emailaddress))
            {
                string guestid = guest.firstname.Substring(0, 1) + guest.lastname.Substring(0, 1) + guest.driverlicensenumber.Substring(guest.driverlicensenumber.Length - 4, 4);
                if (guest.GetByGuestId(guestid))
                {
                    ViewBag.message = true;
                    ViewBag.contents = "This guest is already in database.";
                    return View(guest);
                }
                if(GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")) < 18)
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "BAD", true), (Rule)Enum.Parse(typeof(Rule), "AA", true));
                    ViewBag.agecheck = true;
                    return View(guest);
                }else if (GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")) < 21)
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "VIP", true), (Rule)Enum.Parse(typeof(Rule), "AA", true));
                }
                else
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "GOOD", true), (Rule)Enum.Parse(typeof(Rule), "AA", true));
                }
                History history = new History();
                ViewBag.guestitems = guest.getlast_id();
                int lastid = Int32.Parse(ViewBag.guestitems[0]);
                return RedirectToAction("ConfirmPage", new { stagename = guest.firstname, eventfee = AAEventFee(), age = GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")), lastid = lastid });
            }

            return View(guest);
        }

        public ActionResult LCreate()
        {
            ViewBag.todayfee = LAEventFee();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LCreate([Bind(Include = "firstname,lastname,middlename,birthdate,streetaddress, city, state, driverlicensenumber, expirationdate, height, weight, phonenumber, emailaddress, eyes, sex")] Guests guest)
        {
            if (ModelState.IsValid && IsValid(guest.emailaddress))
            {
                string guestid = guest.firstname.Substring(0, 1) + guest.lastname.Substring(0, 1) + guest.driverlicensenumber.Substring(guest.driverlicensenumber.Length - 4, 4);
                if (guest.GetByGuestId(guestid))
                {
                    ViewBag.message = true;
                    ViewBag.contents = "This guest is already in database.";
                    return View(guest);
                }
                if (GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")) < 18)
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "BAD", true), (Rule)Enum.Parse(typeof(Rule), "LA", true));
                    ViewBag.agecheck = true;
                    return View(guest);
                }
                else if (GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")) < 21)
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "VIP", true), (Rule)Enum.Parse(typeof(Rule), "LA", true));
                }
                else
                {
                    guest.Insert(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, (AgeCheck)Enum.Parse(typeof(AgeCheck), "GOOD", true), (Rule)Enum.Parse(typeof(Rule), "LA", true));
                }
                History history = new History();
                ViewBag.guestitems = guest.getlast_id();
                int lastid = Int32.Parse(ViewBag.guestitems[0]);
                RegisterHistory(lastid, LAEventFee());
                return RedirectToAction("ConfirmPage", new { stagename = guest.firstname + " " + guest.lastname, eventfee = LAEventFee(), age = GetAge(guest.birthdate, DateTime.Now.ToString("yyyy-MM-dd")), lastid = lastid });
            }

            return View(guest);
        }

        public int GetAge(string firstdate, string seconddate)
        {
            int dateYear1 = Int32.Parse(firstdate.Substring(0, 4));
            int dateYear2 = Int32.Parse(seconddate.Substring(0, 4));
            int dateMonth1 = Int32.Parse(firstdate.Substring(5, 2));
            int dateMonth2 = Int32.Parse(seconddate.Substring(5, 2));
            return (((dateYear2 - dateYear1) * 12) + dateMonth2 - dateMonth1) /12;
        }

        public void RegisterHistory(int id, float fee, int termreject = 1, string reason = "")
        {
            History history = new History();
            history.Insert(id.ToString(), Session["username"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("HH:mm"), fee.ToString(), "Photopath", termreject, reason);
        }

        // GET: User/Delete/5
        public ActionResult LDelete(int id)
        {
            Guests guest = new Guests();
            ViewBag.guestlist = guest.GetById(id);
            return View();
        }
        // POST: Accounts/Delete/5
        [HttpPost, ActionName("LDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult LDeleteConfirmed(int id)
        {
            Guests guest = new Guests();
            guest.Delete(id);
            return RedirectToAction("LADeleteVIEW");
        }

        public ActionResult ADelete(int id)
        {
            Guests guest = new Guests();
            ViewBag.guestlist = guest.GetById(id);
            return View();
        }
        // POST: Accounts/Delete/5
        [HttpPost, ActionName("ADelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ADeleteConfirmed(int id)
        {
            Guests guest = new Guests();
            guest.Delete(id);
            return RedirectToAction("AADeleteVIEW");
        }

        // GET: User/Detail/5
        public ActionResult Detail(int id)
        {
            Guests guest = new Guests();
            ViewBag.guestlist = guest.GetById(id);
            return View();
        }

        public ActionResult CheckInNumber()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckInNumber(string guestid)
        {
            Guests guest = new Guests();
            if (!guest.GetByGuestId(guestid))
            {
                string contents = "Guest ID number " + guestid + " is not in the system.  please Add Guest to proceed.";
                return RedirectToAction("ACreate");
            }
            RegisterHistory(guest.GetIdByGuestId(guestid), LAEventFee());
            guest.LoyaltyPointsIncrease(guestid);
            return RedirectToAction("AAVIEW");
        }

        public bool IsValid(string emailaddress)
        {
            if (emailaddress == null)
            {
                return true;
            }
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
        //Get Guest/Edit/3
        public ActionResult AEdit(int id)
        {
            Guests guest = new Guests();
            ViewBag.guestitems = guest.GetById(id);
            ViewBag.gueatid = id;
            guest.guest_id = id;
            guest.firstname = ViewBag.guestitems[1];
            guest.lastname = ViewBag.guestitems[2];
            guest.middlename = ViewBag.guestitems[4];
            guest.birthdate = ViewBag.guestitems[3];
            guest.streetaddress = ViewBag.guestitems[5];
            guest.city = ViewBag.guestitems[6];
            guest.state = ViewBag.guestitems[7];
            guest.driverlicensenumber = ViewBag.guestitems[8];
            guest.expirationdate = ViewBag.guestitems[9];
            guest.height = float.Parse(ViewBag.guestitems[11]);
            guest.weight = float.Parse(ViewBag.guestitems[12]);
            guest.phonenumber = ViewBag.guestitems[14];
            guest.emailaddress = ViewBag.guestitems[15];
            guest.sex = (Sex)Enum.Parse(typeof(Sex), ViewBag.guestitems[10], true);
            guest.eyes = (Colour)Enum.Parse(typeof(Colour), ViewBag.guestitems[13], true);
            return View(guest);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AEdit([Bind(Include = "guest_id,firstname,lastname,middlename,birthdate,streetaddress,city,state,driverlicensenumber,expirationdate,height,weight,phonenumber,sex,eyes,emailaddress")] Guests guest, int id)
        {
            if (ModelState.IsValid && IsValid(guest.emailaddress))
            {
                string guestid = guest.firstname.Substring(0, 1) + guest.lastname.Substring(0, 1) + guest.driverlicensenumber.Substring(guest.driverlicensenumber.Length - 4, 4);

                guest.Update(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, guestid, id);
                return RedirectToAction("AAEditView");
            }

            return View(guest);
        }

        public ActionResult LEdit(int id)
        {
            Guests guest = new Guests();
            ViewBag.guestitems = guest.GetById(id);
            guest.guest_id = id;
            guest.firstname = ViewBag.guestitems[1];
            guest.lastname = ViewBag.guestitems[2];
            guest.middlename = ViewBag.guestitems[4];
            guest.birthdate = ViewBag.guestitems[3];
            guest.streetaddress = ViewBag.guestitems[5];
            guest.city = ViewBag.guestitems[6];
            guest.state = ViewBag.guestitems[7];
            guest.driverlicensenumber = ViewBag.guestitems[8];
            guest.expirationdate = ViewBag.guestitems[9];
            guest.height = float.Parse(ViewBag.guestitems[11]);
            guest.weight = float.Parse(ViewBag.guestitems[12]);
            guest.phonenumber = ViewBag.guestitems[14];
            guest.emailaddress = ViewBag.guestitems[15];
            guest.sex = (Sex)Enum.Parse(typeof(Sex), ViewBag.guestitems[10], true);
            guest.eyes = (Colour)Enum.Parse(typeof(Colour), ViewBag.guestitems[13], true);
            return View(guest);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LEdit([Bind(Include = "guest_id,firstname,lastname,middlename,birthdate,streetaddress,city,state,driverlicensenumber,expirationdate,height,weight,phonenumber,sex,eyes,emailaddress")] Guests guest, int id)
        {
            if (ModelState.IsValid && IsValid(guest.emailaddress))
            {
                string guestid = guest.firstname.Substring(0, 1) + guest.lastname.Substring(0, 1) + guest.driverlicensenumber.Substring(guest.driverlicensenumber.Length - 4, 4);

                guest.Update(guest.firstname, guest.lastname, guest.birthdate, guest.middlename, guest.streetaddress, guest.city, guest.state, guest.driverlicensenumber, guest.expirationdate, guest.sex, guest.height.ToString(), guest.weight.ToString(), guest.eyes, guest.phonenumber, guest.emailaddress, guestid, id);
                return RedirectToAction("LAEditView");
            }

            return View(guest);
        }

        public float AAEventFee()
        {
            AAccess acc = new AAccess();
            return acc.getTodayEventFee();
        }

        public float LAEventFee()
        {
            LAcceess acc = new LAcceess();
            return acc.getTodayEventFee();
        }
        public ActionResult ConfirmPage(string stagename, string eventfee, int age, int lastid)
        {
            ViewBag.stagename = stagename;
            ViewBag.eventfee = eventfee;
            ViewBag.age = age;
            ViewBag.id = lastid;
            return View();
        }


        public ActionResult Succeed(string stagename, string eventfee, int id)
        {
            ViewBag.stagename = stagename;
            ViewBag.eventfee = eventfee;
            ViewBag.id = id;
            // RegisterHistory(id, AAEventFee());
            return View();
        }

        public ActionResult Rejected(string stagename, string eventfee, int id)
        {
            ViewBag.stagename = stagename;
            ViewBag.eventfee = eventfee;
            ViewBag.id = id;
            // RegisterHistory(id, AAEventFee(), 0);
            return View();
        }

        public ActionResult Override(string stagename, string eventfee, int id)
        {
            ViewBag.stagename = stagename;
            ViewBag.eventfee = eventfee;
            ViewBag.id = id;
            // RegisterHistory(id, AAEventFee(), 0);
            return View();
        }
        public ActionResult PartyDateHistory(int id)
        {
            ViewBag.id = id;
            History his = new History();
            ViewBag.historylist = his.GetByGuestId(id);
            return View();
        }


    }
}