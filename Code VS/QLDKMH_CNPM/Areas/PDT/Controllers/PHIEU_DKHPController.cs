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
            ViewBag.CT_PHIEU_DKHPandDS_MONHOC_MO = db.CT_PHIEU_DKHP.Where(m => m.SoPhieuDKHP == id).ToList();
            return View(pHIEU_DKHP);
        }

        // GET: PDT/PHIEU_DKHP/Create
        public ActionResult Create(int code = 1)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";

            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "MaSV");
            ViewBag.HocKyNamHoc = db.HKNHs;
            int sOPHIEUDKHP = 1;
            if (db.PHIEU_DKHP.Count() != 0)
            {
                var SoPhieu_last = db.PHIEU_DKHP.OrderByDescending(x => x.SoPhieuDKHP).FirstOrDefault(); //tìm số phiếu cuối cùng trong database
                sOPHIEUDKHP = SoPhieu_last.SoPhieuDKHP + 1; //tăng sô phiếu cuối lên 1
            }
            //if (db.PHIEU_DKHP.Max(x => (int?)x.SoPhieuDKHP).Equals(null))
            //{
            //    PHIEU_DKHP.SoPhieuDKHP = 1;
            //}
            //else
            //{
            //    pHIEU_DKHP.SoPhieuDKHP = db.PHIEU_DKHP.Max(x => x.SoPhieuDKHP) + 1;
            //}
            ViewBag.SoPhieuDKHP = sOPHIEUDKHP;
            PHIEU_DKHP pHIEU_DKHP = new PHIEU_DKHP();
            pHIEU_DKHP.SoPhieuDKHP = ViewBag.SoPhieuDKHP;
            pHIEU_DKHP.NgayLap = DateTime.Now;
            ViewBag.NgayLap = pHIEU_DKHP.NgayLap;

            pHIEU_DKHP.TongTCLT = 0;
            pHIEU_DKHP.TongTCTH = 0;
            pHIEU_DKHP.TongTienDangKy = 0;
            pHIEU_DKHP.TongTienPhaiDong = 0;
            pHIEU_DKHP.TongTienDaDong = 0;
            pHIEU_DKHP.SoTienConLai = 0;
            ViewBag.TongTCLT = pHIEU_DKHP.TongTCLT;
            ViewBag.TongTCTH = pHIEU_DKHP.TongTCTH;
            ViewBag.TongTienDangKy = pHIEU_DKHP.TongTienDangKy;
            ViewBag.TongTienPhaiDong = pHIEU_DKHP.TongTienPhaiDong;
            ViewBag.TongTienDaDong = pHIEU_DKHP.TongTienDaDong;
            ViewBag.SoTienConLai = pHIEU_DKHP.SoTienConLai;
            return View(pHIEU_DKHP);
        }

        // POST: PDT/PHIEU_DKHP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.PHIEU_DKHP.Add(pHIEU_DKHP);
                    db.SaveChanges();
                    return RedirectToAction("Create", "CT_PHIEU_DKHP", new { id = pHIEU_DKHP.SoPhieuDKHP, HKNH = pHIEU_DKHP.MaHKNH });
                }

                ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "HocKy", pHIEU_DKHP.MaHKNH);
                ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
                return View(pHIEU_DKHP);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "PHIEU_DKHP", new { code = 1 });
            }
        }

        // GET: PDT/PHIEU_DKHP/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
        //    if (pHIEU_DKHP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
        //    return View(pHIEU_DKHP);
        //}

        // POST: PDT/PHIEU_DKHP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(pHIEU_DKHP).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
        //    return View(pHIEU_DKHP);
        //}

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
            ViewBag.id = id;
            return View(pHIEU_DKHP);
        }

        // POST: PDT/CT_PHIEU_DKHP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            db.PHIEU_DKHP.Remove(pHIEU_DKHP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult TraCuu(int? SoPhieuDKHP, string MSSV, int? MHKNH, int? Day, int? Month, int? Year, int? TinhTrang)
        {
            ViewBag.HocKyNamHoc = db.HKNHs;
            //Nếu người dùng không nhập gì thì không hiện gì cả
            if (SoPhieuDKHP == null && (MSSV == null || MSSV == "") && MHKNH == null && Day == null && Month == null && Year == null && TinhTrang == null)
            {
                var EmtyList = new List<PHIEU_DKHP>();
                return View(EmtyList);
            }
            else
            {
                var pHIEU_DKHP = db.PHIEU_DKHP.Where(x => (x.SoPhieuDKHP == SoPhieuDKHP || SoPhieuDKHP == null) && (x.MaSV == MSSV || MSSV == "" || MSSV == null) && (x.HKNH.MaHKNH == MHKNH || MHKNH == null) && (x.NgayLap.Day == Day || Day == null) && (x.NgayLap.Month == Month || Month == null) && (x.NgayLap.Year == Year || Year == null) && (((TinhTrang == 1) ? (x.SoTienConLai == 0) : (x.SoTienConLai > 0)) || TinhTrang == null));
                return View(pHIEU_DKHP.ToList());
            }
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
