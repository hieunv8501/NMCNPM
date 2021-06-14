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
        public ActionResult Index()
        {
            return View(db.NGANHs.ToList());
        }

        // GET: SV/CHUONGTRINHHOCs/Details/5
        public ActionResult Details(string id)
        {
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
