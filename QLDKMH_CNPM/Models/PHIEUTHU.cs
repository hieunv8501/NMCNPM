namespace QLDKMH_CNPM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHIEUTHU")]
    public partial class PHIEUTHU
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SoPhieuThu { get; set; }

        public int SoPhieuDKHP { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime NgayLap { get; set; }

        [Column(TypeName = "money")]
        
        public decimal SoTienThu { get; set; }

        public virtual PHIEU_DKHP PHIEU_DKHP { get; set; }
    }
}
