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
        private static int SoPhieuDKHP_int;
        private static int MaHKNH_int;

        // GET: SV/CT_PHIEU_DKHP/Index
        public ActionResult Index()
        {
            var cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Include(c => c.DS_MONHOC_MO).Include(c => c.PHIEU_DKHP);
            return View(cT_PHIEU_DKHP.ToList());
        }

        // GET: SV/CT_PHIEU_DKHP/Details/5
        public ActionResult Details(int? id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CT_PHIEU_DKHP cT_PHIEU_DKHP = db.CT_PHIEU_DKHP.Find(id);
            if (cT_PHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            return View(cT_PHIEU_DKHP);
        }

        // GET: SV/CT_PHIEU_DKHP/Create
        public ActionResult Create(int id, int HKNH, string mssv)
        {
            ViewData["TenDangNhap"] = mssv;
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
            
                ct_phieu_dkhp.DS_MONHOC_MO = db.DS_MONHOC_MO.Where(x => x.MaMo == ct_phieu_dkhp.MaMo).FirstOrDefault();
                if (DS_MONHOC_DA_DK.Where(x => x.MaMo == item.MaMo).Count() == 0)
                    dS_MONHOC_CT_PHIEUDKHP.Add(ct_phieu_dkhp);
            }

            SoPhieuDKHP_int = id;
            MaHKNH_int = HKNH;
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
            string[] ids = formCollection["MaMo"]?.Split(new char[] { ',' });
            if (ids == null)
                return RedirectToAction("Details", "PHIEU_DKHP", new { @id = SoPhieuDKHP_int });
            foreach (string id in ids)
            {
                //var ct_pdk = ds_ct_pdk.Find(m => m.MaMo == id);
                CT_PHIEU_DKHP cT_PHIEU_DKHP = new CT_PHIEU_DKHP();
                cT_PHIEU_DKHP.SoPhieuDKHP = SoPhieuDKHP_int;
                cT_PHIEU_DKHP.MaMo = id;
                cT_PHIEU_DKHP.GhiChu = null;
                if (cT_PHIEU_DKHP != null)
                {
                    this.db.CT_PHIEU_DKHP.Add(cT_PHIEU_DKHP);
                    this.db.SaveChanges();
                }
            }
            return RedirectToAction("Details", "PHIEU_DKHP", new { @id = SoPhieuDKHP_int });
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
        public ActionResult Delete(int? id, int? id_2)
        {
            ViewBag.Message = "Dung";
            if (id == null || id_2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PHIEU_DKHP pHIEU_DKHP = db.PHIEU_DKHP.Find(id);
            if (pHIEU_DKHP == null)
            {
                return HttpNotFound();
            }
            ViewBag.id = id_2;
            return View(pHIEU_DKHP);
        }

        // POST: SV/CT_PHIEUDK/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(FormCollection formCollection)
        {
            try
            {
                string[] ids = formCollection["MaMo"].Split(new char[] { ',' });

                string id = formCollection["SoPhieuDKHP"];
                foreach (string MaMonHoc in ids)
                {
                    var MonHoc = this.db.CT_PHIEU_DKHP.Find(int.Parse(id), MaMonHoc);
                    this.db.CT_PHIEU_DKHP.Remove(MonHoc);
                    this.db.SaveChanges();
                }
                return RedirectToAction("Details", "Phieu_DKHP", new { id = int.Parse(id) });
            }
            catch (Exception)
            {
                ViewBag.Message = "Sai";
                return RedirectToAction("Details", "Phieu_DKHP", new { id = int.Parse(formCollection["SoPhieuDKHP"]) });
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
