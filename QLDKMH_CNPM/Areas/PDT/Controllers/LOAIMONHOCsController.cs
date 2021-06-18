﻿using System;
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
    public class LOAIMONHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/LOAIMONHOCs
        public ActionResult Index()
        {
            return View(db.LOAIMONHOCs.ToList());
        }

        // GET: PDT/LOAIMONHOCs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PDT/LOAIMONHOCs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiMon,TenLoaiMon,HeSoChia,SoTienMotTinChi")] LOAIMONHOC lOAIMONHOC)
        {
            if (ModelState.IsValid)
            {
                db.LOAIMONHOCs.Add(lOAIMONHOC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMONHOC);
        }

        // POST: PDT/LOAIMONHOCs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiMon,TenLoaiMon,HeSoChia,SoTienMotTinChi")] LOAIMONHOC lOAIMONHOC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lOAIMONHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            if (lOAIMONHOC == null)
            {
                return HttpNotFound();
            }
            return View(lOAIMONHOC);
        }

        // POST: PDT/LOAIMONHOCs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            LOAIMONHOC lOAIMONHOC = db.LOAIMONHOCs.Find(id);
            db.LOAIMONHOCs.Remove(lOAIMONHOC);
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
