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
    public class PHIEUTHUsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/PHIEUTHUs
        public ActionResult Index()
        {
            var pHIEUTHUs = db.PHIEUTHUs.Include(p => p.PHIEU_DKHP);
            return View(pHIEUTHUs.ToList());
        }

        // GET: PDT/PHIEUTHUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // GET: PDT/PHIEUTHUs/Create
        public ActionResult Create()
        {
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV");
            return View();
        }

        // POST: PDT/PHIEUTHUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuThu,SoPhieuDKHP,NgayLap,SoTienThu")] PHIEUTHU pHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.PHIEUTHUs.Add(pHIEUTHU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", pHIEUTHU.SoPhieuDKHP);
            return View(pHIEUTHU);
        }

        // GET: PDT/PHIEUTHUs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", pHIEUTHU.SoPhieuDKHP);
            return View(pHIEUTHU);
        }

        // POST: PDT/PHIEUTHUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieuThu,SoPhieuDKHP,NgayLap,SoTienThu")] PHIEUTHU pHIEUTHU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEUTHU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", pHIEUTHU.SoPhieuDKHP);
            return View(pHIEUTHU);
        }

        // GET: PDT/PHIEUTHUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // POST: PDT/PHIEUTHUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            db.PHIEUTHUs.Remove(pHIEUTHU);
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
