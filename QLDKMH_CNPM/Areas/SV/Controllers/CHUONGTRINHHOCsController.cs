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
        //public ActionResult Index(string id )
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    string id_nganh = db.SINHVIENs.Find(id).MaNganh.ToString();
        //    var cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Include(c => c.MONHOC).Include(c => c.NGANH).Where(c => c.MaNganh == id_nganh).OrderBy(c => c.HocKy);
        //    return View(cHUONGTRINHHOC.ToList());
        //}

        // GET: SV/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id = "000001")
        {
            ViewData["TenDangNhap"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string id_nganh = db.SINHVIENs.Find(id).MaNganh.ToString();
            var cHUONGTRINHHOC = db.CHUONGTRINHHOCs.Include(c => c.MONHOC).Include(c => c.NGANH).Where(c => c.MaNganh == id_nganh).OrderBy(c => c.HocKy);
            return View(cHUONGTRINHHOC.ToList());
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
