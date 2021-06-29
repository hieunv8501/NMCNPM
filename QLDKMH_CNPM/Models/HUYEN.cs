namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HUYEN")]
    public partial class HUYEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HUYEN()
        {
            SINHVIENs = new HashSet<SINHVIEN>();
        }

        [Key]
        [StringLength(4)]
        public string MaHuyen { get; set; }

        [StringLength(30)]
        public string TenHuyen { get; set; }

        [Required]
        [StringLength(4)]
        public string MaTinh { get; set; }

        public bool? VungSauVungXa { get; set; }

        public virtual TINH TINH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SINHVIEN> SINHVIENs { get; set; }
    }
}
