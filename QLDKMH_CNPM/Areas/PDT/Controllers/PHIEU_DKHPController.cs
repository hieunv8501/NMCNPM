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
    public class PHIEU_DKHPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/PHIEU_DKHP
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
            var pHIEU_DKHP = db.PHIEU_DKHP.Include(p => p.HKNH).Include(p => p.SINHVIEN);
            return View(pHIEU_DKHP.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var pHIEU_DKHP = new List<PHIEU_DKHP>();
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
                            var pdk = new PHIEU_DKHP();
                            pdk.SoPhieuDKHP = Convert.ToInt32(workSheet.Cells[iRow, 1].Value.ToString());
                            pdk.MaSV = workSheet.Cells[iRow, 2].Value.ToString();
                            pdk.NgayLap = Convert.ToDateTime(workSheet.Cells[iRow, 3].Value.ToString());
                            pdk.MaHKNH = Convert.ToInt32(workSheet.Cells[iRow, 2].Value.ToString());
                            pHIEU_DKHP.Add(pdk);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in pHIEU_DKHP)
                {
                    excelImport.PHIEU_DKHP.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "PHIEU_DKHP", new { code = 4 });
        }

        // GET: PDT/PHIEU_DKHP/Details/5
        public ActionResult Details(int? id, int code = 0)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            ViewBag.HDHP = (db.HKNHs.Find(pHIEU_DKHP.MaHKNH)).HanDongHocPhi;
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.code = code;
            ViewBag.CT_PHIEU_DKHPandDS_MONHOC_MO = db.CT_PHIEU_DKHP.Where(m => m.SoPhieuDKHP == id).ToList();
            return View(pHIEU_DKHP);
        }

        public ActionResult Details_2(int? id, int? SoPhieuDKHP, string MSSV, int? MHKNH, int? Day, int? Month, int? Year, int? TinhTrang)
        {
            ViewBag.SoPhieuDKHP = SoPhieuDKHP;
            ViewBag.MSSV = MSSV;
            ViewBag.MHKNH = MHKNH;
            ViewBag.Day = Day;
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.TinhTrang = TinhTrang;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.CT_PHIEU_DKHPandDS_MONHOC_MO = db.CT_PHIEU_DKHP.Where(m => m.SoPhieuDKHP == id).ToList();
            return View(pHIEU_DKHP);
        }

        // GET: PDT/PHIEU_DKHP/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            ViewBag.code = code;
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "MaSV");
            ViewBag.HocKyNamHoc = db.HKNHs.Where(x => (x.Nam1 == DateTime.Now.Year || x.Nam2 == DateTime.Now.Year) && DateTime.Now <= x.HanDongHocPhi);
            int sOPHIEUDKHP = 1;
            if (db.PHIEU_DKHP.Count() != 0)
            {
                var SoPhieu_last = db.PHIEU_DKHP.OrderByDescending(x => x.SoPhieuDKHP).FirstOrDefault(); //tìm số phiếu cuối cùng trong database
                sOPHIEUDKHP = SoPhieu_last.SoPhieuDKHP + 1; //tăng sô phiếu cuối lên 1
            }

            //if (db.PHIEU_DKHP.Max(x => (int?)x.SoPhieuDKHP).Equals(null))
            //{
            //    PHIEU_DKHP.SoPhieuDKHP = 1;
            //}
            //else
            //{
            //    pHIEU_DKHP.SoPhieuDKHP = db.PHIEU_DKHP.Max(x => x.SoPhieuDKHP) + 1;
            //}
            ViewBag.SoPhieuDKHP = sOPHIEUDKHP;
            PHIEU_DKHP pHIEU_DKHP = new PHIEU_DKHP();
            pHIEU_DKHP.SoPhieuDKHP = ViewBag.SoPhieuDKHP;
            pHIEU_DKHP.NgayLap = DateTime.Now;
            ViewBag.NgayLap = pHIEU_DKHP.NgayLap;

            pHIEU_DKHP.TongTCLT = 0;
            pHIEU_DKHP.TongTCTH = 0;
            pHIEU_DKHP.TongTienDangKy = 0;
            pHIEU_DKHP.TongTienPhaiDong = 0;
            pHIEU_DKHP.TongTienDaDong = 0;
            pHIEU_DKHP.SoTienConLai = 0;
            ViewBag.TongTCLT = pHIEU_DKHP.TongTCLT;
            ViewBag.TongTCTH = pHIEU_DKHP.TongTCTH;
            ViewBag.TongTienDangKy = pHIEU_DKHP.TongTienDangKy;
            ViewBag.TongTienPhaiDong = pHIEU_DKHP.TongTienPhaiDong;
            ViewBag.TongTienDaDong = pHIEU_DKHP.TongTienDaDong;
            ViewBag.SoTienConLai = pHIEU_DKHP.SoTienConLai;
            return View(pHIEU_DKHP);
        }

        // POST: PDT/PHIEU_DKHP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        {
            if (db.PHIEU_DKHP.Where(x => x.MaHKNH == pHIEU_DKHP.MaHKNH && x.MaSV == pHIEU_DKHP.MaSV).Count() > 0)
            {
                return RedirectToAction("Create", "PHIEU_DKHP", new { code = 2 });
            }
            try
            {
                if (ModelState.IsValid)
                {
                    db.PHIEU_DKHP.Add(pHIEU_DKHP);
                    db.SaveChanges();
                    return RedirectToAction("Create", "CT_PHIEU_DKHP", new { id = pHIEU_DKHP.SoPhieuDKHP, HKNH = pHIEU_DKHP.MaHKNH, code = 10 });
                }

                ViewBag.HocKyNamHoc = db.HKNHs.Where(x => (x.Nam1 == DateTime.Now.Year || x.Nam2 == DateTime.Now.Year) && DateTime.Now <= x.HanDongHocPhi);
                ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
                return View(pHIEU_DKHP);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "PHIEU_DKHP", new { code = 1 });
            }
        }

        // GET: PDT/PHIEU_DKHP/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
        //    if (pHIEU_DKHP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
        //    return View(pHIEU_DKHP);
        //}

        // POST: PDT/PHIEU_DKHP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "SoPhieuDKHP,MaSV,NgayLap,MaHKNH,TongTCLT,TongTCTH,TongTienDangKy,TongTienPhaiDong,TongTienDaDong,SoTienConLai")] PHIEU_DKHP pHIEU_DKHP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(pHIEU_DKHP).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", pHIEU_DKHP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", pHIEU_DKHP.MaSV);
        //    return View(pHIEU_DKHP);
        //}

        // GET: PDT/PHIEU_DKHP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            if (db.PHIEUTHUs.Where(x => x.SoPhieuDKHP == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 1 });
            }
            if (db.CT_PHIEU_DKHP.Where(x => x.SoPhieuDKHP == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 2 });
            }
            ViewBag.id = id;
            return View(pHIEU_DKHP);
        }

        // POST: PDT/CT_PHIEU_DKHP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            db.PHIEU_DKHP.Remove(pHIEU_DKHP);
            db.SaveChanges();
            return RedirectToAction("Index", new { code = 11 });
        }

        public ActionResult TraCuu(int? SoPhieuDKHP, string MSSV, int? MHKNH, int? Day, int? Month, int? Year, int? TinhTrang, int code = 0)
        {
            ViewBag.code = code;
            ViewBag.HocKyNamHoc = db.HKNHs;
            ViewBag.SoPhieuDKHP = SoPhieuDKHP;
            ViewBag.MSSV = MSSV;
            ViewBag.MHKNH = MHKNH;
            ViewBag.Day = Day;
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.TinhTrang = TinhTrang;
            //Nếu người dùng không nhập gì thì không hiện gì cả
            if (SoPhieuDKHP == null && (MSSV == null || MSSV == "") && MHKNH == null && Day == null && Month == null && Year == null && TinhTrang == null)
            {
                var EmtyList = new List<PHIEU_DKHP>();
                return View(EmtyList);
            }
            else
            {
                var pHIEU_DKHP = db.PHIEU_DKHP.Where(x => (x.SoPhieuDKHP == SoPhieuDKHP || SoPhieuDKHP == null) && (x.MaSV == MSSV || MSSV == "" || MSSV == null) && (x.HKNH.MaHKNH == MHKNH || MHKNH == null) && (x.NgayLap.Day == Day || Day == null) && (x.NgayLap.Month == Month || Month == null) && (x.NgayLap.Year == Year || Year == null) && (((TinhTrang == 0) ? (x.SoTienConLai > 0) : ((TinhTrang == 1) ? (x.TongTienDangKy > 0 && x.SoTienConLai == 0) : (x.TongTienDangKy == 0 && x.SoTienConLai == 0))) || TinhTrang == null));
                return View(pHIEU_DKHP.ToList());
            }
        }

        public ActionResult Delete_2(int? id, int? SoPhieuDKHP, string MSSV, int? MHKNH, int? Day, int? Month, int? Year, int? TinhTrang)
        {
            ViewBag.SoPhieuDKHP = SoPhieuDKHP;
            ViewBag.MSSV = MSSV;
            ViewBag.MHKNH = MHKNH;
            ViewBag.Day = Day;
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.TinhTrang = TinhTrang;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            if (db.PHIEUTHUs.Where(x => x.SoPhieuDKHP == id).Count() > 0)
            {
                return RedirectToAction("TraCuu", new { SoPhieuDKHP = ViewBag.SoPhieuDKHP, MSSV = ViewBag.MSSV, MHKNH = ViewBag.MHKNH, Day = ViewBag.Day, Month = ViewBag.Month, Year = ViewBag.Year, TinhTrang = ViewBag.TinhTrang, code = 1 });
            }
            if (db.CT_PHIEU_DKHP.Where(x => x.SoPhieuDKHP == id).Count() > 0)
            {
                return RedirectToAction("TraCuu", new { SoPhieuDKHP = ViewBag.SoPhieuDKHP, MSSV = ViewBag.MSSV, MHKNH = ViewBag.MHKNH, Day = ViewBag.Day, Month = ViewBag.Month, Year = ViewBag.Year, TinhTrang = ViewBag.TinhTrang, code = 2 });
            }
            ViewBag.id = id;
            return View(pHIEU_DKHP);
        }

        // POST: PDT/CT_PHIEU_DKHP/Delete/5
        [HttpPost, ActionName("Delete_2")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_2(int id, int? SoPhieuDKHP, string MSSV, int? MHKNH, int? Day, int? Month, int? Year, int? TinhTrang)
        {
            ViewBag.SoPhieuDKHP = SoPhieuDKHP;
            ViewBag.MSSV = MSSV;
            ViewBag.MHKNH = MHKNH;
            ViewBag.Day = Day;
            ViewBag.Month = Month;
            ViewBag.Year = Year;
            ViewBag.TinhTrang = TinhTrang;
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            db.PHIEU_DKHP.Remove(pHIEU_DKHP);
            db.SaveChanges();
            return RedirectToAction("TraCuu", new { SoPhieuDKHP = ViewBag.SoPhieuDKHP, MSSV = ViewBag.MSSV, MHKNH = ViewBag.MHKNH, Day = ViewBag.Day, Month = ViewBag.Month, Year = ViewBag.Year, TinhTrang = ViewBag.TinhTrang, code = 13 });
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
