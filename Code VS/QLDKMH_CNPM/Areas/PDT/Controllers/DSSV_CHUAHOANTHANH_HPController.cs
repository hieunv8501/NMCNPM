using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class DSSV_CHUAHOANTHANH_HPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Index/5
        public ActionResult Index(string id)
        {
            var dsSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Include(b => b.SINHVIEN);
            List<DSSV_CHUAHOANTHANH_HP> bao_cao = db.DSSV_CHUAHOANTHANH_HP.ToList();
            List<SINHVIEN> sinh_vien = db.SINHVIENs.ToList();
            List<HKNH> hk_nh = db.HKNHs.ToList();
            List<PHIEU_DKHP> phieu_dk = db.PHIEU_DKHP.ToList();

            var danhsach = from b in bao_cao
                           join s in sinh_vien on b.MaSV equals s.MaSV into table1
                           from s in table1.ToList()
                           join h in hk_nh on b.MaHKNH equals h.MaHKNH into table2
                           from h in table2.ToList()
                           select new DSSV_CHUAHOANTHANH_HP
                           {
                               SINHVIEN = s,
                               SoTienPhaiDong = b.SoTienPhaiDong,
                               SoTienDangKy = b.SoTienDangKy,
                               SoTienConLai = b.SoTienConLai,
                               HKNH = h
                           };
            if (id == null)
            {
                ViewBag.Test = dsSV_CHUAHOANTHANH_HP.ToList();
                return View(danhsach.ToList());
            }
            var baocao = from p in phieu_dk
                         join h in hk_nh on p.MaHKNH equals h.MaHKNH into table1
                         from h in table1.ToList()
                         where p.MaHKNH == Convert.ToInt32(id) & p.SoTienConLai > 0
                         join s in sinh_vien on p.MaSV equals s.MaSV into table2
                         from s in table2.ToList()
                         select new DSSV_CHUAHOANTHANH_HP
                         {
                             SINHVIEN = s,
                             SoTienPhaiDong = p.TongTienPhaiDong,
                             SoTienDangKy = p.TongTienDangKy,
                             SoTienConLai = p.SoTienConLai,
                             HKNH = h
                         };

           //Chỗ này nó không chạy qua nhưng SQL Server lại lưu được chạy được :(
             string SQL = "IF NOT EXISTS (SELECT * FROM DSSV_CHUAHOANTHANH_HP JOIN PHIEU_DKHP ON DSSV_CHUAHOANTHANH_HP.MaHKNH = PHIEU_DKHP.MaHKNH AND DSSV_CHUAHOANTHANH_HP.MaSV = PHIEU_DKHP.MaSV WHERE PHIEU_DKHP.SoTienConLai > 0) BEGIN INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH ='"+id+"' END";
             db.Database.ExecuteSqlCommand(SQL);
             
            //INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH = '" + id + "'";
            //string SQL = "INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH = '" + id + "'";
            //db.Database.ExecuteSqlCommand(SQL);
            ViewBag.Message = id;
            if (baocao.Count() == 0)
                return RedirectToAction("ListHK", new { code = 1 });

            return View(baocao.ToList());

            //string SQL_2 = "SELECT * FROM DSSV_CHUAHOANTHANH_HP WHERE MaHKNH = '" + id + "'";
            //if (DateTime.Now > db.HKNHs.Find(id).HanDongHocPhi)
            //{
            //    ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            //    ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            //    ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            //    var check = db.DSSV_CHUAHOANTHANH_HP.Where(x => x.MaHKNH == id);
            //    var count = check.Count();
            //    if (count == 0)
            //    {
            //        string SQL = "INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH = '" + id + "'";
            //        db.Database.ExecuteSqlCommand(SQL);
            //    }
            //    return View(db.Database.SqlQuery<DSSV_CHUAHOANTHANH_HP>(SQL_2).ToList());
            //}
            //ViewBag.check = 0;
            //return View(db.Database.SqlQuery<DSSV_CHUAHOANTHANH_HP>(SQL_2).ToList());
        }

        public ActionResult ListHK(int code = 0)
        {
            ViewBag.message = "";
            if (code == 1)
                ViewBag.message = "Học kỳ này không có sinh viên nợ học phí";
            return View(db.HKNHs.ToList());
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            return View(dSSV_CHUAHOANTHANH_HP);

        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Create
        public ActionResult Create()
        {
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH");
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen");
            return View();
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        {
            if (ModelState.IsValid)
            {
                db.DSSV_CHUAHOANTHANH_HP.Add(dSSV_CHUAHOANTHANH_HP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHKNH,MaSV,SoTienConLai")] DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dSSV_CHUAHOANTHANH_HP).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHKNH = new SelectList(db.HKNHs, "MaHKNH", "MaHKNH", dSSV_CHUAHOANTHANH_HP.MaHKNH);
            ViewBag.MaSV = new SelectList(db.SINHVIENs, "MaSV", "HoTen", dSSV_CHUAHOANTHANH_HP.MaSV);
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            if (dSSV_CHUAHOANTHANH_HP == null)
            {
                return HttpNotFound();
            }
            return View(dSSV_CHUAHOANTHANH_HP);
        }

        // POST: PDT/DSSV_CHUAHOANTHANH_HP/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DSSV_CHUAHOANTHANH_HP dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.Find(id);
            db.DSSV_CHUAHOANTHANH_HP.Remove(dSSV_CHUAHOANTHANH_HP);
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
