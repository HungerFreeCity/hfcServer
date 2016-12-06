using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hfcServer.Models;

namespace hfcServer.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View(db.FoodBanks.ToList());
        }

        // GET: Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodBank foodBank = db.FoodBanks.Find(id);
            if (foodBank == null)
            {
                return HttpNotFound();
            }
            return View(foodBank);
        }

        // GET: Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Address,Email,Name,WebsiteURL,Latitude,ManagerEmail,Longtitude")] FoodBank foodBank)
        {
            if (ModelState.IsValid)
            {
                db.FoodBanks.Add(foodBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodBank);
        }

        // GET: Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodBank foodBank = db.FoodBanks.Find(id);
            if (foodBank == null)
            {
                return HttpNotFound();
            }
            return View(foodBank);
        }

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Address,Email,Name,WebsiteURL,Latitude,ManagerEmail,Longtitude")] FoodBank foodBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodBank).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodBank);
        }

        // GET: Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodBank foodBank = db.FoodBanks.Find(id);
            if (foodBank == null)
            {
                return HttpNotFound();
            }
            return View(foodBank);
        }

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodBank foodBank = db.FoodBanks.Find(id);
            db.FoodBanks.Remove(foodBank);
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
    }
}
