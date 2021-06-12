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
    public class HUYENsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/HUYENs
        public ActionResult Index()
        {
            var hUYENs = db.HUYENs.Include(h => h.TINH);
            return View(hUYENs.ToList());
        }

        // GET: PDT/HUYENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            return View(hUYEN);
        }

        // GET: PDT/HUYENs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.m = "Dung";
            if (code == 1)
                ViewBag.m = "Sai";
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh");
            return View();
        }

        // POST: PDT/HUYENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHuyen,TenHuyen,MaTinh,VungSauVungXa")] HUYEN hUYEN)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.HUYENs.Add(hUYEN);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
                return View(hUYEN);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "HUYENs", new { code = 1 });
            }
        }

        // GET: PDT/HUYENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
            return View(hUYEN);
        }

        // POST: PDT/HUYENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHuyen,TenHuyen,MaTinh,VungSauVungXa")] HUYEN hUYEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hUYEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTinh = new SelectList(db.TINHs, "MaTinh", "TenTinh", hUYEN.MaTinh);
            return View(hUYEN);
        }

        // GET: PDT/HUYENs/Delete/5
        public ActionResult Delete(string id, int code = 0)
        {

            ViewBag.m = "Dung";
            if (code == 1)
                ViewBag.m = "Sai";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HUYEN hUYEN = db.HUYENs.Find(id);
            if (hUYEN == null)
            {
                return HttpNotFound();
            }
            return View(hUYEN);
        }

        // POST: PDT/HUYENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                HUYEN hUYEN = db.HUYENs.Find(id);
                db.HUYENs.Remove(hUYEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", "HUYENs", new { id, code = 1 });
            }
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
