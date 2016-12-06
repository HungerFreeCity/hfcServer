using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hfcServer.Models;

namespace hfcServer.Controllers
{
    public class NeedsController : ApiController
    {
        private NeedModel db = new NeedModel();

        [ResponseType(typeof(void))]
        [ActionName("Donate")]
        public IHttpActionResult Donate(int id, [FromUri] string username)
        {
            if (db.Donations.Where(d => d.DonorId == username && d.NeedId == id).Count() != 0)
            {
                try
                {
                    Donation donation = db.Donations.Where(d => d.DonorId == username && d.NeedId == id).Single();
                    donation.Amount += 1;
                    db.Entry(donation).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch
                {
                    return BadRequest();
                }

            }
            else
            {
                try
                {
                    db.Donations.Add(new Donation { Amount = 1, DonorId = username, NeedId = id });
                    db.SaveChanges();
                }
                catch
                {
                    return BadRequest();
                }
            }
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NeedExists(int id)
        {
            return db.Needs.Count(e => e.Id == id) > 0;
        }
    }
}
