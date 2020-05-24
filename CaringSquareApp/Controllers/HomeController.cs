using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;
using Microsoft.AspNet.Identity;

namespace CaringSquareApp.Controllers
{
    public class HomeController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Dashboard()
        {
            ViewBag.Message = "Your contact page.";

            DateTime dtFrom = DateTime.ParseExact(DateTime.Now.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture);

            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.ToList().Where(s => s.UserUserId == userId && DateTime.ParseExact(s.EventDate.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture) >= dtFrom);

            //var socialEvents = db.SocialEvents.Include(s => s.AspNetUser).Include(s => s.POIs);
            return View(eventLists.OrderBy(s => s.EventDate));
        }
    }
}