namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUONGTRINHHOC")]
    public partial class CHUONGTRINHHOC
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(4)]
        public string MaNganh { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(7)]
        public string MaMonHoc { get; set; }

        public int HocKy { get; set; }

        [StringLength(50)]
        public string GhiChu { get; set; }

        public virtual MONHOC MONHOC { get; set; }

        public virtual NGANH NGANH { get; set; }
    }
}
