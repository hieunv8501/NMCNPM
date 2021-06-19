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
    public class CHUONGTRINHHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/CHUONGTRINHHOCs
        public ActionResult Index()
        {
            return View(db.NGANHs.ToList());
        }

        // GET: PDT/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id)
        {
            ViewBag.TenNganh = db.NGANHs.Find(id).TenNganh;
            ViewBag.MaNganh = db.NGANHs.Find(id).MaNganh;

            return View(db.CHUONGTRINHHOCs.Where(x => x.MaNganh == id).ToList());
        }

        // GET: PDT/CHUONGTRINHHOCs/Create
        public ActionResult Create(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            ViewBag.MaNganh = db.NGANHs.Find(id).MaNganh;
            ViewBag.TenNganh = db.NGANHs.Find(id).TenNganh;

            return View();
        }

        // POST: PDT/CHUONGTRINHHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string id, [Bind(Include = "MaNganh,MaMonHoc,HocKy,GhiChu")] CHUONGTRINHHOC cHUONGTRINHHOC)
        {
            if (ModelState.IsValid)
            {
                cHUONGTRINHHOC.MaNganh = id;
                db.CHUONGTRINHHOCs.Add(cHUONGTRINHHOC);
                db.SaveChanges();
                ViewBag.Message = "Your application description page.";
                return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh });
            }

            ViewBag.MaMonHoc = new SelectList(db.MONHOCs, "MaMonHoc", "TenMonHoc");
            ViewBag.MaNganh = new SelectList(db.NGANHs, "MaNganh", "TenNganh");
            return View(cHUONGTRINHHOC);
        }

        // GET: PDT/CHUONGTRINHHOCs/Edit/5
        public ActionResult Edit(string id_1, string id_2)
        {
            if (id_1 == null || id_2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // POST: PDT/CHUONGTRINHHOCs/Edit/5
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
                return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh });
            }
            return View(cHUONGTRINHHOC);
        }

        // GET: PDT/CHUONGTRINHHOCs/Delete/5
        public ActionResult Delete(string id_1, string id_2)
        {
            if (id_1 == null || id_2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            if (cHUONGTRINHHOC == null)
            {
                return HttpNotFound();
            }
            return View(cHUONGTRINHHOC);
        }

        // POST: PDT/CHUONGTRINHHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id_1, string id_2, FormCollection collection)
        {
            CHUONGTRINHHOC cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Find(id_1, id_2);
            db.CHUONGTRINHHOCs.Remove(cHUONGTRINHHOC);
            db.SaveChanges();
            return RedirectToAction("Details", new { id = cHUONGTRINHHOC.MaNganh });
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
