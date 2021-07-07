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
    public class PHIEUTHUsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/PHIEUTHUs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            var pHIEUTHUs = db.PHIEUTHUs.Include(p => p.PHIEU_DKHP);
            return View(pHIEUTHUs.ToList());
        }
        public ActionResult Upload(FormCollection formCollection)
        {
            var pHIEUTHU = new List<PHIEUTHU>();
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
                            var phieuthu = new PHIEUTHU();
                            phieuthu.SoPhieuThu = Convert.ToInt32(workSheet.Cells[iRow, 1].Value.ToString());
                            phieuthu.SoPhieuDKHP = Convert.ToInt32(workSheet.Cells[iRow, 2].Value.ToString());
                            phieuthu.NgayLap = Convert.ToDateTime(workSheet.Cells[iRow, 3].Value.ToString());
                            phieuthu.SoTienThu = Convert.ToInt32(workSheet.Cells[iRow, 4].Value.ToString());
                            pHIEUTHU.Add(phieuthu);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in pHIEUTHU)
                {
                    excelImport.PHIEUTHUs.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "PHIEUTHUs", new { code = 10 });
        }

        // GET: PDT/PHIEUTHUs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // GET: PDT/PHIEUTHUs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.code = code;
            PHIEUTHU pHIEUTHU = new PHIEUTHU();        
            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP.Where(p => p.SoTienConLai > 0 && DateTime.Now <= p.HKNH.HanDongHocPhi), "SoPhieuDKHP", "SoPhieuDKHP").ToList();
            int sOPHIEUTHU = 1;
            if (db.PHIEUTHUs.Count() != 0)
            {               
                var Phieuthu_last = db.PHIEUTHUs.OrderByDescending(x => x.SoPhieuThu).FirstOrDefault(); //tìm số phiếu cuối cùng trong database
                sOPHIEUTHU = Phieuthu_last.SoPhieuThu + 1; //tăng sô phiếu thu cuối lên 1
            }
            
            pHIEUTHU.SoPhieuThu = sOPHIEUTHU;
            pHIEUTHU.NgayLap = DateTime.Now;
            return View(pHIEUTHU);
        }

        // POST: PDT/PHIEUTHUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuThu,SoPhieuDKHP,NgayLap,SoTienThu")] PHIEUTHU pHIEUTHU)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (db.PHIEU_DKHP.Find(pHIEUTHU.SoPhieuDKHP).SoTienConLai < pHIEUTHU.SoTienThu)
                    {
                        return RedirectToAction("Create", "PHIEUTHUs", new { code = 2 });
                    }
                    db.PHIEUTHUs.Add(pHIEUTHU);
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 10 });
                }
                ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP.Where(p => p.SoTienConLai > 0 && DateTime.Now <= p.HKNH.HanDongHocPhi), "SoPhieuDKHP", "SoPhieuDKHP").ToList();
                return View(pHIEUTHU);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "PHIEUTHUs", new { code = 1 });
            }
        }

        // GET: PDT/PHIEUTHUs/Edit/5
        public ActionResult Edit(int? id, int code = 0)
        {
            ViewBag.code = code;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            if (DateTime.Now > pHIEUTHU.PHIEU_DKHP.HKNH.HanDongHocPhi)
            {
                return RedirectToAction("Index", new { code = 1 });
            }
            //ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP.Where(p => p.SoTienConLai > 0), "SoPhieuDKHP", "SoPhieuDKHP").ToList();
            return View(pHIEUTHU);
        }

        // POST: PDT/PHIEUTHUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SoPhieuThu,SoPhieuDKHP,NgayLap,SoTienThu")] PHIEUTHU pHIEUTHU)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(pHIEUTHU).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { code = 12 });
                }
                //ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP.Where(p => p.SoTienConLai > 0), "SoPhieuDKHP", "SoPhieuDKHP").ToList();
                return View(pHIEUTHU);
            }
            //Gọi sửa thông tin phiếu thu bởi các trường dữ liệu với method POST, nếu sai thì thông báo và nhập lại
            catch (Exception)
            {
                return RedirectToAction("Edit", "PHIEUTHUs", new { id = pHIEUTHU.SoPhieuThu, code = 1 });
            }
        }

        // GET: PDT/PHIEUTHUs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            if (pHIEUTHU == null)
            {
                return HttpNotFound();
            }
            return View(pHIEUTHU);
        }

        // POST: PDT/PHIEUTHUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            PHIEUTHU pHIEUTHU = db.PHIEUTHUs.Find(id);
            db.PHIEUTHUs.Remove(pHIEUTHU);
            db.SaveChanges();
            return RedirectToAction("Index", new { code = 11 });
        }
        [HttpGet]
        public ActionResult GetSelectedSoPhieuDKHP(int id)
        {
            // TODO: Tra ve so tien cua phieu DKHP
                var ThongTin=db.PHIEU_DKHP.Find(id);
            var ThongTinPhieuDKHP = (ThongTin.SINHVIEN.HoTen,ThongTin.MaSV, ThongTin.SoTienConLai,"HK "+ThongTin.HKNH.HocKy.ToString()+"  ("+ ThongTin.HKNH.Nam1.ToString()+" - "+ ThongTin.HKNH.Nam2.ToString()+")");
            return Json(ThongTinPhieuDKHP, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TraCuuPhieuThu(int? SoPhieuDKHP)
        {
            if (SoPhieuDKHP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.SoPhieuDKHP = SoPhieuDKHP;
            var pHIEUTHUs = db.PHIEUTHUs.Where(p => p.SoPhieuDKHP == SoPhieuDKHP);
            ViewBag.SoTienConLai = db.PHIEU_DKHP.Find(SoPhieuDKHP).SoTienConLai;
            return View(pHIEUTHUs.ToList());
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

