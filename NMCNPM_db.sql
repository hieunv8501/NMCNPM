create database QLDKMH
go
use QLDKMH
go

set dateformat DMY

--create tables
--table SINHVIEN
create table SINHVIEN 
(
	MaSV char(6) primary key,
	HoTen nvarchar(30) not null,
	NgaySinh smalldatetime not null,
	GioiTinh nvarchar(3),
	MaHuyen char(4) not null,
	MaNganh char(4) not null,
	MaDoiTuong char(4) not null
)

--table DOITUONG
create table DOITUONG
(
	MaDoiTuong char(4) primary key,
	TenDoiTuong nvarchar(40),
	TiLeGiamHocPhi int
)

--table TINH
create table TINH
(	
	MaTinh char(4) primary key,
	TenTinh nvarchar(30)
)

--table HUYEN
create table HUYEN
(
	MaHuyen char(4) primary key,
	TenHuyen nvarchar(30),
	MaTinh char(4) not null,
	VungSauVungXa bit,
)

--table KHOA
create table KHOA
(
	MaKhoa char(4) primary key,
	TenKhoa nvarchar(40)
)

--table NGANH
create table NGANH 
(
	MaNganh char(4) primary key,
	TenNganh nvarchar(40),
	MaKhoa char(4)
)

--table LOAIMONHOC
CREATE TABLE LOAIMONHOC
(
	MaLoaiMon char(4) primary key,
	TenLoaiMon nvarchar(10),
	HeSoChia int,
	SoTienMotTinChi smallmoney
)

--table MONHOC
CREATE TABLE MONHOC
(
	MaMonHoc char(5) primary key,
	TenMonHoc nvarchar(50),
	MaLoaiMon char(4) not null,
	SoTiet int,
	SoTinChi int,
)

--table CHUONGTRINHHOC
CREATE TABLE CHUONGTRINHHOC	
(
	MaNganh	char(4) not null,
	MaMonHoc char(5) not null,
	HocKy	int,
	GhiChu nvarchar(50),
	primary key (MaNganh, MaMonHoc)
)

--table DS_MONHOC_MO
CREATE TABLE DS_MONHOC_MO
(
	MaMo char(11) primary key,
	MaHKNH int,
	MaMonHoc char(5)
)

--table PHIEU_DKHP
CREATE TABLE PHIEU_DKHP
(
	SoPhieuDKHP int primary key,
	MaSV char(6),
	NgayLap smalldatetime,
	MaHKNH int,
	TongTCLT int,
	TongTCTH int,
	TongTienDangKy money DEFAULT 0,
	TongTienPhaiDong money DEFAULT 0,
	TongTienDaDong money DEFAULT 0,
	SoTienConLai money DEFAULT 0
)
--table CT_PHIEU_DKHP
CREATE TABLE CT_PHIEU_DKHP
(
	SoPhieuDKHP int not null,
	MaMo char(11) not null,
	GhiChu nvarchar(40),
	primary key(SoPhieuDKHP, MaMo)
)
--table PHIEUTHU
CREATE TABLE PHIEUTHU
(
	SoPhieuThu int primary key,
	SoPhieuDKHP int not null, 
	NgayLap smalldatetime,
	SoTienThu smallmoney
)

--table HKNH
CREATE TABLE HKNH
(
	MaHKNH int primary key,
	HocKy int not null,
	Nam1 int,
	Nam2 int,
	HanDongHocPhi smalldatetime
)

--table DSSV_CHUAHOANTHANH_HP
CREATE TABLE DSSV_CHUAHOANTHANH_HP
(
	MaHKNH int not null,
	MaSV char(6) not null,
	SoTienConLai smallmoney,
	primary key (MaHKNH, MaSV)
)

-- TẠO CÁC TABLE CHO CHỨC NĂNG PHÂN QUYỀN

--Table CHUCNANG
CREATE TABLE CHUCNANG
(
	MaChucNang varchar(30) primary key,
	TenChucNang nvarchar(50),
	TenManHinhDuocLoad char(20)
)

/*Dữ liệu của Table này do người xây dựng hệ thống nhập đầy đủ cho nó
trước khi triển khai hệ thống cho người dùng cuối sử dụng.
Dữ liệu của các Table còn lại đều có thể được xem/thêm/xoá/sửa bởi
người quản trị hệ thống (người có quyền cao nhất)*/

--Table NHOMNGUOIDUNG
CREATE TABLE NHOMNGUOIDUNG
(
	MaNhom char(10) primary key,
	TenNhom nvarchar(50)
)

--Table PHANQUYEN
CREATE TABLE PHANQUYEN
(
	MaNhom char(10) references NHOMNGUOIDUNG(MaNhom),
	MaChucNang varchar(30) references CHUCNANG(MaChucNang),
	primary key(MaNhom, MaChucNang)
)

