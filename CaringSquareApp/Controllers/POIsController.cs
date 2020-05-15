using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;

namespace CaringSquareApp.Controllers
{
    public class POIsController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        // GET: POIs
        public ActionResult Index()
        {

            return View(db.POIs.ToList());
        }

        public ActionResult Shopping()
        {
            var shoppingLists = db.POIs.Where(s => s.Category == "shopping").ToList();
            return View(shoppingLists);
        }

        // GET: POIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POIs pOIs = db.POIs.Find(id);
            if (pOIs == null)
            {
                return HttpNotFound();
            }
            return View(pOIs);
        }

        public ActionResult Category()
        {
            return View();
        }
        public ActionResult POI(int? id)
        {
            //{ "library", "cinema", "sport","hangout","religious","museum","park","shopping","bbq" }
            var tempString = "";
            if (id == 1)
            {
                tempString = "cinema";
            }
            else if (id == 2)
            {
                tempString = "shopping";
            }
            else if (id == 3)
            {
                tempString = "sport";
            }
            else if (id == 4)
            {
                tempString = "hangout";
            }
            else if (id == 5)
            {
                tempString = "religious";
            }
            else if (id == 6)
            {
                tempString = "museum";
            }
            else if (id == 7)
            {
                tempString = "park";
            }
            else if (id == 8)
            {
                tempString = "library";
            }
            else if (id == 9)
            {
                tempString = "bbq";
            }

            var poi_list = db.POIs.Where(s => s.Category.Contains(tempString)).ToList();
            return View(poi_list);
        }

        // GET: POIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POIs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PlaceId,Name,Theme,SubTheme,Address,Latitude,Longitude,Category")] POIs pOIs)
        {
            if (ModelState.IsValid)
            {
                db.POIs.Add(pOIs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pOIs);
        }

        // GET: POIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POIs pOIs = db.POIs.Find(id);
            if (pOIs == null)
            {
                return HttpNotFound();
            }
            return View(pOIs);
        }

        // POST: POIs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlaceId,Name,Theme,SubTheme,Address,Latitude,Longitude,Category")] POIs pOIs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOIs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pOIs);
        }

        // GET: POIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POIs pOIs = db.POIs.Find(id);
            if (pOIs == null)
            {
                return HttpNotFound();
            }
            return View(pOIs);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POIs pOIs = db.POIs.Find(id);
            db.POIs.Remove(pOIs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult AddFilter(bool category1 = false, bool category2 = false, bool category3 = false, bool category4 = false, bool category5 = false, bool category6 = false, bool category7 = false, bool category8 = false, bool category9 = false)
        {
            
            var tempString1 = "exception"; var tempString2 = "exception"; var tempString3 = "exception"; var tempString4 = "exception"; var tempString5 = "exception"; var tempString6 = "exception"; var tempString7 = "exception"; var tempString8 = "exception"; var tempString9 = "exception";
            if (category1 == true)
            {
                tempString1 = "cinema";
            }
            if (category2 == true)
            {
                tempString2 = "shopping";
            }
            if (category3 == true)
            {
                tempString3 = "sport";
            }
            if (category4 == true)
            {
                tempString4 = "hangout";
            }
            if (category5 == true)
            {
                tempString5 = "religious";
            }
            if (category6 == true)
            {
                tempString6 = "museum";
            }
            if (category7 == true)
            {
                tempString7 = "park";
            }
            if (category8 == true)
            {
                tempString8 = "library";
            }
            if (category9 == true)
            {
                tempString9 = "bbq";
            }
            var poi_list1 = db.POIs.Where(s => s.Category.Contains(tempString1)).ToList();
            var poi_list2 = db.POIs.Where(s => s.Category.Contains(tempString2)).ToList();
            var poi_list3 = db.POIs.Where(s => s.Category.Contains(tempString3)).ToList();
            var poi_list4 = db.POIs.Where(s => s.Category.Contains(tempString4)).ToList();
            var poi_list5 = db.POIs.Where(s => s.Category.Contains(tempString5)).ToList();
            var poi_list6 = db.POIs.Where(s => s.Category.Contains(tempString6)).ToList();
            var poi_list7 = db.POIs.Where(s => s.Category.Contains(tempString7)).ToList();
            var poi_list8 = db.POIs.Where(s => s.Category.Contains(tempString8)).ToList();
            var poi_list9 = db.POIs.Where(s => s.Category.Contains(tempString9)).ToList();
            var newList = poi_list1.Concat(poi_list2).Concat(poi_list3).Concat(poi_list4).Concat(poi_list5).Concat(poi_list6).Concat(poi_list7).Concat(poi_list8).Concat(poi_list9);
            return View(newList);
        }

        public ActionResult AdvancedSearch(string PlaceName, string StreetName)
        {
            Console.WriteLine("Our total" + PlaceName);
            Console.WriteLine("Our total" + StreetName);
            var poi_list1 = db.POIs.Where(s => s.Name.Contains(PlaceName)).ToList();
            //var poi_list2 = 
            var newList = poi_list1;
            if(StreetName != "")
                newList = db.POIs.Where(s => s.Address.Contains(StreetName)).ToList();
            return View(newList);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
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
