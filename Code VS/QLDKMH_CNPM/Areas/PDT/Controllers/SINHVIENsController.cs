using System;
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
        //public ActionResult Upload(FormCollection formCollection)
        //{
        //    var sINHVIEN = new List<SINHVIEN>();
        //    if (Request != null)
        //    {
        //        HttpPostedFileBase file = Request.Files["UploadedFile"];
        //        if ((file != null) && (file.ContentLength > 0) && !string.IsNullOrEmpty(file.FileName))
        //        {
        //            string fileName = file.FileName;
        //            string fileContentType = file.ContentType;
        //            byte[] fileBytes = new byte[file.ContentLength];
        //            var data = file.InputStream.Read(fileBytes, 0, Convert.ToInt32(file.ContentLength));
        //            using (var package = new ExcelPackage(file.InputStream))
        //            {
        //                var currentSheet = package.Workbook.Worksheets;
        //                var workSheet = currentSheet.First();
        //                var SL_Col = workSheet.Dimension.End.Column;
        //                var SL_Row = workSheet.Dimension.End.Row;
        //                for (int iRow = 2; iRow <= SL_Row; iRow++)
        //                {
        //                    var sinhvien = new SINHVIEN();
        //                    sinhvien.MaSV = workSheet.Cells[iRow, 1].Value.ToString();
        //                    sinhvien.HoTen = workSheet.Cells[iRow, 2].Value.ToString();
        //                    DateTime datetime = workSheet.Cells[iRow, 3].GetValue();

        //                    sinhvien.NgaySinh = type;
        //                    sinhvien.GioiTinh = workSheet.Cells[iRow, 4].Value.ToString();
        //                    sinhvien.MaNganh = workSheet.Cells[iRow, 5].Value.ToString();
        //                    sinhvien.MaDoiTuong = workSheet.Cells[iRow, 6].Value.ToString();
        //                    sinhvien.Huyen = workSheet.Cells[iRow, 7].Value.ToString();
        //                    sINHVIEN.Add(sinhvien);
        //                }
        //            }
        //        }
        //    }
        //    using (CNPM_DBContext excelImport = new CNPM_DBContext())
        //    {
        //        foreach (var item in sINHVIEN)
        //        {
        //            excelImport.SINHVIENs.Add(item);
        //        }
        //        excelImport.SaveChanges();
        //    }
        //    return RedirectToAction("Index", "SINHVIENs");
        //}

        // GET: PDT/SINHVIENs/Details/5
        public ActionResult Details(string id)
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

        public JsonResult GetHuyen(string id)
        {
            var ddlHuyen = db.HUYENs.Where(x => x.MaTinh == id).ToList();
            List<SelectListItem> listHuyen = new List<SelectListItem>();

            listHuyen.Add(new SelectListItem { Text = "-- Chọn Huyện --", Value = "0" });
            if (ddlHuyen != null)
            {
                foreach (var x in ddlHuyen)
                {
                    listHuyen.Add(new SelectListItem { Text = x.TenHuyen, Value = x.MaHuyen.ToString() });
                }
            }
            return Json(new SelectList(listHuyen, "Value", "Text", JsonRequestBehavior.AllowGet));
        }

        // GET: DateTimePicker


        // GET: PDT/SINHVIENs/Create/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
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
            catch (Exception)
            {
                return RedirectToAction("Create", "SINHVIENs", new { code = 1 });
            }
        }

        // GET: PDT/SINHVIENs/Edit/5
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
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
            catch (Exception)
            {
                return RedirectToAction("Edit", "SINHVIENs", new { id = sINHVIEN.MaSV, code = 1 });
            }
        }

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
