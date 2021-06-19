using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLDKMH_CNPM.Models;

namespace QLDKMH_CNPM.Areas.PDT.Controllers
{
    public class DOITUONGsController : Controller
    {
        private CNPM_DBContext db = new CNPM_DBContext();
        //Hàm load danh sách đối tượng ưu tiên
        // GET: PDT/DOITUONGs
        public ActionResult Index()
        {
            return View(db.DOITUONGs.ToList());
        }
        //Hàm load thông tin chi tiết đối tượng ưu tiên
        // GET: PDT/DOITUONGs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOITUONG dOITUONG = db.DOITUONGs.Find(id);
            if (dOITUONG == null)
            {
                return HttpNotFound();
            }
            return View(dOITUONG);
        }
        //Hàm tạo mới thông tin đối tượng ưu tiên, dúng thì lưu lại, sai thì thông báo và nhập lại
        // GET: PDT/DOITUONGs/Create
        public ActionResult Create(int code = 0)
        {
            ViewBag.Message = "Dung";
            if (code == 1)
                ViewBag.Message = "Sai";
            return View();
        }

        // POST: PDT/DOITUONGs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDoiTuong,TenDoiTuong,TiLeGiamHocPhi")] DOITUONG dOITUONG)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.DOITUONGs.Add(dOITUONG);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(dOITUONG);
            }
            catch (Exception)
            {
                return RedirectToAction("Create", "DOITUONGs", new { code = 1 });
            }
        }
        //Hàm sửa thông tin chi tiết đối tượng ưu tiên
        // GET: PDT/DOITUONGs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOITUONG dOITUONG = db.DOITUONGs.Find(id);
            if (dOITUONG == null)
            {
                return HttpNotFound();
            }
            return View(dOITUONG);
        }

        // POST: PDT/DOITUONGs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDoiTuong,TenDoiTuong,TiLeGiamHocPhi")] DOITUONG dOITUONG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dOITUONG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dOITUONG);
        }
        //Hàm xóa thông tin chi tiết đối tượng ưu tiên
        // GET: PDT/DOITUONGs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DOITUONG dOITUONG = db.DOITUONGs.Find(id);
            if (dOITUONG == null)
            {
                return HttpNotFound();
            }
            return View(dOITUONG);
        }

        // POST: PDT/DOITUONGs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DOITUONG dOITUONG = db.DOITUONGs.Find(id);
            db.DOITUONGs.Remove(dOITUONG);
            db.SaveChanges();
            return RedirectToAction("Index");
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
