using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CaringSquareApp.Models;
using CaringSquareApp.Utils;
using Microsoft.AspNet.Identity;

namespace CaringSquareApp.Controllers
{
    public class SocialEventsController : Controller
    {
        private CaringSquareEntities db = new CaringSquareEntities();

        // GET: SocialEvents
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            //var socialEvents = db.SocialEvents.Include(s => s.AspNetUser).Include(s => s.POIs);
            return View(eventLists);
        }

        // GET: SocialEvents/Details/5
        public ActionResult Details(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if(alreadyExist == true)
            {
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }

        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        // GET: SocialEvents/Create
        public ActionResult Create()
        {
            
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email");
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name");

            return View();
        }

        // POST: SocialEvents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,EventName,EventDate,EventTime,UserUserId,POIPlaceId")] SocialEvent socialEvent)
        {
            if (ModelState.IsValid)
            {
                db.SocialEvents.Add(socialEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
            return View(socialEvent);
        }

        [Authorize]
        // GET: SocialEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {

                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if (alreadyExist == true)
            {
                ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
                ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
            
        }

        // POST: SocialEvents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,EventName,EventDate,EventTime,UserUserId,POIPlaceId")] SocialEvent socialEvent)
        {
            
            if (ModelState.IsValid)
            {
                db.Entry(socialEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserUserId = new SelectList(db.AspNetUsers, "Id", "Email", socialEvent.UserUserId);
            ViewBag.POIPlaceId = new SelectList(db.POIs, "PlaceId", "Name", socialEvent.POIPlaceId);
            return View(socialEvent);
        }

        [Authorize]
        // GET: SocialEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            var userId = User.Identity.GetUserId();
            var eventLists = db.SocialEvents.Where(s => s.UserUserId == userId).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            if (socialEvent == null)
            {
                return HttpNotFound();
            }
            bool alreadyExist = eventLists.Contains(socialEvent);
            if (alreadyExist == true)
            {
                return View(socialEvent);
            }
            else
            {
                return RedirectToAction("AccessDenied");
            }
            
        }

        // POST: SocialEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SocialEvent socialEvent = db.SocialEvents.Find(id);
            db.SocialEvents.Remove(socialEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        public ActionResult Share_Facebook()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;


                    EmailSender es = new EmailSender();
                    es.Send(toEmail, subject, contents);

                    ViewBag.Result = "Email has been sent successfully!";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }

        public ActionResult Send_Bulk_Email()
        {
            return View(new SendEmailViewModelMultipleRecepients());
        }

        [HttpPost]
        public ActionResult Send_Bulk_Email(SendEmailViewModelMultipleRecepients model)
        {
            
            if (ModelState.IsValid)
            {
                try
                {
                    var emails = model.ToEmail;
                    List<String> toEmail = emails.ToList();
                    String subject = model.Subject;
                    String contents = model.Contents;
                    EmailSender es = new EmailSender();
                    es.SendBulkEmail(toEmail, subject, contents);
                    ViewBag.Result = "Emails has been send.";
                    ModelState.Clear();
                    return View(new SendEmailViewModelMultipleRecepients());
                }
                catch
                {
                    return View();
                }
            }

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
