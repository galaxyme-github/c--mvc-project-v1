using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc.Models;

namespace mvc.Controllers
{
    public class FeeController : Controller
    {
        // GET: Fee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EventFee(int id = 1)
        {
            LAcceess fee = new LAcceess();
            ViewBag.lfeeitems = fee.GetById(id);
            fee.id = Int32.Parse(ViewBag.lfeeitems[0]);
            fee.Monday = float.Parse(ViewBag.lfeeitems[1]);
            fee.Tuesday = float.Parse(ViewBag.lfeeitems[2]);
            fee.Wednesday = float.Parse(ViewBag.lfeeitems[3]);
            fee.Thursday = float.Parse(ViewBag.lfeeitems[4]);
            fee.Friday = float.Parse(ViewBag.lfeeitems[5]);
            fee.Saturday = float.Parse(ViewBag.lfeeitems[6]);
            return View(fee);
        }

        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EventFee([Bind(Include = "id,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")] LAcceess lfee)
        {
            if (ModelState.IsValid)
            {
                lfee.Update(lfee.Monday, lfee.Tuesday, lfee.Wednesday, lfee.Thursday, lfee.Friday, lfee.Saturday, lfee.id);
                return View(lfee);
            }

            return View(lfee);
        }

        [HttpGet]
        public ActionResult AAEventFee(int id = 1)
        {
            AAccess fee = new AAccess();
            ViewBag.lfeeitems = fee.GetById(id);
            fee.id = Int32.Parse(ViewBag.lfeeitems[0]);
            fee.Monday = float.Parse(ViewBag.lfeeitems[1]);
            fee.Tuesday = float.Parse(ViewBag.lfeeitems[2]);
            fee.Wednesday = float.Parse(ViewBag.lfeeitems[3]);
            fee.Thursday = float.Parse(ViewBag.lfeeitems[4]);
            fee.Friday = float.Parse(ViewBag.lfeeitems[5]);
            fee.Saturday = float.Parse(ViewBag.lfeeitems[6]);
            return View(fee);
        }
        // POST: User/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AAEventFee([Bind(Include = "id,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday")] AAccess afee)
        {
            if (ModelState.IsValid)
            {
                afee.Update(afee.Monday.ToString(), afee.Tuesday.ToString(), afee.Wednesday.ToString(), afee.Thursday.ToString(), afee.Friday.ToString(), afee.Saturday.ToString(), afee.id);
                return RedirectToAction("AAEventFee");
            }

            return View(afee);
        }

        public ActionResult EarlyEventFee()
        {
            EarlyFee efee = new EarlyFee();
            ViewBag.efeeitems = efee.GetById();
            efee.id = Int32.Parse(ViewBag.efeeitems[0]);
            efee.early_fee = float.Parse(ViewBag.efeeitems[1]);
            return View(efee);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EarlyEventFee([Bind(Include = "id,early_fee")] EarlyFee efee)
        {
            if (ModelState.IsValid)
            {
                efee.Update(efee.early_fee, efee.id);
                return View(efee);
            }
            return View(efee);
        }

        public ActionResult LateEventFee()
        {
            LateFee lfee = new LateFee();
            ViewBag.lfeeitems = lfee.GetById();
            lfee.id = Int32.Parse(ViewBag.lfeeitems[0]);
            lfee.late_fee = float.Parse(ViewBag.lfeeitems[1]);
            return View(lfee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LateEventFee([Bind(Include = "id,late_fee")] LateFee lfee)
        {
            if (ModelState.IsValid)
            {
                lfee.Update(lfee.late_fee, lfee.id);
                return View(lfee);
            }
            return View(lfee);
        }


        public ActionResult EarlyBirdSpecial()
        {
            Bird bird = new Bird();
            ViewBag.birditems = bird.GetById();
            bird.id = Int32.Parse(ViewBag.birditems[0]);
            bird.end_time = ViewBag.birditems[1];
            return View(bird);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EarlyBirdSpecial([Bind(Include = "id,end_time")] Bird bird)
        {
            if (ModelState.IsValid)
            {
                bird.Update(bird.end_time, bird.id);
                return View(bird);
            }
            return View(bird);
        }
        public ActionResult AppMessages()
        {
            return View();
        }

    }
}