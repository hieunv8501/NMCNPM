using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class LOAIMONHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/LOAIMONHOCs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            return View(db.LOAIMONHOCs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var lOAIMONHOC = new List<LOAIMONHOC>();
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
                            var lmh = new LOAIMONHOC();
                            lmh.MaLoaiMon = workSheet.Cells[iRow, 1].Value.ToString();
                            lmh.TenLoaiMon = workSheet.Cells[iRow, 2].Value.ToString();
                            lmh.SoTienMotTinChi = Convert.ToInt32(workSheet.Cells[iRow, 3].Value.ToString());
                            lOAIMONHOC.Add(lmh);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in lOAIMONHOC)
                {
                    excelImport.LOAIMONHOCs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "LOAIMONHOCs", new { code = 10 });
        }
        // GET: PDT/LOAIMONHOCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.code = code;
            return View();
        }

        // POST: PDT/LOAIMONHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiMon,TenLoaiMon,HeSoChia,SoTienMotTinChi")] LOAIMONHOC lOAIMONHOC)
        {
            if (ModelState.IsValid)
            {
                if (db.LOAIMONHOCs.Where(x => x.MaLoaiMon == lOAIMONHOC.MaLoaiMon).Count() > 0)
                {
                    return RedirectToAction("Create", new { code = 1 });
                }
                string a = lOAIMONHOC.TenLoaiMon.Trim();
                Regex trimmer = new Regex(@"\s\s+");
                a = trimmer.Replace(a, " ");
                foreach (var item in db.LOAIMONHOCs)
                {
                    string b = item.TenLoaiMon.Trim();
                    b = trimmer.Replace(b, " ");
                    if (string.Compare(a, b, true) == 0)
                    {
                        return RedirectToAction("Create", new { code = 3 });
                    }
                }
                db.LOAIMONHOCs.Add(lOAIMONHOC);
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 10 });
            }

            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Edit/5
        public ActionResult Edit(string id, int code = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);

            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.code = code;
            return View(lOAIMONHOC);
        }

        // POST: PDT/LOAIMONHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiMon,TenLoaiMon,HeSoChia,SoTienMotTinChi")] LOAIMONHOC lOAIMONHOC)
        {
            if (ModelState.IsValid)
            {
                string a = lOAIMONHOC.TenLoaiMon.Trim();
                Regex trimmer = new Regex(@"\s\s+");
                a = trimmer.Replace(a, " ");
                List<LOAIMONHOC> LMH = db.LOAIMONHOCs.Where(x => x.MaLoaiMon != lOAIMONHOC.MaLoaiMon).ToList();
                foreach (var item in LMH)
                {

                    string b = item.TenLoaiMon.Trim();
                    b = trimmer.Replace(b, " ");
                    if (string.Compare(a, b, true) == 0)
                    {
                        return RedirectToAction("Edit", new { code = 3 });
                    }

                }
                db.Entry(lOAIMONHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 12 });
            }
            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            if (db.MONHOCs.Where(x => x.MaLoaiMon == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 1 });
            }
            return View(lOAIMONHOC);
        }

        // POST: PDT/LOAIMONHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            db.LOAIMONHOCs.Remove(lOAIMONHOC);
            db.SaveChanges();
            return RedirectToAction("Index", new { code = 11 });
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
