using FInalProject.DATA.EF;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FinalProject.UI.MVC.Controllers
{
    public class ApplicationsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: Applications
        [Authorize(Roles = "Admin, Manager, Employee")]
        public ActionResult Index()
        {
            if (User.IsInRole("Manager"))
            {
                var userId = User.Identity.GetUserId();
                var managerApps = db.Applications.Where(x => x.OpenPosition.Location.ManagerID == userId).Include(a => a.OpenPosition).Include(a => a.UserDetail);

                return View(managerApps.ToList());
            }
            if (User.IsInRole("Employee"))
            {
                var userId = User.Identity.GetUserId();
                var userApps = db.Applications.Where(x => x.UserID == userId).Include(a => a.OpenPosition).Include(a => a.UserDetail);

                return View(userApps.ToList());
            }
            var applications = db.Applications.Include(a => a.ApplicationStatu).Include(a => a.OpenPosition).Include(a => a.UserDetail);
            return View(applications.ToList());
        }

        // GET: Applications/Details/5
        [Authorize(Roles = "Admin, Manager, Employee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);

            if (application == null)
            {
                return HttpNotFound();
            }

            if (User.IsInRole("Manager") && application.ApplicationStatu.StatusName == "Pending")
            {
                application.ApplicationStatus = 2;
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(application);
        }

        // GET: Applications/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName");
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID");
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,ApplicationStatus,ResumeFilename,EmployeeNotes")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Applications.Add(application);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Edit([Bind(Include = "ApplicationID,OpenPositionID,UserID,ApplicationDate,ManagerNotes,ApplicationStatus,ResumeFilename,EmployeeNotes")] Application application)
        {
            if (ModelState.IsValid)
            {
                db.Entry(application).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationStatus = new SelectList(db.ApplicationStatus1, "ApplicationStatusID", "StatusName", application.ApplicationStatus);
            ViewBag.OpenPositionID = new SelectList(db.OpenPositions, "OpenPositionID", "OpenPositionID", application.OpenPositionID);
            ViewBag.UserID = new SelectList(db.UserDetails, "UserID", "FirstName", application.UserID);
            return View(application);
        }

        // GET: Applications/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Application application = db.Applications.Find(id);
            if (application == null)
            {
                return HttpNotFound();
            }
            return View(application);
        }

        // POST: Applications/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Application application = db.Applications.Find(id);
            db.Applications.Remove(application);
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
        [Authorize(Roles = "Employee")]
        public ActionResult OneClickApply(int id)
        {
            string userId = User.Identity.GetUserId();

            Application empApp = new Application();

            UserDetail userDetail = db.UserDetails.Where(x => x.UserID == userId).FirstOrDefault();

            if (userDetail.ResumeFilename != null)
            {
                empApp.UserID = userId;
                empApp.OpenPositionID = id;
                empApp.ApplicationDate = DateTime.Now;
                empApp.ApplicationStatus = 1;
                empApp.ResumeFilename = userDetail.ResumeFilename;
                db.Applications.Add(empApp);
                db.SaveChanges();

                return View("ApplicationConfirmed", empApp);
            }

            return RedirectToAction("ApplicationError");
        }

        public ActionResult ApplicationError()
        {
            return View();
        }
    }
}
