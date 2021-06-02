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
    public class CT_PHIEU_DKHPsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/CT_PHIEU_DKHPs
        public ActionResult Index()
        {
            var cT_PHIEU_DKHP = db.CT_PHIEU_DKHPs.Include(c => c.DS_MONHOC_MO).Include(c => c.PHIEU_DKHP);
            return View(cT_PHIEU_DKHP.ToList());
        }

        // GET: PDT/CT_PHIEU_DKHPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHPs.Find(id);
            if (cT_PHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEU_DKHP);
        }

        // GET: PDT/CT_PHIEU_DKHPs/Create
        public ActionResult Create()
        {
            ViewBag.MaMo = new SelectList(db.DS_MONHOC_MOs, "MaMo", "MaMonHoc");
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV");
            return View();
        }

        // POST: PDT/CT_PHIEU_DKHPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuDKHP,MaMo,GhiChu")] CT_PHIEU_DKHP cT_PHIEU_DKHP)
        {
            if (ModelState.IsValid)
            {
                db.CT_PHIEU_DKHPs.Add(cT_PHIEU_DKHP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMo = new SelectList(db.DS_MONHOC_MOs, "MaMo", "MaMonHoc", cT_PHIEU_DKHP.MaMo);
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", cT_PHIEU_DKHP.SoPhieuDKHP);
            return View(cT_PHIEU_DKHP);
        }

        // GET: PDT/CT_PHIEU_DKHPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHPs.Find(id);
            if (cT_PHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMo = new SelectList(db.DS_MONHOC_MOs, "MaMo", "MaMonHoc", cT_PHIEU_DKHP.MaMo);
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", cT_PHIEU_DKHP.SoPhieuDKHP);
            return View(cT_PHIEU_DKHP);
        }

        // POST: PDT/CT_PHIEU_DKHPs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieuDKHP,MaMo,GhiChu")] CT_PHIEU_DKHP cT_PHIEU_DKHP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cT_PHIEU_DKHP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMo = new SelectList(db.DS_MONHOC_MOs, "MaMo", "MaMonHoc", cT_PHIEU_DKHP.MaMo);
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHPs, "SoPhieuDKHP", "MaSV", cT_PHIEU_DKHP.SoPhieuDKHP);
            return View(cT_PHIEU_DKHP);
        }

        // GET: PDT/CT_PHIEU_DKHPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHPs.Find(id);
            if (cT_PHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEU_DKHP);
        }

        // POST: PDT/CT_PHIEU_DKHPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHPs.Find(id);
            db.CT_PHIEU_DKHPs.Remove(cT_PHIEU_DKHP);
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
