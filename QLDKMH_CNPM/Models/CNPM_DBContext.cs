using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace QLDKMH_CNPM.Models
{
    public partial class CNPM_DBContext : DbContext
    {
        public CNPM_DBContext() : base("name=CNPM_DBContext")
        {
        }

        public virtual DbSet<CHUCNANG> CHUCNANGs { get; set; }
        public virtual DbSet<CHUONGTRINHHOC> CHUONGTRINHHOCs { get; set; }
        public virtual DbSet<CT_PHIEU_DKHP> CT_PHIEU_DKHP { get; set; }
        public virtual DbSet<DOITUONG> DOITUONGs { get; set; }
        public virtual DbSet<DS_MONHOC_MO> DS_MONHOC_MO { get; set; }
        public virtual DbSet<DSSV_CHUAHOANTHANH_HP> DSSV_CHUAHOANTHANH_HP { get; set; }
        public virtual DbSet<HKNH> HKNHs { get; set; }
        public virtual DbSet<HUYEN> HUYENs { get; set; }
        public virtual DbSet<KHOA> KHOAs { get; set; }
        public virtual DbSet<LOAIMONHOC> LOAIMONHOCs { get; set; }
        public virtual DbSet<MONHOC> MONHOCs { get; set; }
        public virtual DbSet<NGANH> NGANHs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHOMNGUOIDUNG> NHOMNGUOIDUNGs { get; set; }
        public virtual DbSet<PHIEU_DKHP> PHIEU_DKHP { get; set; }
        public virtual DbSet<PHIEUTHU> PHIEUTHUs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<TINH> TINHs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CHUCNANG>()
                .Property(e => e.MaChucNang)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHUCNANG>()
                .HasMany(e => e.NHOMNGUOIDUNGs)
                .WithMany(e => e.CHUCNANGs)
                .Map(m => m.ToTable("PHANQUYEN").MapLeftKey("MaChucNang").MapRightKey("MaNhom"));

            modelBuilder.Entity<CHUONGTRINHHOC>()
                .Property(e => e.MaNganh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CHUONGTRINHHOC>()
                .Property(e => e.MaMonHoc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<CT_PHIEU_DKHP>()
                .Property(e => e.MaMo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DOITUONG>()
                .Property(e => e.MaDoiTuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DOITUONG>()
                .HasMany(e => e.SINHVIENs)
                .WithRequired(e => e.DOITUONG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DS_MONHOC_MO>()
                .Property(e => e.MaMo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DS_MONHOC_MO>()
                .Property(e => e.MaMonHoc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DS_MONHOC_MO>()
                .HasMany(e => e.CT_PHIEU_DKHP)
                .WithRequired(e => e.DS_MONHOC_MO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DSSV_CHUAHOANTHANH_HP>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<DSSV_CHUAHOANTHANH_HP>()
                .Property(e => e.SoTienConLai)
                .HasPrecision(19, 4);

            modelBuilder.Entity<HKNH>()
                .HasMany(e => e.DS_MONHOC_MO)
                .WithRequired(e => e.HKNH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HKNH>()
                .HasMany(e => e.DSSV_CHUAHOANTHANH_HP)
                .WithRequired(e => e.HKNH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HKNH>()
                .HasMany(e => e.PHIEU_DKHP)
                .WithRequired(e => e.HKNH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<HUYEN>()
                .Property(e => e.MaHuyen)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HUYEN>()
                .Property(e => e.MaTinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HUYEN>()
                .HasMany(e => e.SINHVIENs)
                .WithRequired(e => e.HUYEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHOA>()
                .Property(e => e.MaKhoa)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOAIMONHOC>()
                .Property(e => e.MaLoaiMon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LOAIMONHOC>()
                .Property(e => e.SoTienMotTinChi)
                .HasPrecision(19, 4);

            modelBuilder.Entity<LOAIMONHOC>()
                .HasMany(e => e.MONHOCs)
                .WithRequired(e => e.LOAIMONHOC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONHOC>()
                .Property(e => e.MaMonHoc)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MONHOC>()
                .Property(e => e.MaLoaiMon)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MONHOC>()
                .HasMany(e => e.CHUONGTRINHHOCs)
                .WithRequired(e => e.MONHOC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MONHOC>()
                .HasMany(e => e.DS_MONHOC_MO)
                .WithRequired(e => e.MONHOC)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGANH>()
                .Property(e => e.MaNganh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NGANH>()
                .Property(e => e.MaKhoa)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<NGANH>()
                .HasMany(e => e.CHUONGTRINHHOCs)
                .WithRequired(e => e.NGANH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGANH>()
                .HasMany(e => e.SINHVIENs)
                .WithRequired(e => e.NGANH)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.TenDangNhap)
                .IsUnicode(false);

            modelBuilder.Entity<NGUOIDUNG>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<NHOMNGUOIDUNG>()
                .HasMany(e => e.NGUOIDUNGs)
                .WithRequired(e => e.NHOMNGUOIDUNG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEU_DKHP>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<PHIEU_DKHP>()
                .Property(e => e.TongTienDangKy)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PHIEU_DKHP>()
                .Property(e => e.TongTienPhaiDong)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PHIEU_DKHP>()
                .Property(e => e.TongTienDaDong)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PHIEU_DKHP>()
                .Property(e => e.SoTienConLai)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PHIEU_DKHP>()
                .HasMany(e => e.CT_PHIEU_DKHP)
                .WithRequired(e => e.PHIEU_DKHP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEU_DKHP>()
                .HasMany(e => e.PHIEUTHUs)
                .WithRequired(e => e.PHIEU_DKHP)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PHIEUTHU>()
                .Property(e => e.SoTienThu)
                .HasPrecision(19, 4);

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.MaSV)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.MaNganh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.MaDoiTuong)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SINHVIEN>()
                .Property(e => e.MaHuyen)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SINHVIEN>()
                .HasMany(e => e.DSSV_CHUAHOANTHANH_HP)
                .WithRequired(e => e.SINHVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SINHVIEN>()
                .HasMany(e => e.PHIEU_DKHP)
                .WithRequired(e => e.SINHVIEN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TINH>()
                .Property(e => e.MaTinh)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TINH>()
                .HasMany(e => e.HUYENs)
                .WithRequired(e => e.TINH)
                .WillCascadeOnDelete(false);

            
            
        }
    }
}
