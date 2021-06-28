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
    public class SINHVIENsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        //Hàm xem thông tin chi tiết của 1 sinh viên khi có được xác thực đăng nhập
        // GET: SV/SINHVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Tìm id (MSSV) và trả về view data
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }
        //Hàm sửa thông tin SV
        // GET: SINHVIEN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return View(sINHVIEN);
        }

        // POST: SINHVIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("SVHome","SV", new { TenDangNhap = sINHVIEN.MaSV });
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return RedirectToAction("SVHome", new { area ="SV", TenDangNhap = sINHVIEN.MaSV });
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
