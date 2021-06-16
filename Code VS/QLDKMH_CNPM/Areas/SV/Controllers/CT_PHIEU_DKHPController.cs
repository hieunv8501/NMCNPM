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
    public class CT_PHIEU_DKHPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();
        private static int _SoPhieuDKHP;
        private static int _MaHKNH;

        // GET: SV/CT_PHIEU_DKHP/Index
        //public ActionResult Index()
        //{
        //    var cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Include(c => c.DS_MONHOC_MO).Include(c => c.PHIEU_DKHP);
        //    return View(cT_PHIEU_DKHP.ToList());
        //}

        //// GET: SV/CT_PHIEU_DKHP/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Find(id);
        //    if (cT_PHIEU_DKHP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(cT_PHIEU_DKHP);
        //}

        // GET: SV/CT_PHIEU_DKHP/Create
        public ActionResult Create(int id = 0, int HKNH = 0)
        {
            CT_PHIEU_DKHP cT_PHIEU_DKHP = new CT_PHIEU_DKHP();
            ViewBag.MaMo = db.DS_MONHOC_MO.Where(x => x.MaHKNH == HKNH).ToList();
            //ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP, "SoPhieuDKHP", "MaSV");
            var DS_MONHOC_DA_DK = db.CT_PHIEU_DKHP.Where(x => x.SoPhieuDKHP == id);

            List<DS_MONHOC_MO> dS_MONHOC_MO = db.DS_MONHOC_MO.Where(x => x.MaHKNH == HKNH).ToList();
            List<CT_PHIEU_DKHP> dS_MONHOC_CT_PHIEUDKHP = new List<CT_PHIEU_DKHP>();

            foreach (DS_MONHOC_MO item in dS_MONHOC_MO)
            {
                CT_PHIEU_DKHP ct_phieu_dkhp = new CT_PHIEU_DKHP();
                ct_phieu_dkhp.SoPhieuDKHP = id;
                ct_phieu_dkhp.MaMo = item.MaMo;
                ct_phieu_dkhp.GhiChu = null;
                //ct.IsCheck = 0;
                //if (item.MaMo == "0001")
                //ct.IsCheck = 1;
                ct_phieu_dkhp.DS_MONHOC_MO = db.DS_MONHOC_MO.Where(x => x.MaMo == ct_phieu_dkhp.MaMo).FirstOrDefault();
                if (DS_MONHOC_DA_DK.Where(x => x.MaMo == item.MaMo).Count() == 0)
                    dS_MONHOC_CT_PHIEUDKHP.Add(ct_phieu_dkhp);
            }

            _SoPhieuDKHP = id;
            _MaHKNH = HKNH;
            ViewBag.SoPhieuDKHP = id;
            ViewBag.ListMonHoc = dS_MONHOC_CT_PHIEUDKHP.ToList();
            return View();
        }

        // POST: SV/CT_PHIEU_DKHP/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection formCollection)
        {
            string[] ids = formCollection["ID"]?.Split(new char[] { ',' });
            if (ids == null)
                return RedirectToAction("Details", "PHIEU_DKHP", new { @id = _SoPhieuDKHP });
            foreach (string id in ids)
            {
                //var ct_pdk = ds_ct_pdk.Find(m => m.MaMo == id);
                CT_PHIEU_DKHP cT_PHIEU_DKHP = new CT_PHIEU_DKHP();
                cT_PHIEU_DKHP.SoPhieuDKHP = _SoPhieuDKHP;
                cT_PHIEU_DKHP.MaMo = id;
                cT_PHIEU_DKHP.GhiChu = null;
                if (cT_PHIEU_DKHP != null)
                {
                    this.db.CT_PHIEU_DKHP.Add(cT_PHIEU_DKHP);
                    this.db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "PHIEU_DKHP", new { @id = _SoPhieuDKHP });
        }

        //// GET: SV/CT_PHIEU_DKHP/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Find(id);
        //    if (cT_PHIEU_DKHP == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.MaMo = new SelectList(db.DS_MONHOC_MO, "MaMo", "MaMonHoc", cT_PHIEU_DKHP.MaMo);
        //    ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP, "SoPhieuDKHP", "MaSV", cT_PHIEU_DKHP.SoPhieuDKHP);
        //    return View(cT_PHIEU_DKHP);
        //}

        //// POST: SV/CT_PHIEU_DKHP/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit([Bind(Include = "SoPhieuDKHP,MaMo,GhiChu")] CT_PHIEU_DKHP cT_PHIEU_DKHP)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(cT_PHIEU_DKHP).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            ViewBag.MaMo = new SelectList(db.DS_MONHOC_MO, "MaMo", "MaMonHoc", cT_PHIEU_DKHP.MaMo);
        //            ViewBag.SoPhieuDKHP = new SelectList(db.PHIEU_DKHP, "SoPhieuDKHP", "MaSV", cT_PHIEU_DKHP.SoPhieuDKHP);
        //            return View(cT_PHIEU_DKHP);
        //        }

        // GET: SV/CT_PHIEU_DKHP/Delete/5
        public ActionResult Delete(int id_PhieuDKHP, string id_MaMo, int id = 0)
        {
            ViewBag.Message = "Dung";
            if (id == 1)
                ViewBag.Message = "Sai";
            if (id_PhieuDKHP < 0 || id_MaMo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEU_DKHP cT_PHIEUDK = db.CT_PHIEU_DKHP.Where(x => x.SoPhieuDKHP == id_PhieuDKHP).Where(x => x.MaMo == id_MaMo).FirstOrDefault();
            if (cT_PHIEUDK == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEUDK);
        }

        // POST: SV/CT_PHIEUDK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id_PhieuDKHP, string id_MaMo)
        {
            try
            {
                CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Where(x => x.SoPhieuDKHP == id_PhieuDKHP).Where(x => x.MaMo == id_MaMo).FirstOrDefault();
                db.CT_PHIEU_DKHP.Remove(cT_PHIEU_DKHP);
                db.SaveChanges();
                return RedirectToAction("Details", "PHIEU_DKHP", new { @id = id_PhieuDKHP });
            }
            catch (Exception)
            {
                return RedirectToAction("Delete", "CT_PHIEU_DKHP", new { id_PhieuDKHP = id_PhieuDKHP, id_MaMo = id_MaMo, id = 1 });
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
