using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.SV.Controllers
{
    public class SVController : Controller
    {
        // GET: SV/SV
        private CNPM_DBContext db = new CNPM_DBContext();
        public ActionResult SVHome(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        public ActionResult SVmodule0(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        public ActionResult SVmodule1(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        public ActionResult SVmodule2(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        public ActionResult SVmodule3(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
    }
}