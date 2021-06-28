using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using OfficeOpenXml;
using QLDKMH_CNPM.Models;


namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class TINHsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/TINHs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            return View(db.TINHs.ToList());
        }

        //Đây là chức năng hỗ trợ export thông tin ra định dạng file Excel


        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var tINH = new List<TINH>();
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
                            var tinh = new TINH();
                            tinh.MaTinh = workSheet.Cells[iRow, 1].Value.ToString();
                            tinh.TenTinh = workSheet.Cells[iRow, 2].Value.ToString();
                            tINH.Add(tinh);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in tINH)
                {
                    excelImport.TINHs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "TINHs", new { code = 3});
        }

        // GET: PDT/TINHs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            return View(tINH);
        }

        // GET: PDT/TINHs/Create
        public ActionResult Create(int code = 0)
        {
            //Kiểm tra thông tin nhập vào đúng chưa, nếu chưa thì thông báo và yêu cầu nhập lại
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            ViewBag.code = code;
            return View();
        }

        // POST: PDT/TINHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinh,TenTinh")] TINH tINH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.TINHs.Where(x => x.MaTinh == tINH.MaTinh).Count() > 0)
                    {
                        return RedirectToAction("Create", new { code = 2 });
                    }
                    db.TINHs.Add(tINH);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 3 });
                }
                return View(tINH);
            }
            //Kiểm tra thông tin nhập vào đúng chưa, nếu chưa thì thông báo và yêu cầu nhập lại
            catch (Exception)
            {
                return RedirectToAction("Create", "TINHs", new { code = 1});
            }
        }
        //Hàm sửa thông tin nhập
        // GET: PDT/TINHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            return View(tINH);
        }

        // POST: PDT/TINHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinh,TenTinh")] TINH tINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code =  5});
            }
            return View(tINH);
        }
        //Hàm xóa thông tin
        // GET: PDT/TINHs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            if (db.HUYENs.Where(x => x.MaTinh == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 2 });
            }
            return View(tINH);
        }

        // POST: PDT/TINHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TINH tINH = db.TINHs.Find(id);
            db.TINHs.Remove(tINH);
            db.SaveChanges();
            return RedirectToAction("Index", new { code = 4});
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
