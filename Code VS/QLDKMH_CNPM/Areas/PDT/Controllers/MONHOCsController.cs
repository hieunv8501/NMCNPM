﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using OfficeOpenXml;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class MONHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/MONHOCs
        public ActionResult Index()
        {
            var mONHOCs = db.MONHOCs.Include(m => m.LOAIMONHOC);
            return View(mONHOCs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var mONHOC = new List<MONHOC>();
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
                            var mh = new MONHOC();
                            mh.MaMonHoc = workSheet.Cells[iRow, 1].Value.ToString();
                            mh.TenMonHoc = workSheet.Cells[iRow, 2].Value.ToString();
                            mh.MaLoaiMon = workSheet.Cells[iRow, 3].Value.ToString();
                            mh.SoTiet = Convert.ToInt32(workSheet.Cells[iRow, 4].Value.ToString());
                            mh.SoTinChi = Convert.ToInt32(workSheet.Cells[iRow, 5].Value.ToString());
                            mONHOC.Add(mh);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in mONHOC)
                {
                    excelImport.MONHOCs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "MONHOCs");
        }

        //Export danh sách môn học sang file excel
        public ActionResult ExportToExcel()
        {
            var mONHOCs = new System.Data.DataTable();
            mONHOCs.Columns.Add("Mã môn học", typeof(string));
            mONHOCs.Columns.Add("Tên môn học", typeof(string));
            mONHOCs.Columns.Add("Loại môn", typeof(string));
            mONHOCs.Columns.Add("Số tiết", typeof(int));
            mONHOCs.Columns.Add("Số tín chỉ", typeof(int));

            foreach (MONHOC item in db.MONHOCs)
            {
                mONHOCs.Rows.Add(item.MaMonHoc, item.TenMonHoc, item.LOAIMONHOC.TenLoaiMon, item.SoTiet, item.SoTinChi);
            }

            var grid = new GridView();
            grid.DataSource = mONHOCs;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachMonHoc.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");
        }
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            return View(mONHOC);
        }

        // GET: PDT/MONHOCs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiMon = new SelectList(db.LOAIMONHOCs, "MaLoaiMon", "TenLoaiMon");
            return View();
        }

        // POST: PDT/MONHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaMonHoc,TenMonHoc,MaLoaiMon,SoTiet,SoTinChi")] MONHOC mONHOC)
        {
            if (ModelState.IsValid)
            {
                db.MONHOCs.Add(mONHOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiMon = new SelectList(db.LOAIMONHOCs, "MaLoaiMon", "TenLoaiMon", mONHOC.MaLoaiMon);
            return View(mONHOC);
        }

        // GET: PDT/MONHOCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiMon = new SelectList(db.LOAIMONHOCs, "MaLoaiMon", "TenLoaiMon", mONHOC.MaLoaiMon);
            return View(mONHOC);
        }

        // POST: PDT/MONHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMonHoc,TenMonHoc,MaLoaiMon,SoTiet,SoTinChi")] MONHOC mONHOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiMon = new SelectList(db.LOAIMONHOCs, "MaLoaiMon", "TenLoaiMon", mONHOC.MaLoaiMon);
            return View(mONHOC);
        }

        // GET: PDT/MONHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONHOC mONHOC = db.MONHOCs.Find(id);
            if (mONHOC == null)
            {
                return HttpNotFound();
            }
            return View(mONHOC);
        }

        // POST: PDT/MONHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MONHOC mONHOC = db.MONHOCs.Find(id);
            db.MONHOCs.Remove(mONHOC);
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
