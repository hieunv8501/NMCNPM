using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class DSSV_CHUAHOANTHANH_HPController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();

        public ActionResult ListHK(int code = 0)
        {
            ViewBag.message = "";
            if (code == 1)
                ViewBag.message = "Học kỳ này không có sinh viên nợ học phí";
            //if (code == 2)
            //    ViewBag.message = "Không thể xem báo cáo vì chưa đến hạn đóng học phí!";
            return View(db.HKNHs.ToList());
        }

        // GET: PDT/DSSV_CHUAHOANTHANH_HP/Index/5
        public ActionResult Index(int id)
        {
            if (DateTime.Now > db.HKNHs.Find(id).HanDongHocPhi)
            {
                ViewBag.MaHKNH = db.HKNHs.Find(id).MaHKNH;
                ViewBag.HocKy = db.HKNHs.Find(id).HocKy;
                ViewBag.Nam1 = db.HKNHs.Find(id).Nam1;
                ViewBag.Nam2 = db.HKNHs.Find(id).Nam2;
                var check = db.DSSV_CHUAHOANTHANH_HP.Where(x => x.MaHKNH == id);

                if (check.Count() == 0)
                {
                    string SQL = "INSERT INTO DSSV_CHUAHOANTHANH_HP(MaHKNH, MaSV, SoTienConLai) SELECT MaHKNH, MaSV, SoTienConLai FROM PHIEU_DKHP WHERE SoTienConLai > 0 AND MaHKNH = '" + id + "'";
                    db.Database.ExecuteSqlCommand(SQL);
                }
                List<SINHVIEN> SV = db.SINHVIENs.ToList();
                List<PHIEU_DKHP> PD = db.PHIEU_DKHP.ToList();
                List<DSSV_CHUAHOANTHANH_HP> DCH = db.DSSV_CHUAHOANTHANH_HP.ToList();

                var baocao = from P in PD
                             join D in DCH on P.MaHKNH equals D.MaHKNH into TB1
                             from D in TB1.ToList()
                             where P.MaSV == D.MaSV && D.MaHKNH == id
                             join S in SV on D.MaSV equals S.MaSV into TB2
                             from S in TB2.ToList()
                             select new DSSV_CHUAHOANTHANH_HP
                             {
                                 SINHVIEN = S,
                                 SoTienPhaiDong = P.TongTienPhaiDong,
                                 SoTienDangKy = P.TongTienDangKy,
                                 SoTienConLai = D.SoTienConLai,
                             };

                if (baocao.Count() == 0)
                    return RedirectToAction("ListHK", new { code = 1 });
                return View(baocao.ToList());
            }
            ViewBag.check = 0;
            return View();
        }

        public ActionResult ExportToExcel(int id)
        {
            var dSSV_CHUAHOANTHANH_HP = new System.Data.DataTable();
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Mã sinh viên", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Tên sinh viên", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Số tiền đăng ký", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Số tiền phải đóng", typeof(string));
            dSSV_CHUAHOANTHANH_HP.Columns.Add("Số tiền còn lại", typeof(string));

            List<SINHVIEN> SV = db.SINHVIENs.ToList();
            List<PHIEU_DKHP> PD = db.PHIEU_DKHP.ToList();
            List<DSSV_CHUAHOANTHANH_HP> DCH = db.DSSV_CHUAHOANTHANH_HP.ToList();

            var baocao = from P in PD
                         join D in DCH on P.MaHKNH equals D.MaHKNH into TB1
                         from D in TB1.ToList()
                         where P.MaSV == D.MaSV && D.MaHKNH == id
                         join S in SV on D.MaSV equals S.MaSV into TB2
                         from S in TB2.ToList()
                         select new DSSV_CHUAHOANTHANH_HP
                         {
                             SINHVIEN = S,
                             SoTienPhaiDong = P.TongTienPhaiDong,
                             SoTienDangKy = P.TongTienDangKy,
                             SoTienConLai = D.SoTienConLai,
                         };
            foreach (var item in baocao)
            {
                dSSV_CHUAHOANTHANH_HP.Rows.Add(item.SINHVIEN.MaSV, item.SINHVIEN.HoTen, item.SoTienDangKy, item.SoTienPhaiDong, item.SoTienConLai);
            }

            var grid = new GridView();
            grid.DataSource = dSSV_CHUAHOANTHANH_HP;
            grid.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DanhSachSinhVienNoHP.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);

            grid.RenderControl(htw);

            return Content(sw.ToString(), "application/ms-excel");
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
