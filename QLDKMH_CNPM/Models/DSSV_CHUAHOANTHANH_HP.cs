namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DSSV_CHUAHOANTHANH_HP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHKNH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(6)]
        public string MaSV { get; set; }

        [Column(TypeName = "money")]
        public decimal SoTienConLai { get; set; }

        public virtual HKNH HKNH { get; set; }

        public virtual SINHVIEN SINHVIEN { get; set; }

        public object SoTienDangKy { get; internal set; }
        public object SoTienPhaiDong { get; internal set; }
    }
}
