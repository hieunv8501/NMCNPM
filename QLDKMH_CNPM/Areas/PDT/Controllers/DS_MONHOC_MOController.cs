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
    public class DS_MONHOC_MOController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/DS_MONHOC_MO
        public ActionResult Index()
        {
            var dS_MONHOC_MO = db.DS_MONHOC_MO.Include(d => d.HKNH).Include(d => d.MONHOC);
            return View(dS_MONHOC_MO.ToList());
        }

        // GET: PDT/DS_MONHOC_MO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            return View(dS_MONHOC_MO);
        }

        // GET: PDT/DS_MONHOC_MO/Create
        public ActionResult Create()
        {
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            return View();
        }

        // POST: PDT/DS_MONHOC_MO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMo,MaHKNH,MaMonHoc")] DS_MONHOC_MO dS_MONHOC_MO)
        {
            if (ModelState.IsValid)
            {
                db.DS_MONHOC_MO.Add(dS_MONHOC_MO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        // GET: PDT/DS_MONHOC_MO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        // POST: PDT/DS_MONHOC_MO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMo,MaHKNH,MaMonHoc")] DS_MONHOC_MO dS_MONHOC_MO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dS_MONHOC_MO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        // GET: PDT/DS_MONHOC_MO/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            return View(dS_MONHOC_MO);
        }

        // POST: PDT/DS_MONHOC_MO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            db.DS_MONHOC_MO.Remove(dS_MONHOC_MO);
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
