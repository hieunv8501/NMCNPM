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
    public class NHOMNGUOIDUNGsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: Admin/NHOMNGUOIDUNGs
        public ActionResult Index()
        {
            return View(db.NHOMNGUOIDUNGs.ToList());
        }

        // GET: Admin/NHOMNGUOIDUNGs/Details/5
        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMNGUOIDUNG nHOMNGUOIDUNG = db.NHOMNGUOIDUNGs.Find(id);
            if (nHOMNGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHOMNGUOIDUNG);
        }

        // GET: Admin/NHOMNGUOIDUNGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/NHOMNGUOIDUNGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNhom,TenNhom")] NHOMNGUOIDUNG nHOMNGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.NHOMNGUOIDUNGs.Add(nHOMNGUOIDUNG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nHOMNGUOIDUNG);
        }

        // GET: Admin/NHOMNGUOIDUNGs/Edit/5
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMNGUOIDUNG nHOMNGUOIDUNG = db.NHOMNGUOIDUNGs.Find(id);
            if (nHOMNGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHOMNGUOIDUNG);
        }

        // POST: Admin/NHOMNGUOIDUNGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNhom,TenNhom")] NHOMNGUOIDUNG nHOMNGUOIDUNG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nHOMNGUOIDUNG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nHOMNGUOIDUNG);
        }

        // GET: Admin/NHOMNGUOIDUNGs/Delete/5
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NHOMNGUOIDUNG nHOMNGUOIDUNG = db.NHOMNGUOIDUNGs.Find(id);
            if (nHOMNGUOIDUNG == null)
            {
                return HttpNotFound();
            }
            return View(nHOMNGUOIDUNG);
        }

        // POST: Admin/NHOMNGUOIDUNGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NHOMNGUOIDUNG nHOMNGUOIDUNG = db.NHOMNGUOIDUNGs.Find(id);
            db.NHOMNGUOIDUNGs.Remove(nHOMNGUOIDUNG);
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
