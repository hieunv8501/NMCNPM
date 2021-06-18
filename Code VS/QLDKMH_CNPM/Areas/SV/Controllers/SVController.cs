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

        // Module load trang chủ của sinh viên khi có mã sinh viên, load trả về view data của sinh viên đó
        public ActionResult SVHome(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }

        //Module0 load chức năng xem giao diện sự kiện của sinh viên khi có mã sinh viên, load trả về view data của sinh viên đó
        public ActionResult SVmodule0(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        //Module0 load chức năng xem giao diện thông báo của sinh viên khi có mã sinh viên, load trả về view data của sinh viên đó
        public ActionResult SVmodule1(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        //Module0 load chức năng xem giao diện kế hoạch năm của sinh viên khi có mã sinh viên, load trả về view data của sinh viên đó
        public ActionResult SVmodule2(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
        //Module0 load chức năng xem giao diện quy định - hướng dẫn của sinh viên khi có mã sinh viên, load trả về view data của sinh viên đó
        public ActionResult SVmodule3(string TenDangNhap)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.FirstOrDefault(sv => sv.MaSV == TenDangNhap);

            ViewData["TenDangNhap"] = sINHVIEN.MaSV;
            return View();
        }
    }
}