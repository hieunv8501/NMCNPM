using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class TINHsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/TINHs
        public ActionResult Index()
        {
            return View(db.TINHs.ToList());
        }

        // GET: PDT/TINHs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            return View(tINH);
        }

        // GET: PDT/TINHs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            return View();
        }

        // POST: PDT/TINHs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTinh,TenTinh")] TINH tINH)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TINHs.Add(tINH);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(tINH);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "TINHs", new { code = 1 });
            }
        }

        // GET: PDT/TINHs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            return View(tINH);
        }

        // POST: PDT/TINHs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTinh,TenTinh")] TINH tINH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tINH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tINH);
        }

        // GET: PDT/TINHs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TINH tINH = db.TINHs.Find(id);
            if (tINH == null)
            {
                return HttpNotFound();
            }
            return View(tINH);
        }

        // POST: PDT/TINHs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TINH tINH = db.TINHs.Find(id);
            db.TINHs.Remove(tINH);
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
