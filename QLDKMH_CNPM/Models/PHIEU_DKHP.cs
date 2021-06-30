namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    public partial class PHIEU_DKHP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHIEU_DKHP()
        {
            CT_PHIEU_DKHP = new HashSet<CT_PHIEU_DKHP>();
            PHIEUTHUs = new HashSet<PHIEUTHU>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhieuDKHP { get; set; }

        [Required]
        [StringLength(6)]
        public string MaSV { get; set; }

        [Column(TypeName = "smalldatetime")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayLap { get; set; }

        public int MaHKNH { get; set; }

        public int TongTCLT { get; set; }

        public int TongTCTH { get; set; }

        [Column(TypeName = "money")]
        public decimal TongTienDangKy { get; set; }

        [Column(TypeName = "money")]
        public decimal TongTienPhaiDong { get; set; }

        [Column(TypeName = "money")]
        public decimal TongTienDaDong { get; set; }

        [Column(TypeName = "money")]
        public decimal SoTienConLai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_PHIEU_DKHP> CT_PHIEU_DKHP { get; set; }

        public virtual HKNH HKNH { get; set; }

        public virtual SINHVIEN SINHVIEN { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUTHU> PHIEUTHUs { get; set; }
    }
}
