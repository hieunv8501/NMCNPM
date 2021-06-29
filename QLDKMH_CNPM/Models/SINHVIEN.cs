namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Data.Entity.Validation;

    [Table("SINHVIEN")]
    public partial class SINHVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SINHVIEN()
        {
            DSSV_CHUAHOANTHANH_HP = new HashSet<DSSV_CHUAHOANTHANH_HP>();
            PHIEU_DKHP = new HashSet<PHIEU_DKHP>();
        }

        [Key]
        [StringLength(6)]
        [MaxLength(6)]
        public string MaSV { get; set; }

        [Required]
        [StringLength(30)]
        public string HoTen { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgaySinh { get; set; }

        [StringLength(3)]
        public string GioiTinh { get; set; }

        [Required]
        [StringLength(4)]
        public string MaNganh { get; set; }

        [Required]
        [StringLength(4)]
        public string MaDoiTuong { get; set; }

        [Required]
        [StringLength(4)]
        public string MaHuyen { get; set; }

        public virtual DOITUONG DOITUONG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DSSV_CHUAHOANTHANH_HP> DSSV_CHUAHOANTHANH_HP { get; set; }

        public virtual HUYEN HUYEN { get; set; }

        public virtual NGANH NGANH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEU_DKHP> PHIEU_DKHP { get; set; }
    }
}
