using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.Admin.Controllers
{
    public class NGUOIDUNGsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: Admin/NGUOIDUNGs
        public ActionResult Index()
        {
            var nGUOIDUNGs = db.NGUOIDUNGs.Include(n => n.NHOMNGUOIDUNG);
            return View(nGUOIDUNGs.ToList());
        }

        // GET: Admin/NGUOIDUNGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nGUOIDUNG);
        }

        // GET: Admin/NGUOIDUNGs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            ViewBag.MaNhom = new SelectList(db.NHOMNGUOIDUNGs, "MaNhom", "TenNhom");
            return View();
        }

        // POST: Admin/NGUOIDUNGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TenDangNhap,MatKhau,MaNhom")] NGUOIDUNG nGUOIDUNG)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.NGUOIDUNGs.Add(nGUOIDUNG);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaNhom = new SelectList(db.NHOMNGUOIDUNGs, "MaNhom", "TenNhom", nGUOIDUNG.MaNhom);
                return View(nGUOIDUNG);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "NGUOIDUNGs", new { code = 1 });
            }
        }

        // GET: Admin/NGUOIDUNGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNhom = new SelectList(db.NHOMNGUOIDUNGs, "MaNhom", "TenNhom", nGUOIDUNG.MaNhom);
            return View(nGUOIDUNG);
        }

        // POST: Admin/NGUOIDUNGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TenDangNhap,MatKhau,MaNhom")] NGUOIDUNG nGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nGUOIDUNG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNhom = new SelectList(db.NHOMNGUOIDUNGs, "MaNhom", "TenNhom", nGUOIDUNG.MaNhom);
            return View(nGUOIDUNG);
        }

        // GET: Admin/NGUOIDUNGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            if (nGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nGUOIDUNG);
        }

        // POST: Admin/NGUOIDUNGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NGUOIDUNG nGUOIDUNG = db.NGUOIDUNGs.Find(id);
            db.NGUOIDUNGs.Remove(nGUOIDUNG);
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
