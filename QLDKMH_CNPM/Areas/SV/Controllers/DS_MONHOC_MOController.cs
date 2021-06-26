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
    public class DS_MONHOC_MOController : Controller
    {

        private CNPM_DBContext db = new CNPM_DBContext();
        public ActionResult ListHK(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["TenDangNhap"] = id;
            return View(db.HKNHs.ToList());
        }
        // GET: SV/DS_MONHOC_MO
        public ActionResult Index(int id, string id_2)
        {
            ViewData["TenDangNhap"] = id_2;
            ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            return View(db.DS_MONHOC_MO.Where(x => x.MaHKNH == id).ToList());
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
