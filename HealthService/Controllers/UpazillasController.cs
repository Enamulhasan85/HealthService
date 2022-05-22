using HealthService.cs;
using HealthService.CustomAuthentication;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HealthService.Models;
namespace HealthService.Controllers
{
    [CustomAuthorize(Roles = "systemadmin, admin")]
    public class UpazillasController : Controller
    {
        private HealthServiceContext db = new HealthServiceContext();

        // GET: Upazillas
        public ActionResult Index()
        {
            return View(db.Upazilla.ToList());
        }

        // GET: Upazillas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upazilla upazilla = db.Upazilla.Find(id);
            if (upazilla == null)
            {
                return HttpNotFound();
            }
            return View(upazilla);
        }

        // GET: Upazillas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Upazillas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Upazilla upazilla)
        {
            if (ModelState.IsValid)
            {
                db.Upazilla.Add(upazilla);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(upazilla);
        }

        // GET: Upazillas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upazilla upazilla = db.Upazilla.Find(id);
            if (upazilla == null)
            {
                return HttpNotFound();
            }
            return View(upazilla);
        }

        // POST: Upazillas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Upazilla upazilla)
        {
            if (ModelState.IsValid)
            {
                db.Entry(upazilla).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(upazilla);
        }

        // GET: Upazillas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Upazilla upazilla = db.Upazilla.Find(id);
            if (upazilla == null)
            {
                return HttpNotFound();
            }
            return View(upazilla);
        }

        // POST: Upazillas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Upazilla upazilla = db.Upazilla.Find(id);
            db.Upazilla.Remove(upazilla);
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
