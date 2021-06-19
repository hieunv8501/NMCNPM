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
    public class PHIEU_DKHPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();
        private string TenDangNhap;

        // GET: SV/PHIEU_DKHP
        public ActionResult IndexSV(string id)
        {
            TenDangNhap = id;
            ViewData["TenDangNhap"] = id;
            var pHIEU_DKHP = db.PHIEU_DKHP.Include(p => p.HKNH).Include(p => p.SINHVIEN).Where(p => p.MaSV == id);
            return View(pHIEU_DKHP.ToList());
        }

        // GET: SV/PHIEU_DKHP/Details/5
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
            ViewData["TenDangNhap"] = pHIEU_DKHP.MaSV;
            ViewBag.CT_PHIEU_DKHPandDS_MONHOC_MO = db.CT_PHIEU_DKHP.Where(m => m.SoPhieuDKHP == id).ToList();
            return View(pHIEU_DKHP);
        }

        // GET: SV/PHIEU_DKHP/Create
        public ActionResult Create(string id_sv, int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";

            TenDangNhap = id_sv;
            ViewData["TenDangNhap"] = id_sv;
            ViewBag.MaSV = db.SINHVIENs.Find(id_sv); //Tìm sinh viên có mã sv tương ứng
            ViewBag.HocKyNamHoc = db.HKNHs; // Lấy hết thông tin học kỳ năm học vào viewbag
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
            pHIEU_DKHP.MaSV = ViewBag.MaSV.MaSV; //Gán mặc định sv khi lập bc dựa vào id_sv đầu vào
            pHIEU_DKHP.SoPhieuDKHP = ViewBag.SoPhieuDKHP; //Tạo id tự tăng cho phiếu đk
            pHIEU_DKHP.NgayLap = DateTime.Now; //Gán ngày đk mặc định là hôm nay
            ViewBag.NgayLap = pHIEU_DKHP.NgayLap;

            pHIEU_DKHP.TongTCLT = 0;
            pHIEU_DKHP.TongTCTH = 0;
            pHIEU_DKHP.TongTienDangKy = 0;
            pHIEU_DKHP.TongTienPhaiDong = 0;
            pHIEU_DKHP.TongTienDaDong = 0;
            pHIEU_DKHP.SoTienConLai = 0;
            //ViewBag.TongTCLT = pHIEU_DKHP.TongTCLT;
            //ViewBag.TongTCTH = pHIEU_DKHP.TongTCTH;
            //ViewBag.TongTienDangKy = pHIEU_DKHP.TongTienDangKy;
            //ViewBag.TongTienPhaiDong = pHIEU_DKHP.TongTienPhaiDong;
            //ViewBag.TongTienDaDong = pHIEU_DKHP.TongTienDaDong;
            //ViewBag.SoTienConLai = pHIEU_DKHP.SoTienConLai;
            return View(pHIEU_DKHP);
        }

        // POST: SV/PHIEU_DKHP/Create
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
                    return RedirectToAction("Create", "CT_PHIEU_DKHP", new { id = pHIEU_DKHP.SoPhieuDKHP, HKNH = pHIEU_DKHP.MaHKNH, mssv = pHIEU_DKHP.MaSV });
                }

                ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "HocKy", pHIEU_DKHP.MaHKNH);
                ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
                return View(pHIEU_DKHP);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "PHIEU_DKHP", new { id_sv = pHIEU_DKHP.MaSV, code = 1 });
            }
        }

        // GET: SV/PHIEU_DKHP/Edit/5
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

        // POST: SV/PHIEU_DKHP/Edit/5
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

        //// GET: SV/PHIEU_DKHP/Delete/5
        //public ActionResult Delete(int? id)
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
        //    return View(pHIEU_DKHP);
        //}

        //// POST: SV/PHIEU_DKHP/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
        //    db.PHIEU_DKHP.Remove(pHIEU_DKHP);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
        public ActionResult TraCuu(int? SoPhieuDKHP, string MSSV, int? mhknh, int? day, int? month, int? year)
        {
            var pHIEU_DKHP = db.PHIEU_DKHP.Where(x => (x.SoPhieuDKHP == SoPhieuDKHP || SoPhieuDKHP == null) && (x.MaSV == MSSV || MSSV == "" || MSSV == null) && (x.HKNH.HocKy == mhknh || mhknh == null) && (x.NgayLap.Day == day || day == null) && (x.NgayLap.Month == month || month == null) && (x.NgayLap.Year == year || year == null));
            return View(pHIEU_DKHP.ToList());
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
