namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHUCNANG")]
    public partial class CHUCNANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CHUCNANG()
        {
            NHOMNGUOIDUNGs = new HashSet<NHOMNGUOIDUNG>();
        }

        [Key]
        [StringLength(10)]
        public string MaChucNang { get; set; }

        [StringLength(30)]
        public string TenChucNang { get; set; }

        [StringLength(30)]
        public string TenManHinhDuocLoad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHOMNGUOIDUNG> NHOMNGUOIDUNGs { get; set; }
    }
}
