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
        public ActionResult Index(int id)
        {
            var danhsach = db.DSSV_CHUAHOANTHANH_HP.Include(b => b.SINHVIEN).Include(h => h.HKNH);    //kết hai bảng SINHVIEN va DSSV_CHUAHOANTHANH_HP

            List<DSSV_CHUAHOANTHANH_HP> dSSV_CHUAHOANTHANH_HP = db.DSSV_CHUAHOANTHANH_HP.ToList(); //xuất ds sv nợ học phí ra list 
            List<SINHVIEN> sINHVIEN = db.SINHVIENs.ToList(); //xuất SV ra list
            List<HKNH> hKNH = db.HKNHs.ToList(); //xuất học kỳ ra list
            List<PHIEU_DKHP> pHIEU_DKHP = db.PHIEU_DKHP.ToList(); //xuất phiếu DKHP ra list

            var model1 = from d in dSSV_CHUAHOANTHANH_HP
                        join s in sINHVIEN on d.MaSV equals s.MaSV into table_temp1   // join SINHVIEN và DSSV_CHUAHOANTHANH_HP sang bảng tạm
                        from s in table_temp1.ToList()                                // xuất ra list
                        join h in hKNH on d.MaHKNH equals h.MaHKNH into table_temp2   // join thêm HKNH sang bảng tạm khác
                        from h in table_temp2.ToList()                                // xuất ra list
                            select new DSSV_CHUAHOANTHANH_HP
                            {
                                HKNH = h,
                                SINHVIEN = s,
                                SoTienConLai = d.SoTienConLai
                            };

            if (id <= 0)
            {
                ViewBag.KIEMTRA = danhsach.ToList();
                return View(model1.ToList());
            }
            var model2 = (from p in pHIEU_DKHP
                         join h in hKNH on p.MaHKNH equals h.MaHKNH into table_temp1
                         where (p.MaHKNH == id) && (p.SoTienConLai > 0)
                         from h in table_temp1.ToList()
                         select new DSSV_CHUAHOANTHANH_HP
                         {
                             PHIEU_DKHP = p,
                             HKNH = h
                         });

            ViewBag.M = id;
            if (model2.Count() == 0)
                return RedirectToAction("ListHK", new { code = 1 });

            return View(model2.ToList());
        }

        public ActionResult ListHK(int code = 0)
        {
            ViewBag.Message = "";
            if (code == 1)
                ViewBag.Message = "Học kỳ này không có sinh viên nợ học phí";
            var namHoc = db.HKNHs;
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
