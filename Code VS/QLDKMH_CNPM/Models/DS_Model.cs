namespace QLDKMH_CNPM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;
    using System.Linq;
    using System.ComponentModel;
    using System.Web.Mvc;

    public class DS_model
    {
        public DS_model()
        {
            Publishers = new List<SelectListItem>();
        }
        public int MaHKNH { get; set; }
        public string MaSV { get; set; }
        public string HoTen { get; set; }

        public string MaNganh { get; set; }
        public int HocKy { get; set; }
        public int Nam1 { get; set; }

        public int Nam2 { get; set; }
        public decimal SoTienConLai { get; set; }

        public IEnumerable<SelectListItem> Publishers { get; set; }
    }
}