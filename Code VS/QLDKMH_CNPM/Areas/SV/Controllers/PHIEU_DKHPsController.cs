using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.SV.Controllers
{
    public class PHIEU_DKHPsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: SV/PHIEU_DKHPs
        public ActionResult Index()
        {
            var pHIEU_DKHP = db.PHIEU_DKHPs.Include(p => p.HKNH).Include(p => p.SINHVIEN);
            return View(pHIEU_DKHP.ToList());
        }

        // GET: SV/PHIEU_DKHPs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHPs.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEU_DKHP);
        }

        // GET: SV/PHIEU_DKHPs/Create
        public ActionResult Create()
        {
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen");
            return View();
        }

        // POST: SV/PHIEU_DKHPs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        {
            if (ModelState.IsValid)
            {
                db.PHIEU_DKHPs.Add(pHIEU_DKHP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
            return View(pHIEU_DKHP);
        }

        // GET: SV/PHIEU_DKHPs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHPs.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
            return View(pHIEU_DKHP);
        }

        // POST: SV/PHIEU_DKHPs/Edit/5
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

        // GET: SV/PHIEU_DKHPs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHPs.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(pHIEU_DKHP);
        }

        // POST: SV/PHIEU_DKHPs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHPs.Find(id);
            db.PHIEU_DKHPs.Remove(pHIEU_DKHP);
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
