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
    public class HUYENsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        //Hàm load danh sách huyện có cùng mã tỉnh
        // GET: PDT/HUYENs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            var hUYENs = db.HUYENs.Include(h => h.TINH);
            return View(hUYENs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var hUYEN = new List<HUYEN>();
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
                            var huyen = new HUYEN();
                            huyen.MaHuyen = workSheet.Cells[iRow, 1].Value.ToString();
                            huyen.TenHuyen = workSheet.Cells[iRow, 2].Value.ToString();
                            huyen.MaTinh = workSheet.Cells[iRow, 3].Value.ToString();
                            hUYEN.Add(huyen);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in hUYEN)
                {
                    excelImport.HUYENs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "HUYENs", new { code = 3});
        }

        //Hàm load thông tin chi tiết một huyện
        // GET: PDT/HUYENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            return View(hUYEN);
        }
        //Hàm tạo mới huyện, đúng thì lưu lại, sai thì thông báo và yêu cầu nhập lại
        // GET: PDT/HUYENs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            ViewBag.code = code;
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh");
            return View();
        }

        // POST: PDT/HUYENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHuyen,TenHuyen,MaTinh,VungSauVungXa")] HUYEN hUYEN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.HUYENs.Where(x => x.MaHuyen == hUYEN.MaHuyen).Count() > 0)
                    {
                        return RedirectToAction("Create", new { code = 2 });
                    }
                    db.HUYENs.Add(hUYEN);
                    db.SaveChanges();
                    return RedirectToAction("Index", new {code = 3});
                }

                ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
                return View(hUYEN);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "HUYENs", new { code = 1 });
            }
        }
        //Hàm sửa thông tin huyện
        // GET: PDT/HUYENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
            return View(hUYEN);
        }

        // POST: PDT/HUYENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHuyen,TenHuyen,MaTinh,VungSauVungXa")] HUYEN hUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hUYEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 5 });
            }
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
            return View(hUYEN);
        }
        //Hàm xóa thông tin huyện
        // GET: PDT/HUYENs/Delete/5
        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            if (db.SINHVIENs.Where(x => x.MaHuyen == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 2} );
            }
            return View(hUYEN);
        }

        // POST: PDT/HUYENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
             HUYEN hUYEN = db.HUYENs.Find(id);
             db.HUYENs.Remove(hUYEN);
             db.SaveChanges();
             return RedirectToAction("Index", new { code = 4 });           
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
