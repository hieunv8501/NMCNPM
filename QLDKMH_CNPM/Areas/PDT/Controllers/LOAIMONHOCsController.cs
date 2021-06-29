using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class LOAIMONHOCsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        // GET: PDT/LOAIMONHOCs
        public ActionResult Index(int code = 0)
        {
            ViewBag.code = code;
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
        public ActionResult Create(int code = 0)
        {
            ViewBag.code = code;
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
                if (db.LOAIMONHOCs.Where(x => x.MaLoaiMon == lOAIMONHOC.MaLoaiMon).Count() > 0)
                {
                    return RedirectToAction("Create", new { code = 1 });
                }
                string a = lOAIMONHOC.TenLoaiMon.Trim();
                Regex trimmer = new Regex(@"\s\s+");
                a = trimmer.Replace(a, " ");
                foreach (var item in db.LOAIMONHOCs)
                {
                    string b = item.TenLoaiMon.Trim();
                    b = trimmer.Replace(b, " ");
                    if (string.Compare(a, b, true) == 0)
                    {
                        return RedirectToAction("Create", new { code = 3 });
                    }
                }
                db.LOAIMONHOCs.Add(lOAIMONHOC);
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 10 });
            }

            return View(lOAIMONHOC);
        }

        // GET: PDT/LOAIMONHOCs/Edit/5
        public ActionResult Edit(string id, int code = 0)
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
            ViewBag.code = code;
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
                string a = lOAIMONHOC.TenLoaiMon.Trim();
                Regex trimmer = new Regex(@"\s\s+");
                a = trimmer.Replace(a, " ");
                List<LOAIMONHOC> LMH = db.LOAIMONHOCs.Where(x => x.MaLoaiMon != lOAIMONHOC.MaLoaiMon).ToList();
                foreach (var item in LMH)
                {

                    string b = item.TenLoaiMon.Trim();
                    b = trimmer.Replace(b, " ");
                    if (string.Compare(a, b, true) == 0)
                    {
                        return RedirectToAction("Edit", new { code = 3 });
                    }

                }
                db.Entry(lOAIMONHOC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { code = 12 });
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
            if (db.MONHOCs.Where(x => x.MaLoaiMon == id).Count() > 0)
            {
                return RedirectToAction("Index", new { code = 1 });
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
            return RedirectToAction("Index", new { code = 11 });
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
