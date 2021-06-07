namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAIMONHOC")]
    public partial class LOAIMONHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LOAIMONHOC()
        {
            MONHOCs = new HashSet<MONHOC>();
        }

        [Key]
        [StringLength(2)]
        public string MaLoaiMon { get; set; }

        [StringLength(10)]
        public string TenLoaiMon { get; set; }

        public int? HeSoChia { get; set; }

        [Column(TypeName = "money")]
        public decimal? SoTienMotTinChi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MONHOC> MONHOCs { get; set; }
    }
}
