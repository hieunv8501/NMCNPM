namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DS_MONHOC_MO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DS_MONHOC_MO()
        {
            CT_PHIEU_DKHP = new HashSet<CT_PHIEU_DKHP>();
        }

        [Key]
        [StringLength(11)]
        public string MaMo { get; set; }

        public int MaHKNH { get; set; }

        [Required]
        [StringLength(7)]
        public string MaMonHoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEU_DKHP> CT_PHIEU_DKHP { get; set; }

        public virtual MONHOC MONHOC { get; set; }

        public virtual HKNH HKNH { get; set; }
    }
}
