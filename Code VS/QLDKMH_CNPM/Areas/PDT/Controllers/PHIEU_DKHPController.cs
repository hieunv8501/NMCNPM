﻿using System;
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
    public class PHIEU_DKHPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/PHIEU_DKHP
        public ActionResult Index()
        {
            var pHIEU_DKHP = db.PHIEU_DKHP.Include(p => p.HKNH).Include(p => p.SINHVIEN);
            return View(pHIEU_DKHP.ToList());
        }

        // GET: PDT/PHIEU_DKHP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEU_DKHP);
        }

        // GET: PDT/PHIEU_DKHP/Create
        public ActionResult Create()
        {
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen");
            return View();
        }

        // POST: PDT/PHIEU_DKHP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        {
            if (ModelState.IsValid)
            {
                db.PHIEU_DKHP.Add(pHIEU_DKHP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
            return View(pHIEU_DKHP);
        }

        // GET: PDT/PHIEU_DKHP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
            return View(pHIEU_DKHP);
        }

        // POST: PDT/PHIEU_DKHP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pHIEU_DKHP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
            return View(pHIEU_DKHP);
        }

        // GET: PDT/PHIEU_DKHP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEU_DKHP);
        }

        // POST: PDT/PHIEU_DKHP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            db.PHIEU_DKHP.Remove(pHIEU_DKHP);
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
