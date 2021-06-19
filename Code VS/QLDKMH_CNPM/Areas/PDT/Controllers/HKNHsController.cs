using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class HKNHsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/HKNHs
        public ActionResult Index()
        {
            return View(db.HKNHs.ToList());
        }

        // GET: PDT/HKNHs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HKNH hKNH = db.HKNHs.Find(id);
            if (hKNH == null)
            {
                return HttpNotFound();
            }
            return View(hKNH);
        }

        // GET: PDT/HKNHs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PDT/HKNHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHKNH,HocKy,Nam1,Nam2,HanDongHocPhi")] HKNH hKNH)
        {
            if (ModelState.IsValid)
            {
                db.HKNHs.Add(hKNH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hKNH);
        }

        // GET: PDT/HKNHs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HKNH hKNH = db.HKNHs.Find(id);
            if (hKNH == null)
            {
                return HttpNotFound();
            }
            return View(hKNH);
        }

        // POST: PDT/HKNHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHKNH,HocKy,Nam1,Nam2,HanDongHocPhi")] HKNH hKNH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hKNH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hKNH);
        }

        // GET: PDT/HKNHs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HKNH hKNH = db.HKNHs.Find(id);
            if (hKNH == null)
            {
                return HttpNotFound();
            }
            return View(hKNH);
        }

        // POST: PDT/HKNHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HKNH hKNH = db.HKNHs.Find(id);
            db.HKNHs.Remove(hKNH);
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
