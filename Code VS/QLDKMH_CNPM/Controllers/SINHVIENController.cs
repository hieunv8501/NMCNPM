using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Controllers
{
    public class SINHVIENController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: SINHVIEN
        public ActionResult Index()
        {
            var sINHVIENs = db.SINHVIENs.Include(s => s.DOITUONG).Include(s => s.HUYEN).Include(s => s.NGANH);
            return View(sINHVIENs.ToList());
        }

        // GET: SINHVIEN/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }
        //GetHuyen for choosing Huyen from List after selecting Tinh
        public JsonResult GetHuyen(string id)
        {
            List<HUYEN> ddlHuyen = db.HUYENs.Where(x => x.MaTinh == id).ToList();
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            List<SelectListItem> listHuyen = selectListItems;

            listHuyen.Add(new SelectListItem { Text = "-- Chọn Huyện --", Value = "0" });
            if (ddlHuyen != null)
            {
                foreach (var x in ddlHuyen)
                {
                    listHuyen.Add(new SelectListItem { Text = x.TenHuyen, Value = x.MaHuyen.ToString() });
                }
            }
            return Json(new SelectList(listHuyen, "Value", "Text", JsonRequestBehavior.AllowGet));
        }
        // GET: SINHVIEN/Create
        public ActionResult Create()
        {
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong");
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh");
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen");
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh");
            return View();
        }

        // POST: SINHVIEN/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.SINHVIENs.Add(sINHVIEN);
                db.SaveChanges();
                ViewBag.Message = "Your application description page.";
                return RedirectToAction("Index");
            }

            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return View(sINHVIEN);
        }

        // GET: SINHVIEN/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return View(sINHVIEN);
        }

        // POST: SINHVIEN/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoTen,NgaySinh,GioiTinh,MaNganh,MaDoiTuong,MaHuyen")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDoiTuong = new SelectList(db.DOITUONGs, "MaDoiTuong", "TenDoiTuong", sINHVIEN.MaDoiTuong);
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", sINHVIEN.HUYEN.MaTinh);
            ViewBag.MaHuyen = new SelectList(db.HUYENs, "MaHuyen", "TenHuyen", sINHVIEN.MaHuyen);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", sINHVIEN.MaNganh);
            return View(sINHVIEN);
        }

        // GET: SINHVIEN/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // POST: SINHVIEN/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
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
