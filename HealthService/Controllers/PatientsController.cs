using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthService.Models;
using HealthService.cs;
using HealthService.CustomAuthentication;
using HealthService.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Security;

namespace HealthService.Controllers
{
    [CustomAuthorize(Roles = "systemadmin, admin, upazilla user")]
    public class PatientsController : Controller
    {
        private HealthServiceContext db = new HealthServiceContext();

        // GET: Patients
        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            User user = db.Users.Where(r => r.Username == username).FirstOrDefault();
            if (user.Roles.FirstOrDefault().RoleName == "systemadmin" || user.Roles.FirstOrDefault().RoleName == "admin")
            {
                ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name");
                ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");
                var patient = db.Patient.Include(p => p.Disease).Include(p => p.Upazilla);
                return View(patient.ToList());
            }
            else
            {
                ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name");
                ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");
                var patient = db.Patient.Where(r => r.UserId == user.UserId).Include(p => p.Disease).Include(p => p.Upazilla);
                return View(patient.ToList());
            }

        }

        [HttpPost]
        public ActionResult Index(int? DiseaseId, DateTime? from, DateTime? to)
        {
            HealthServiceContext dbcontext = new HealthServiceContext();
            string sql = "select * from Patient where 1=1";
            if(DiseaseId != null)
            {
                sql += "and DiseaseId = " + DiseaseId;
            }
            if(from != null)
            {
                sql += "and registrationdate >= '" + from + "'";
            }
            if (to != null)
            {
                sql += "and registrationdate <= '" + to + "'";
            }
            var list = dbcontext.Patient.SqlQuery(sql).ToList();
            ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name");
            ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name");
            return View(list);
        }
        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // GET: Patients/Create
        [CustomAuthorize(Roles = "upazilla user")]
        public ActionResult Create()
        {
           
            ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "upazilla user")]
        public ActionResult Create(Patient patient)
        {
            //int Id = System.Web.HttpContext.Current.User.Identity.GetUserId<int>();
            if (ModelState.IsValid)
            {
                var username = User.Identity.GetUserName();
                User user = db.Users.Where(r => r.Username == username).FirstOrDefault();
                patient.UpazillaId = user.UpazillaId;
                patient.UserId = user.UserId;
                patient.entrydate = DateTime.Now;
                patient.Status = 0;
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name", patient.DiseaseId);
            //ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name", patient.UpazillaId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        [CustomAuthorize(Roles = "systemadmin, admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name", patient.DiseaseId);
            ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name", patient.UpazillaId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "systemadmin, admin")]
        public ActionResult Edit(Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiseaseId = new SelectList(db.Disease, "Id", "Name", patient.DiseaseId);
            //ViewBag.UpazillaId = new SelectList(db.Upazilla, "Id", "Name", patient.UpazillaId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        [CustomAuthorize(Roles = "systemadmin, admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return HttpNotFound();
            }
            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "systemadmin, admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Patient patient = db.Patient.Find(id);
            db.Patient.Remove(patient);
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
