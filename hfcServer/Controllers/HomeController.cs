using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

using hfcServer.Models;

namespace hfcServer.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private NeedModel db = new NeedModel();

        // GET: Needs
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId();
            var foodbank = getCurrentUserFoodbank();
            try
            {
                var needs = db.Needs.Where(n => n.FoodBank.Id == foodbank.Id);
                var chartData = new List<DonationViewModel>();
                foreach (Need n in needs)
                {
                    chartData.Add(new DonationViewModel(n));
                }
                if (chartData.Count > 0)
                    Session["chartData"] = JsonConvert.SerializeObject(chartData);
                else
                    Session["chartData"] = "{ }";
                Session["foodBankName"] = foodbank.Name;
                Session["foodBankId"] = foodbank.Id;
                return View(needs.ToList());
            }
            catch
            {
                Session["foodBankName"] = "";
                Session["chartData"] = "{ }";
                return View(new List<Need>());
            }
        }

        // GET: Needs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Need need = db.Needs.Find(id);
            if (need == null)
            {
                return HttpNotFound();
            }
            return View(need);
        }

        // GET: Needs/Create
        public ActionResult Create()
        {
            string temp = Session["foodbankId"].ToString();
            return View();
        }

        // POST: Needs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Rank,Units,Display,FoodBankId")] Need need)
        {
            need.FoodBankId = int.Parse(Session["foodbankId"].ToString());
            if (ModelState.IsValid)
            {
                db.Needs.Add(need);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(need);
        }

        // GET: Needs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Need need = db.Needs.Find(id);
            if (need == null)
            {
                return HttpNotFound();
            }
            Session["foodbankId"] = getCurrentUserFoodbankId();
            return View(need);
        }

        // POST: Needs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Rank,Units,Display,FoodBankId")] Need need)
        {
            if (ModelState.IsValid)
            {
                db.Entry(need).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(need);
        }

        // GET: Needs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Need need = db.Needs.Find(id);
            if (need == null)
            {
                return HttpNotFound();
            }
            return View(need);
        }

        // POST: Needs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Need need = db.Needs.Find(id);
            db.Needs.Remove(need);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private FoodBank getCurrentUserFoodbank()
        {
            var user = User.Identity.GetUserName();
            try
            {
                List<FoodBank> possibleFoodbanks = db.FoodBanks.Where(f => f.ManagerEmail.Equals(user)).ToList<FoodBank>();
                return possibleFoodbanks.First();
            }
            catch
            {
            }
            return null;
        }
        private int getCurrentUserFoodbankId()
        {
            try
            {
                return getCurrentUserFoodbank().Id;
            }
            catch
            {

            }
            return -1;
        }
    }
}