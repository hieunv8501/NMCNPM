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
    public class NGANHsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/NGANHs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            //Load những index có cùng mã khoa
            var nGANHs = db.NGANHs.Include(n => n.KHOA);
            return View(nGANHs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var nGANH = new List<NGANH>();
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
                            var ng = new NGANH();
                            ng.MaNganh = workSheet.Cells[iRow, 1].Value.ToString();
                            ng.TenNganh = workSheet.Cells[iRow, 2].Value.ToString();
                            ng.MaKhoa = workSheet.Cells[iRow, 3].Value.ToString();
                            nGANH.Add(ng);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in nGANH)
                {
                    excelImport.NGANHs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "NGANHs", new { code = 4});
        }

        // GET: PDT/NGANHs/Details/5
        public ActionResult Details(string id)
        {
            //Load thông tin ngành dựa vào mã ngành
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGANH nGANH = db.NGANHs.Find(id);
            if (nGANH == null)
            {
                return HttpNotFound();
            }
            return View(nGANH);
        }

        //Hàm tạo mới thông tin ngành
        // GET: PDT/NGANHs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.code = code;
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa");
            return View();
        }

        // POST: PDT/NGANHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNganh,TenNganh,MaKhoa")] NGANH nGANH)
        {
            if (ModelState.IsValid)
            {
                if (db.NGANHs.Where(x => x.MaNganh == nGANH.MaNganh).Count() > 0)
                {
                    return RedirectToAction("Create", new { code = 1 });
                }
                db.NGANHs.Add(nGANH);
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 4 });
            }

            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", nGANH.MaKhoa);
            return View(nGANH);
        }
        //Hàm sửa thông tin ngành
        // GET: PDT/NGANHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGANH nGANH = db.NGANHs.Find(id);
            if (nGANH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", nGANH.MaKhoa);
            return View(nGANH);
        }

        // POST: PDT/NGANHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNganh,TenNganh,MaKhoa")] NGANH nGANH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGANH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 6 });
            }
            ViewBag.MaKhoa = new SelectList(db.KHOAs, "MaKhoa", "TenKhoa", nGANH.MaKhoa);
            return View(nGANH);
        }

        //Hàm xóa thông tin ngành
        // GET: PDT/NGANHs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGANH nGANH = db.NGANHs.Find(id);
            if (db.SINHVIENs.Where(x => x.MaNganh == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 2 });
            }
            if (db.CHUONGTRINHHOCs.Where(x => x.MaNganh == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 3 });
            }
            if (nGANH == null)
            {
                return HttpNotFound();
            }
            return View(nGANH);
        }

        // POST: PDT/NGANHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NGANH nGANH = db.NGANHs.Find(id);
            db.NGANHs.Remove(nGANH);
            db.SaveChanges();
            return RedirectToAction("Index", new { code = 5 });
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
