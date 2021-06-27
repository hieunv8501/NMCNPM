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
    public class CHUCNANGsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: Admin/CHUCNANGs
        public ActionResult Index()
        {
            return View(db.CHUCNANGs.ToList());
        }

        // GET: Admin/CHUCNANGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCNANG cHUCNANG = db.CHUCNANGs.Find(id);
            if (cHUCNANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCNANG);
        }

        // GET: Admin/CHUCNANGs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CHUCNANGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChucNang,TenChucNang,TenManHinhDuocLoad")] CHUCNANG cHUCNANG)
        {
            if (ModelState.IsValid)
            {
                db.CHUCNANGs.Add(cHUCNANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cHUCNANG);
        }

        // GET: Admin/CHUCNANGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCNANG cHUCNANG = db.CHUCNANGs.Find(id);
            if (cHUCNANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCNANG);
        }

        // POST: Admin/CHUCNANGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChucNang,TenChucNang,TenManHinhDuocLoad")] CHUCNANG cHUCNANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUCNANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cHUCNANG);
        }

        // GET: Admin/CHUCNANGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUCNANG cHUCNANG = db.CHUCNANGs.Find(id);
            if (cHUCNANG == null)
            {
                return HttpNotFound();
            }
            return View(cHUCNANG);
        }

        // POST: Admin/CHUCNANGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHUCNANG cHUCNANG = db.CHUCNANGs.Find(id);
            db.CHUCNANGs.Remove(cHUCNANG);
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
