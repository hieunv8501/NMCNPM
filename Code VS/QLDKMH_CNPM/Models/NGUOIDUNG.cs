namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NGUOIDUNG")]
    public partial class NGUOIDUNG
    {
        [Key]
        [StringLength(50)]
        public string TenDangNhap { get; set; }

        [Required]
        [StringLength(30)]
        public string MatKhau { get; set; }

        [StringLength(10)]
        public string MaNhom { get; set; }

        public virtual NHOMNGUOIDUNG NHOMNGUOIDUNG { get; set; }
    }
}
