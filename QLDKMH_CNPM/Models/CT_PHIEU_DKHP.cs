namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_PHIEU_DKHP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhieuDKHP { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(11)]
        public string MaMo { get; set; }

        [StringLength(40)]
        public string GhiChu { get; set; }

        public virtual DS_MONHOC_MO DS_MONHOC_MO { get; set; }

        public virtual PHIEU_DKHP PHIEU_DKHP { get; set; }
    }
}
