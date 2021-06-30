namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HKNH")]
    public partial class HKNH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HKNH()
        {
            DS_MONHOC_MO = new HashSet<DS_MONHOC_MO>();
            DSSV_CHUAHOANTHANH_HP = new HashSet<DSSV_CHUAHOANTHANH_HP>();
            PHIEU_DKHP = new HashSet<PHIEU_DKHP>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHKNH { get; set; }

        public int HocKy { get; set; }

        public int Nam1 { get; set; }

        public int Nam2 { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HanDongHocPhi { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DS_MONHOC_MO> DS_MONHOC_MO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DSSV_CHUAHOANTHANH_HP> DSSV_CHUAHOANTHANH_HP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEU_DKHP> PHIEU_DKHP { get; set; }
    }
}
