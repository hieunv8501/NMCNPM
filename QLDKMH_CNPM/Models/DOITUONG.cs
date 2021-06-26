namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DOITUONG")]
    public partial class DOITUONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DOITUONG()
        {
            SINHVIENs = new HashSet<SINHVIEN>();
        }

        [Key]
        [StringLength(4)]
        public string MaDoiTuong { get; set; }

        [StringLength(40)]
        public string TenDoiTuong { get; set; }

        public int? TiLeGiamHocPhi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SINHVIEN> SINHVIENs { get; set; }
    }
}
