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
using CrystalDecisions.CrystalReports.Engine;
using QLDKMH_CNPM.Models;


namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class DSSV_CHUAHOANTHANH_HPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Index/5
        public ActionResult Index(int id)
        {
            if (DateTime.Now > db.HKNHs.Find(id).HanDongHocPhi)
            {
                ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
                ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
                ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
                ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
                var check = db.DSSV_CHUAHOANTHANH_HP.Where(x => x.MaHKNH == id);

                if (check.Count() == 0)
                {
                    string SQL = "INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH = '" + id + "'";
                    db.Database.ExecuteSqlCommand(SQL);
                }
                List<SINHVIEN> SV = db.SINHVIENs.ToList();
                List<PHIEU_DKHP> PD = db.PHIEU_DKHP.ToList();
                List<DSSV_CHUAHOANTHANH_HP> DCH = db.DSSV_CHUAHOANTHANH_HP.ToList();

                var baocao = from P in PD
                             join D in DCH on P.MaHKNH equals D.MaHKNH into TB1
                             from D in TB1.ToList()
                             where P.MaSV == D.MaSV && D.MaHKNH == id && P.SoTienConLai > 0
                             join S in SV on D.MaSV equals S.MaSV into TB2
                             from S in TB2.ToList()
                             select new DSSV_CHUAHOANTHANH_HP
                             {
                                 SINHVIEN = S,
                                 SoTienPhaiDong = P.TongTienPhaiDong,
                                 SoTienDangKy = P.TongTienDangKy,
                                 SoTienConLai = D.SoTienConLai,
                             };

                if (baocao.Count() == 0)
                    return RedirectToAction("ListHK", new { code = 1 });
                return View(baocao.ToList());
            }
            ViewBag.check = 0;
            return View();
        }

        public ActionResult ListHK(int code = 0)
        {
            ViewBag.Message = "";
            if (code == 1)
                ViewBag.Message = "Học kỳ này không có sinh viên nợ học phí";
            return View(db.HKNHs.ToList());
        }
        public ActionResult ExportToExcel(int id)
        {
            var dSSV_CHUAHOANTHANH_HP = new System.Data.DataTable();
            dSSV_CHUAHOANTHANH_HP.Columns.Add("MSSV", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Ho ten", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("So tien dang ky", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("So tien phai dong", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("So tien con lai", typeof(string));

            List<SINHVIEN> SV = db.SINHVIENs.ToList();
            List<PHIEU_DKHP> PD = db.PHIEU_DKHP.ToList();
            List<DSSV_CHUAHOANTHANH_HP> DCH = db.DSSV_CHUAHOANTHANH_HP.ToList();

            var baocao = from P in PD
                         join D in DCH on P.MaHKNH equals D.MaHKNH into TB1
                         from D in TB1.ToList()
                         where P.MaSV == D.MaSV && D.MaHKNH == id && P.SoTienConLai > 0
                         join S in SV on D.MaSV equals S.MaSV into TB2
                         from S in TB2.ToList()
                         select new DSSV_CHUAHOANTHANH_HP
                         {
                             SINHVIEN = S,
                             SoTienPhaiDong = P.TongTienPhaiDong,
                             SoTienDangKy = P.TongTienDangKy,
                             SoTienConLai = D.SoTienConLai,
                         };
            foreach (var item in baocao)
            {
                dSSV_CHUAHOANTHANH_HP.Rows.Add(item.SINHVIEN.MaSV, item.SINHVIEN.HoTen, item.SoTienDangKy, item.SoTienPhaiDong, item.SoTienConLai);
            }

            var grid = new GridView();
            grid.DataSource = dSSV_CHUAHOANTHANH_HP;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachSinhVienNoHP.xls");
            Response.Charset = "utf-8";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");
        }


        //// GET: PDT/DSSV_CHUAHOANTHANH_HP/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
        //    if (dSSV_CHUAHOANTHANH_HP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dSSV_CHUAHOANTHANH_HP);

        //}

        //// GET: PDT/DSSV_CHUAHOANTHANH_HP/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen");
        //    return View();
        ////}

        //// POST: PDT/DSSV_CHUAHOANTHANH_HP/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.DSSV_CHUAHOANTHANH_HP.Add(dSSV_CHUAHOANTHANH_HP);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
        //    return View(dSSV_CHUAHOANTHANH_HP);
        //}

        //// GET: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
        //    if (dSSV_CHUAHOANTHANH_HP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
        //    return View(dSSV_CHUAHOANTHANH_HP);
        //}

        //// POST: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(dSSV_CHUAHOANTHANH_HP).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
        //    ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
        //    return View(dSSV_CHUAHOANTHANH_HP);
        //}

        //// GET: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
        //    if (dSSV_CHUAHOANTHANH_HP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dSSV_CHUAHOANTHANH_HP);
        //}

        //// POST: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
        //    db.DSSV_CHUAHOANTHANH_HP.Remove(dSSV_CHUAHOANTHANH_HP);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
