﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class SINHVIENsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/SINHVIENs
        public ActionResult Index()
        {
            var sINHVIENs = db.SINHVIENs.Include(s => s.DOITUONG).Include(s => s.HUYEN).Include(s => s.NGANH);
            return View(sINHVIENs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var sINHVIEN = new List<SINHVIEN>();
            if (Request != null)
            {
                HttpPostedFileBase file = Request.Files["UploadedFile"];
                if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
                {
                    string fileName = file.FileName;
                    string fileContentType = file.ContentType;
                    byte[] fileBytes = new byte[file.ContentLength];
                    var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
                    using (var package = new ExcelPackage(file.InputStream))
                    {
                        var currentSheet = package.Workbook.Worksheets;
                        var workSheet = currentSheet.First();
                        var SL_Col = workSheet.Dimension.End.Column;
                        var SL_Row = workSheet.Dimension.End.Row;
                        for (int iRow = 2; iRow <= SL_Row; iRow++)
                        {
                            var sv = new SINHVIEN();
                            sv.MaSV = workSheet.Cells[iRow, 1].Value.ToString();
                            sv.HoTen = workSheet.Cells[iRow, 2].Value.ToString();
                            sv.NgaySinh = Convert.ToDateTime(workSheet.Cells[iRow, 3].Value.ToString());
                            sv.GioiTinh = workSheet.Cells[iRow, 4].Value.ToString();
                            sv.MaNganh = workSheet.Cells[iRow, 5].Value.ToString();
                            sv.MaDoiTuong = workSheet.Cells[iRow, 6].Value.ToString();
                            sv.MaHuyen = workSheet.Cells[iRow, 7].Value.ToString();
                            sINHVIEN.Add(sv);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in sINHVIEN)
                {
                    excelImport.SINHVIENs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "SINHVIENs");
        }

        // GET: PDT/SINHVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Xem chi tiết sinh viên
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        //Đây là function để khi ta chọn được Tỉnh ở combobox dropdownlist thì sẽ load dữ liệu lên combobox dropdownlist của Huyện
        public JsonResult GetHuyen(string id)
        {
            //Tìm những huyện có cùng mã tỉnh
            var ddlHuyen = db.HUYENs.Where(x => x.MaTinh == id).ToList();

            //Tạo mới list Huyện
            List<SelectListItem> listHuyen = new List<SelectListItem>();
            listHuyen.Add(new SelectListItem { Text = "-- Chọn Huyện --", Value = "0" });

            //Add từng huyện trong ddlHuyen vào trong listHuyen
            if (ddlHuyen != null)
            {
                foreach (var x in ddlHuyen)
                {
                    listHuyen.Add(new SelectListItem { Text = x.TenHuyen, Value = x.MaHuyen.ToString() });
                }
            }
            //Trả về kết quả
            return Json(new SelectList(listHuyen, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        // GET: PDT/SINHVIENs/Create/5
        //Phương thức GET giao diện tạo mới sinh viên
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";

            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong");
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh");
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen");
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh");
            return View();
        }

        // POST: PDT/SINHVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //Phương thức POST giao diện tạo mới sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
            //Gọi hàm nhập thông tin SV bởi các trường dữ liệu nếu đúng thì trả về Index
            try
            {
                if (ModelState.IsValid)
                {
                    db.SINHVIENs.Add(sINHVIEN);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
                ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.HUYEN.MaTinh);
                ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
                ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
                return View(sINHVIEN);
            }
            // Gọi hàm nhập thông tin SV bởi các trường dữ liệu, nếu sai thì thông báo và nhập lại
            catch (Exception)
            {
                return RedirectToAction("Create", "SINHVIENs", new { code = 1 });
            }
        }

        // GET: PDT/SINHVIENs/Edit/5
        //  Gọi hàm sửathông tin SV bởi các trường dữ liệu với method GET
        public ActionResult Edit(string id, int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
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
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return View(sINHVIEN);
        }

        // POST: PDT/SINHVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // Gọi hàm sửathông tin SV bởi các trường dữ liệu với method POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
            //Gọi sửa thông tin SV bởi các trường dữ liệu với method POST, nếu đúng trả về Index
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sINHVIEN).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
                ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
                ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
                ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
                return View(sINHVIEN);
            }
            //Gọi sửa thông tin SV bởi các trường dữ liệu với method POST, nếu sai thì thông báo và nhập lại
            catch (Exception)
            {
                return RedirectToAction("Edit", "SINHVIENs", new { id = sINHVIEN.MaSV, code = 1 });
            }
        }
        //Gọi hàm xóa thông tin SV bởi các trường dữ liệu với method GET, sau đó thì lưu lại với method POST
        // GET: PDT/SINHVIENs/Delete/5
        public ActionResult Delete(string id)
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
            return View(sINHVIEN);
        }

        // POST: PDT/SINHVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
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
