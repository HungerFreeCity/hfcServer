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
using System.Collections.ObjectModel;
using hfcServer.Models;

namespace hfcServer.Controllers
{
    public class FoodBanksController : ApiController
    {
        private NeedModel db = new NeedModel();
        public class GeoPoint
        {
            public double Latitude { get; set; }
            public double Longtitude { get; set; }
            private static double ToRad(double input)
            {
                return input * (Math.PI / 180);
            }
            public static bool withinRange(GeoPoint point1, GeoPoint point2)
            {
                const double EARTH_RADIUS_KM = 6371;
                const double range = 25;
                double dLat = ToRad(point2.Latitude - point1.Latitude);
                double dLon = ToRad(point2.Longtitude - point1.Longtitude);

                double a = Math.Pow(Math.Sin(dLat / 2), 2) +
                           Math.Cos(ToRad(point1.Latitude)) * Math.Cos(ToRad(point2.Latitude)) *
                           Math.Pow(Math.Sin(dLon / 2), 2);

                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

                double distance = EARTH_RADIUS_KM * c;
                return distance < range;
            }
        }

        // GET: api/FoodBanks
        public IQueryable<FoodbankViewModel> GetFoodBank([FromUri] GeoPoint userLocation)
        {
            if (userLocation.Latitude != 0 && userLocation.Longtitude != 0)
            {
                List<FoodbankViewModel> minFoodbankFiltered = new List<FoodbankViewModel>();
                foreach (FoodBank fb in db.FoodBanks)
                {
                    if (GeoPoint.withinRange(userLocation, new GeoPoint { Latitude = fb.Latitude, Longtitude = fb.Longtitude }))
                    {
                        ObservableCollection<NeedViewModel> minNeeds = new ObservableCollection<NeedViewModel>();
                        IQueryable<Need> needs = db.Needs.Where(n => n.FoodBankId == fb.Id);
                        foreach (Need n in needs.Where(n => n.Display ==true).ToList<Need>())
                        {
                            minNeeds.Add(new NeedViewModel(n));
                        }
                        minFoodbankFiltered.Add(new FoodbankViewModel(fb, minNeeds));
                    }
                }
                return minFoodbankFiltered.AsQueryable();
            }
            else
            {
                List<FoodbankViewModel> minFoodbank = new List<FoodbankViewModel>();
                List<FoodBank> fbs = db.FoodBanks.Take(25).ToList();
                foreach (FoodBank fb in fbs)
                {
                    ObservableCollection<NeedViewModel> minNeeds = new ObservableCollection<NeedViewModel>();
                    List<Need> needs = db.Needs.Where(n => n.FoodBankId == fb.Id).ToList<Need>();
                    foreach (Need n in needs.Where(n => n.Display == true))
                    {
                        minNeeds.Add(new NeedViewModel(n));
                    }
                    minFoodbank.Add(new FoodbankViewModel(fb, minNeeds));
                }
                return minFoodbank.AsQueryable();
            }
        }
        [ActionName("GetNeeds")]
        public IQueryable<NeedViewModel> GetNeeds(int id)
        {
            ObservableCollection<NeedViewModel> minNeeds = new ObservableCollection<NeedViewModel>();
            List<Need> needs = db.Needs.Where(n => n.FoodBankId == id).ToList<Need>();
            foreach (Need n in needs.Where(n => n.Display == true))
            {
                minNeeds.Add(new NeedViewModel(n));
            }
            return minNeeds.AsQueryable();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodBankExists(int id)
        {
            return db.FoodBanks.Count(e => e.Id == id) > 0;
        }
    }
}
