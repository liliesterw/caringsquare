using System;
using System.Collections.Generic;
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

            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            //var socialEvents = db.SocialEvents.Include(s => s.AspNetUser).Include(s => s.POIs);
            return View(eventLists);
        }
    }
}