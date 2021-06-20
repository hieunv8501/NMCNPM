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

        // GET: SV/CHUONGTRINHHOCs/Index
        public ActionResult Index(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["TenDangNhap"] = id;
            //string id_nganh = db.SINHVIENs.Find(id).MaNganh.ToString();
            //var cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Include(c => c.MONHOC).Include(c => c.NGANH).Where(c => c.MaNganh == id_nganh).OrderBy(c => c.HocKy);
            return View(db.NGANHs.ToList());
        }

        // GET: SV/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id, string id_2)
        {
            ViewData["TenDangNhap"] = id_2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.TenNganh = db.NGANHs.Find(id).TenNganh;
            ViewBag.MaNganh = db.NGANHs.Find(id).MaNganh;

            return View(db.CHUONGTRINHHOCs.Where(x => x.MaNganh == id).ToList());
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
