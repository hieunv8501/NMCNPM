using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login/Login
        private CNPM_DBContext db = new CNPM_DBContext();
        public ActionResult Login(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            return View();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string TenDangNhap, string MatKhau)
        {
            var nGUOIDUNG = db.NGUOIDUNGs.Where(nd => nd.TenDangNhap == TenDangNhap & nd.MatKhau == MatKhau);
            if (nGUOIDUNG.ToList().Count() == 0)
            {
                return RedirectToAction("Login", "Login", new { code = 1 });
            }
            else
            {
                var AdminList = (from nd in db.NGUOIDUNGs
                                join nnd in db.NHOMNGUOIDUNGs
                                on nd.MaNhom equals nnd.MaNhom
                                where nnd.TenNhom.Equals("Admin")
                                 select nd.TenDangNhap);
                


                var PdtList = (from nd in db.NGUOIDUNGs
                              join nnd in db.NHOMNGUOIDUNGs
                              on nd.MaNhom equals nnd.MaNhom
                              where nnd.TenNhom.Equals("Phòng đào tạo")
                              select nd.TenDangNhap); 
                
                if (AdminList.Contains(TenDangNhap))
                {
                    return RedirectToAction("AdminHome", "Admin", new { area = "Admin" });
                }
                else if (PdtList.Contains(TenDangNhap))
                {
                    return RedirectToAction("PDTHome", "PDT", new { area = "PDT" });
                }
                return RedirectToAction("SVHome", "SV", new { area = "SV", TenDangNhap = TenDangNhap });
            }
            
        }
    }
}
