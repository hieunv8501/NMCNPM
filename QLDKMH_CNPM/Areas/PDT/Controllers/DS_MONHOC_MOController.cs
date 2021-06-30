using System;
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
    public class DS_MONHOC_MOController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();
        public ActionResult ListHK()
        {
            return View(db.HKNHs.ToList());
        }

        //Đây là chức năng hỗ trợ import thông tin bằng định dạng file Excel
        [HttpPost]
        public ActionResult Upload(FormCollection formCollection)
        {
            var dS_MONHOC_MO = new List<DS_MONHOC_MO>();
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
                            var ds_mo = new DS_MONHOC_MO();
                            ds_mo.MaMo = workSheet.Cells[iRow, 1].Value.ToString();
                            ds_mo.MaHKNH = Convert.ToInt32(workSheet.Cells[iRow, 2].Value.ToString());
                            ds_mo.MaMonHoc = workSheet.Cells[iRow, 3].Value.ToString();
                            dS_MONHOC_MO.Add(ds_mo);
                        }
                    }
                }
            }
            using (CNPM_DBContext excelImport = new CNPM_DBContext())
            {
                foreach (var item in dS_MONHOC_MO)
                {
                    excelImport.DS_MONHOC_MO.Add(item);
                }
                excelImport.SaveChanges();
            }
            return RedirectToAction("Index", "DS_MONHOC_MO", new {id = dS_MONHOC_MO.ToList().FirstOrDefault().MaHKNH , code = 10 });
        }

        //Export danh sách môn học sang file excel
        public ActionResult ExportToExcel()
        {
            var mONHOCMO = new System.Data.DataTable();
            mONHOCMO.Columns.Add("Mã môn học mở", typeof(string));
            mONHOCMO.Columns.Add("Mã môn học", typeof(string));
            mONHOCMO.Columns.Add("Tên môn học", typeof(string));
            mONHOCMO.Columns.Add("Loại môn học", typeof(string));
            mONHOCMO.Columns.Add("Số tín chỉ", typeof(int));

            foreach (DS_MONHOC_MO item in db.DS_MONHOC_MO)
            {
                mONHOCMO.Rows.Add(item.MaMo, item.MaMonHoc, item.MONHOC.TenMonHoc, item.MONHOC.LOAIMONHOC.TenLoaiMon, item.MONHOC.SoTinChi);
            }

            var grid = new GridView();
            grid.DataSource = mONHOCMO;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachMonHocMo.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");
        }
        // GET: PDT/DS_MONHOC_MO
        public ActionResult Index(int id = 0, int code = 0)
        {
            ViewBag.code = code;
            ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            ViewBag.HanDongHP = db.HKNHs.Find(id).HanDongHocPhi;
            return View(db.DS_MONHOC_MO.Where(x => x.MaHKNH == id).ToList());
        }

        // GET: PDT/DS_MONHOC_MO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            return View(dS_MONHOC_MO);
        }

        // GET: PDT/DS_MONHOC_MO/Create
        public ActionResult Create(int id = 0, int code = 0)
        {
            ViewBag.code = code;
            ViewBag.MONHOC = db.MONHOCs.ToList();
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            return View();
            //ViewBag.code = code;
            //ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            //int hk = db.HKNHs.Find(id).HocKy;

            //if (hk == 1 || hk == 2)
            //{
            //    int check = (hk % 2 == 0) ? 0 : 1;
            //    var DanhSachMonHoc = db.CHUONGTRINHHOCs.Where(x => ((x.HocKy % 2) == check));
            //    ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            //    ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            //    ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            //    ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            //    return View(DanhSachMonHoc.ToList());
            //}
            //else if (hk == 3)
            //{
            //    var DanhSachMonHoc = db.CHUONGTRINHHOCs;
            //    ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            //    ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            //    ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            //    ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            //    return View(DanhSachMonHoc.ToList());
            //}
            //else
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
        }

        // POST: PDT/DS_MONHOC_MO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int id, [Bind(Include = "MaMo,MaHKNH,MaMonHoc")] DS_MONHOC_MO dS_MONHOC_MO)
        {
            string s = "_";
            int c = id + db.HKNHs.Find(id).HocKy;
            string a = Convert.ToString(c);
            string b = a.Remove(0, 3);
            char d = (char)65;
            string MaMo = dS_MONHOC_MO.MaMonHoc + s + d + b;
            if (db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0)
            {
                for (int i = 66; db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0; i++)
                {
                    d = (char)i;
                    MaMo = dS_MONHOC_MO.MaMonHoc + s + d + b;
                }
            }
            dS_MONHOC_MO.MaMo = MaMo;
            if (ModelState.IsValid)
            {
                if (db.DS_MONHOC_MO.Where(x => x.MaMo == dS_MONHOC_MO.MaMo).Count() > 0)
                {
                    return RedirectToAction("Create", new { id = id, code = 1 });
                }
                dS_MONHOC_MO.MaHKNH = id;
                db.DS_MONHOC_MO.Add(dS_MONHOC_MO);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = dS_MONHOC_MO.MaHKNH, code = 10 });
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        public ActionResult Create_2(int MaHKNH)
        {
            string s = "_";
            int c = MaHKNH + db.HKNHs.Find(MaHKNH).HocKy;
            string a = Convert.ToString(c);
            string b = a.Remove(0, 3);

            if (db.HKNHs.Find(MaHKNH).HocKy % 2 == 0)
            {
                List<CHUONGTRINHHOC> CTH = db.CHUONGTRINHHOCs.Where(x => x.HocKy % 2 == 0).ToList();
                foreach (var item in CTH)
                {
                    char d = (char)65;
                    string MaMo = item.MaMonHoc + s + d + b;
                    if (db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0)
                    {
                        for (int i = 66; db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0; i++)
                        {
                            d = (char)i;
                            MaMo = item.MaMonHoc + s + d + b;
                        }
                    }
                    string MaMonHoc = item.MaMonHoc;
                    string SQL = "INSERT INTO DS_MONHOC_MO VALUES('" + MaMo + "','" + MaHKNH + "','" + MaMonHoc + "')";
                    db.Database.ExecuteSqlCommand(SQL);
                }
            }
            else
            {
                List<CHUONGTRINHHOC> CTH = db.CHUONGTRINHHOCs.Where(x => x.HocKy % 2 != 0).ToList();
                foreach (var item in CTH)
                {
                    char d = (char)65;
                    string MaMo = item.MaMonHoc + s + d + b;
                    if (db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0)
                    {
                        for (int i = 66; db.DS_MONHOC_MO.Where(x => x.MaMo == MaMo).Count() > 0; i++)
                        {
                            d = (char)i;
                            MaMo = item.MaMonHoc + s + d + b;
                        }
                    }

                    string MaMonHoc = item.MaMonHoc;
                    string SQL = "INSERT INTO DS_MONHOC_MO VALUES('" + MaMo + "','" + MaHKNH + "','" + MaMonHoc + "')";
                    db.Database.ExecuteSqlCommand(SQL);
                }

            }

            return RedirectToAction("Index", new { id = MaHKNH, code = 12 });
        }

        // GET: PDT/DS_MONHOC_MO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            if (db.CT_PHIEU_DKHP.Where(x => x.MaMo == id).Count() > 0)
            {
                return RedirectToAction("Index", new { id = dS_MONHOC_MO.MaHKNH, code = 1 });
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        // POST: PDT/DS_MONHOC_MO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaMo,MaHKNH,MaMonHoc")] DS_MONHOC_MO dS_MONHOC_MO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dS_MONHOC_MO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = dS_MONHOC_MO.MaHKNH, code = 11 });
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dS_MONHOC_MO.MaHKNH);
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", dS_MONHOC_MO.MaMonHoc);
            return View(dS_MONHOC_MO);
        }

        // GET: PDT/DS_MONHOC_MO/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            if (dS_MONHOC_MO == null)
            {
                return HttpNotFound();
            }
            if (db.CT_PHIEU_DKHP.Where(x => x.MaMo == id).Count() > 0)
            {
                return RedirectToAction("Index", new { id = dS_MONHOC_MO.MaHKNH, code = 1 });
            }
            return View(dS_MONHOC_MO);
        }

        // POST: PDT/DS_MONHOC_MO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DS_MONHOC_MO dS_MONHOC_MO = db.DS_MONHOC_MO.Find(id);
            db.DS_MONHOC_MO.Remove(dS_MONHOC_MO);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = dS_MONHOC_MO.MaHKNH, code = 11 });
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