--TABLE NGUOIDUNG
CREATE TABLE NGUOIDUNG
(
	TenDangNhap varchar(50) primary key,
	MatKhau varchar(30) not null,
	MaNhom char(10) references NHOMNGUOIDUNG(MaNhom)
)

-- TẠO CÁC RÀNG BUỘC VỀ KHÓA NGOẠI
alter table HUYEN add constraint FK_HUYEN_TINH foreign key (MaTinh) references TINH(MaTinh)	 
alter table SINHVIEN add constraint FK_SV_HUYEN foreign key (MaHuyen) references HUYEN(MaHuyen)
alter table SINHVIEN add constraint FK_SV_NGANH foreign key (MaNganh) references NGANH(MaNganh)
alter table SINHVIEN add constraint FK_SV_DOITUONG foreign key (MaDoiTuong) references DOITUONG(MaDoiTuong)
alter table NGANH add constraint FK_NGANH_KHOA foreign key (MaKhoa) references KHOA(MaKhoa)
alter table MONHOC add constraint FK_MONHOC_LOAIMONHOC foreign key (MaLoaiMon) references LOAIMONHOC(MaLoaiMon)
alter table CHUONGTRINHHOC add constraint FK_CTH_NGANH foreign key (MaNganh) references NGANH(MaNganh)
alter table CHUONGTRINHHOC add constraint FK_CTH_MONHOC foreign key (MaMonHoc) references MONHOC(MaMonHoc)
alter table PHIEUTHU add constraint FK_PHIEUTHU_PHIEUDKHP foreign key (SoPhieuDKHP) references PHIEU_DKHP(SoPhieuDKHP)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__HKNH foreign key (MaHKNH) references HKNH(MaHKNH)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__SINHVIEN foreign key (MaSV) references SINHVIEN(MaSV)
alter table DS_MONHOC_MO add constraint FK_MAHKNH_HKNH FOREIGN KEY (MaHKNH) references HKNH(MaHKNH)
alter table PHIEU_DKHP add constraint FK_MASV_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV)
alter table CT_PHIEU_DKHP ADD
CONSTRAINT FK_MaMo_DS_MONHOC_MO FOREIGN KEY (MaMo) references DS_MONHOC_MO(MaMo),
CONSTRAINT FK_SoPhieuDKHP_PHIEU_DKHP FOREIGN KEY (SoPhieuDKHP) REFERENCES PHIEU_DKHP(SoPhieuDKHP)

-- TẠO CÁC RÀNG BUỘC CHECK
alter table SINHVIEN add constraint CHECK_GIOITINH check (GioiTinh in (N'Nam', N'Nữ'))
alter table DOITUONG add constraint CHECK_TILE check (TiLeGiamHocPhi >= 0)
alter table LOAIMONHOC add constraint CHECK_HESOCHIA check (HeSoChia > 0)
alter table LOAIMONHOC add constraint CHECK_SOTIENMOTINCHI check (SoTienMotTinChi > 0)
alter table MONHOC add constraint CHECK_SOTIET check (SoTiet > 0)
alter table MONHOC add constraint CHECK_SOTC check (SoTinChi > 0)

--Tính tổng số tín chỉ của PHIEU_DKHP
GO
CREATE TRIGGER TRIG_TONGTINCHI ON CT_PHIEU_DKHP
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SOPHIEU INT, @MAMO CHAR(11), @SOTINCHILT INT, @SOTINCHITH INT, @TONGSOTINCHILT INT, @TONGSOTINCHITH INT
	--LẤY THÔNG TIN CỦA CT_PHIEU_DKHP MỚI THÊM VÀO HOẶC MỚI UPDATE
	SELECT @SOPHIEU = SoPhieuDKHP, @MAMO = MaMo FROM inserted
	--TÌM SỐ TÍN CHỈ CỦA MÔN HỌC MỚI THÊM
	SELECT @SOTINCHILT = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'LT' AND B.MaMo = @MAMO
	SELECT @SOTINCHITH = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'TH' AND B.MaMo = @MAMO
	SELECT @TONGSOTINCHILT = TongTCLT FROM PHIEU_DKHP WHERE SoPhieuDKHP = @SOPHIEU 
	SELECT @TONGSOTINCHITH = TongTCTH FROM PHIEU_DKHP WHERE SoPhieuDKHP = @SOPHIEU 
	SET @TONGSOTINCHILT = @TONGSOTINCHILT + @SOTINCHILT
	SET @TONGSOTINCHITH = @TONGSOTINCHITH + @SOTINCHITH
	UPDATE PHIEU_DKHP SET TongTCLT = @TONGSOTINCHILT WHERE SoPhieuDKHP = @SOPHIEU 
	UPDATE PHIEU_DKHP SET TongTCTH = @TONGSOTINCHITH WHERE SoPhieuDKHP = @SOPHIEU 
END
GO


