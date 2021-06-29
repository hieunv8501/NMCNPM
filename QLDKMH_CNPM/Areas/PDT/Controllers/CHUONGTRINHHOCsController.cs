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
    public class CHUONGTRINHHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/CHUONGTRINHHOCs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            return View(db.NGANHs.ToList());
        }
        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var cHUONGTRINHHOC = new List<CHUONGTRINHHOC>();
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
                            var cth = new CHUONGTRINHHOC();
                            cth.MaNganh = workSheet.Cells[iRow, 1].Value.ToString();
                            cth.MaMonHoc = workSheet.Cells[iRow, 2].Value.ToString();
                            cth.HocKy = Convert.ToInt32(workSheet.Cells[iRow, 3].Value.ToString());
                            cth.GhiChu = workSheet.Cells[iRow, 4].Value.ToString();
                            cHUONGTRINHHOC.Add(cth);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in cHUONGTRINHHOC)
                {
                    excelImport.CHUONGTRINHHOCs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "CHUONGTRINHHOCs", new {code = 10});
        }

        // GET: PDT/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id, int code = 0)
        {
            ViewBag.code = code;
            ViewBag.TenNganh = db.NGANHs.Find(id).TenNganh;
            ViewBag.MaNganh = db.NGANHs.Find(id).MaNganh;
            return View(db.CHUONGTRINHHOCs.Where(x => x.MaNganh == id).ToList());
        }

        // GET: PDT/CHUONGTRINHHOCs/Create
        public ActionResult Create(string id, int code = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.code = code;
            ViewBag.MONHOC = db.MONHOCs.ToList();
            ViewBag.MaNganh = db.NGANHs.Find(id).MaNganh;
            ViewBag.TenNganh = db.NGANHs.Find(id).TenNganh;
            return View();
        }

        // POST: PDT/CHUONGTRINHHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, [Bind(Include = "MaNganh,MaMonHoc,HocKy,GhiChu")] CHUONGTRINHHOC cHUONGTRINHHOC)
        {
            if (ModelState.IsValid)
            {
                if (db.CHUONGTRINHHOCs.Where(x => x.MaNganh == id && x.MaMonHoc == cHUONGTRINHHOC.MaMonHoc).Count() > 0)
                {
                    return RedirectToAction("Create", new { id = id, code = 1 });
                }
                cHUONGTRINHHOC.MaNganh = id;
                db.CHUONGTRINHHOCs.Add(cHUONGTRINHHOC);
                db.SaveChanges();
                return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh, code = 10 });
            }

            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh");
            return View(cHUONGTRINHHOC);
        }

        // GET: PDT/CHUONGTRINHHOCs/Edit/5
        public ActionResult Edit(string id_1, string id_2)
        {
            if (id_1 == null || id_2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // POST: PDT/CHUONGTRINHHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNganh,MaMonHoc,HocKy,GhiChu")] CHUONGTRINHHOC cHUONGTRINHHOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUONGTRINHHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh, code = 12 });
            }
            return View(cHUONGTRINHHOC);
        }

        // GET: PDT/CHUONGTRINHHOCs/Delete/5
        public ActionResult Delete(string id_1, string id_2)
        {
            if (id_1 == null || id_2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // POST: PDT/CHUONGTRINHHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id_1, string id_2, FormCollection collection)
        {
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            db.CHUONGTRINHHOCs.Remove(cHUONGTRINHHOC);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh, code = 11 });
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
