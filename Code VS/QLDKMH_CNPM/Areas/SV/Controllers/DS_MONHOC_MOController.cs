using QLDKMH_CNPM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLDKMH_CNPM.Areas.SV.Controllers
{
    public class DS_MONHOC_MOController : Controller
    {

        private CNPM_DBContext db = new CNPM_DBContext();
        public ActionResult ListHK()
        {
            return View(db.HKNHs.ToList());
        }
        // GET: SV/DS_MONHOC_MO
        public ActionResult Index(int id)
        {
            ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
            ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
            ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
            ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
            return View(db.DS_MONHOC_MO.Where(x => x.MaHKNH == id).ToList());
        }
    }
}
