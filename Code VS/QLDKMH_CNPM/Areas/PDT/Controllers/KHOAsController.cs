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
    public class KHOAsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/KHOAs
        public ActionResult Index()
        {
            //Hàm load danh sách khoa
            return View(db.KHOAs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var kHOA = new List<KHOA>();
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
                            var khoa = new KHOA();
                            khoa.MaKhoa = workSheet.Cells[iRow, 1].Value.ToString();
                            khoa.TenKhoa = workSheet.Cells[iRow, 2].Value.ToString();
                            kHOA.Add(ng);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in kHOA)
                {
                    excelImport.NGANHs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "KHOAs");
        }

        //Hàm load chi tiết khoa
        // GET: PDT/KHOAs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA kHOA = db.KHOAs.Find(id);
            if (kHOA == null)
            {
                return HttpNotFound();
            }
            return View(kHOA);
        }

        //Hàm tạo mới khoa, nếu đúng thì lưu lại, sai thì thông báo và nhập lại
        // GET: PDT/KHOAs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            return View();
        }

        // POST: PDT/KHOAs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhoa,TenKhoa")] KHOA kHOA)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.KHOAs.Add(kHOA);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(kHOA);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "KHOAs", new { code = 1 });
            }
        }

        //Hàm sửa thông tin khoa, nếu đúng thì lưu lại, sai thì thông báo và nhập lại
        // GET: PDT/KHOAs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA kHOA = db.KHOAs.Find(id);
            if (kHOA == null)
            {
                return HttpNotFound();
            }
            return View(kHOA);
        }

        // POST: PDT/KHOAs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhoa,TenKhoa")] KHOA kHOA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHOA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHOA);
        }
        //Hàm xóa thông tin khoa, rồi lưu lại
        // GET: PDT/KHOAs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHOA kHOA = db.KHOAs.Find(id);
            if (kHOA == null)
            {
                return HttpNotFound();
            }
            return View(kHOA);
        }

        // POST: PDT/KHOAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            KHOA kHOA = db.KHOAs.Find(id);
            db.KHOAs.Remove(kHOA);
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
