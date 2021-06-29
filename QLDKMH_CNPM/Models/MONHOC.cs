namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MONHOC")]
    public partial class MONHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONHOC()
        {
            CHUONGTRINHHOCs = new HashSet<CHUONGTRINHHOC>();
            DS_MONHOC_MO = new HashSet<DS_MONHOC_MO>();
        }

        [Key]
        [StringLength(7)]
        public string MaMonHoc { get; set; }

        [StringLength(50)]
        public string TenMonHoc { get; set; }

        [Required]
        [StringLength(2)]
        public string MaLoaiMon { get; set; }

        public int? SoTiet { get; set; }

        public int? SoTinChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUONGTRINHHOC> CHUONGTRINHHOCs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_MONHOC_MO> DS_MONHOC_MO { get; set; }

        public virtual LOAIMONHOC LOAIMONHOC { get; set; }
    }
}
