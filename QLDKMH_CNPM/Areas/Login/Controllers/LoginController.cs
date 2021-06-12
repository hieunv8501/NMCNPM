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
        private CNPM_DBContext login = new CNPM_DBContext();
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ErrorPage()
        {
            return View();
        }
    }
}