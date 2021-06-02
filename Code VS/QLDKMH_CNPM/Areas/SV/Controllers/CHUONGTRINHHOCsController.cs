using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.SV.Controllers
{
    public class CHUONGTRINHHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: SV/CHUONGTRINHHOCs
        public ActionResult Index()
        {
            var cHUONGTRINHHOCs = db.CHUONGTRINHHOCs.Include(c => c.MONHOC).Include(c => c.NGANH);
            return View(cHUONGTRINHHOCs.ToList());
        }

        // GET: SV/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // GET: SV/CHUONGTRINHHOCs/Create
        public ActionResult Create()
        {
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh");
            return View();
        }

        // POST: SV/CHUONGTRINHHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNganh,MaMonHoc,HocKy,GhiChu")] CHUONGTRINHHOC cHUONGTRINHHOC)
        {
            if (ModelState.IsValid)
            {
                db.CHUONGTRINHHOCs.Add(cHUONGTRINHHOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", cHUONGTRINHHOC.MaMonHoc);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", cHUONGTRINHHOC.MaNganh);
            return View(cHUONGTRINHHOC);
        }

        // GET: SV/CHUONGTRINHHOCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", cHUONGTRINHHOC.MaMonHoc);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", cHUONGTRINHHOC.MaNganh);
            return View(cHUONGTRINHHOC);
        }

        // POST: SV/CHUONGTRINHHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNganh,MaMonHoc,HocKy,GhiChu")] CHUONGTRINHHOC cHUONGTRINHHOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cHUONGTRINHHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc", cHUONGTRINHHOC.MaMonHoc);
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh", cHUONGTRINHHOC.MaNganh);
            return View(cHUONGTRINHHOC);
        }

        // GET: SV/CHUONGTRINHHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // POST: SV/CHUONGTRINHHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id);
            db.CHUONGTRINHHOCs.Remove(cHUONGTRINHHOC);
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
