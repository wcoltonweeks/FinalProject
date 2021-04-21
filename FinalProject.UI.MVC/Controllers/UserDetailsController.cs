using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalProject.UI.MVC.Utilities;
using FInalProject.DATA.EF;

namespace FinalProject.UI.MVC.Controllers
{
    public class UserDetailsController : Controller
    {
        private JobBoardEntities db = new JobBoardEntities();

        // GET: UserDetails
        public ActionResult Index()
        {
            return View(db.UserDetails.ToList());
        }

        // GET: UserDetails/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,FirstName,LastName,ResumeFilename,Photo")] UserDetail userDetail)
        {
            if (ModelState.IsValid)
            {
                db.UserDetails.Add(userDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDetail);
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,FirstName,LastName,ResumeFilename,Photo")] UserDetail userDetail, HttpPostedFileBase resume, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                #region CustomUserDetails/ResumeUpload
                string file = null;
                string picfile = "NoImage.png";

                if (resume != null)
                {
                    file = resume.FileName;
                    string ext = file.Substring(file.LastIndexOf('.'));
                    string[] goodExts = { ".pdf", ".docx" }; //good file extensions. add more good extensions later
                    if (goodExts.Contains(ext.ToLower()) && resume.ContentLength <= 4194304)
                    {
                        string newName = Guid.NewGuid() + ext;
                        resume.SaveAs(Server.MapPath("~/Content/Documents/") + newName);
                        file = newName;//Creates a new file name to be stored in the DB and store the pdf in the specified path
                    }
                }



                #endregion

                
               
                if (photo != null)
                {
                    picfile = photo.FileName;
                    string ext = picfile.Substring(picfile.LastIndexOf('.'));
                    string[] goodExts = { ".jpeg", ".jpg", ".png", ".gif" };
                    
                    if (goodExts.Contains(ext.ToLower()) && photo.ContentLength <= 4194304)
                    {
                        //greate a new file name using a GUID
                        picfile = Guid.NewGuid() + ext;
                    
                        string savePath = Server.MapPath("~/Content/images/Photos/");

                        Image convertedImage = Image.FromStream(photo.InputStream);
                        int maxImageSize = 500; 
                        int maxThumbSize = 100; 

                        ImageService.ResizeImage(savePath, picfile, convertedImage, maxImageSize, maxThumbSize);
                        
                    }
                   
                }
              

                userDetail.ResumeFilename = file;
                userDetail.Photo = picfile;
               
                db.Entry(userDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id=userDetail.UserID});
            }
            return View(userDetail);
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDetail userDetail = db.UserDetails.Find(id);
            if (userDetail == null)
            {
                return HttpNotFound();
            }
            return View(userDetail);
        }

        // POST: UserDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserDetail userDetail = db.UserDetails.Find(id);
            db.UserDetails.Remove(userDetail);
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
