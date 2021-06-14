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
    public class DSSV_CHUAHOANTHANH_HPController : Controller
    {
         private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Index/5
        public ActionResult Index()
        {
            IList<DS_model> DS = new List<DS_model>();
            var model = from a in db.DSSV_CHUAHOANTHANH_HP
                        join b in db.SINHVIENs on a.MaSV equals b.MaSV
                        join c in db.HKNHs on a.MaHKNH equals c.MaHKNH
                        join d in db.PHIEU_DKHP on a.MaSV equals d.MaSV
                        where d.SoTienConLai > 0
                        select new DS_model()
                        {
                            MaHKNH = a.MaHKNH,
                            MaSV = b.MaSV,
                            HoTen = b.HoTen,
                            MaNganh = b.MaNganh,
                            HocKy = c.HocKy,
                            Nam1 = c.Nam1,
                            Nam2 = c.Nam2,
                            SoTienConLai = (decimal)d.SoTienConLai
                        };
            DS = model.ToList();
            return View(DS);
        }
        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Create
        public ActionResult Create()
        {
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen");
            return View();
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        {
            if (ModelState.IsValid)
            {
                db.DSSV_CHUAHOANTHANH_HP.Add(dSSV_CHUAHOANTHANH_HP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dSSV_CHUAHOANTHANH_HP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            db.DSSV_CHUAHOANTHANH_HP.Remove(dSSV_CHUAHOANTHANH_HP);
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
