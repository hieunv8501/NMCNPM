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
	MaNganh char(4) not null,
	MaDoiTuong char(4) not null,
	MaHuyen char(4) not null,
)

--table DOITUONG
create table DOITUONG
(
	MaDoiTuong char(4) primary key,
	TenDoiTuong nvarchar(40),
	TiLeGiamHP int
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
	UuTien bit,
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
	MaLoai		char(4) PRIMARY KEY,
	TenLoai		nvarchar(10),
	TỉLeChia	int,
)

--table MONHOC
CREATE TABLE MONHOC
(
	MaMonHoc	char(5) PRIMARY KEY,
	TenMonHoc	nvarchar(50),
	MaLoai		char(4) CONSTRAINT FK_MH_LMH FOREIGN KEY REFERENCES LOAIMONHOC(MaLoai),
	SoTiet		int,
	SoTinChi	int,
)

--table HOCKY
CREATE TABLE HOCKY
(
	MaHocKy		char(3) PRIMARY KEY,
	TenHocKy	nvarchar(20)
)

--table CT_CHUONGTRINHHOC
CREATE TABLE CT_CHUONGTRINHHOC	
(
	MaNganh		char(4) CONSTRAINT FK_CTCTH_NGANH FOREIGN KEY REFERENCES NGANH(MaNganh),
	MaMonHoc	char(5) CONSTRAINT FK_CTCTH_MONHOC FOREIGN KEY REFERENCES MONHOC(MaMonHoc),
	MaHocKy		char(3) CONSTRAINT FK_CTCTH_HOCKY FOREIGN KEY REFERENCES HOCKY(MaHocKy),
	GhiChu		nvarchar(50),
	PRIMARY KEY (MaNganh, MaMonHoc)
)

--table PHIEUTHU
CREATE TABLE PHIEUTHU
(
	MaPhieuThu int primary key,
	MaPhieuDKHP int not null, --References to DKHP(MaPhieuDKHP),
	NgayLap smalldatetime,
	SoTienThu money
)

--table HOCKY_NAMHOC
CREATE TABLE HOCKY_NAMHOC
(
	MaHKNH int primary key,
	HocKy int not null, --References to DKHP(HocKy),
	NamNK1 int,
	NamNK2 int,
	HanDongHocPhi smalldatetime
)

--table DSSV_CHUAHOANTHANH_HP
CREATE TABLE DSSV_CHUAHOANTHANH_HP
(
	STT int,
	MaHKNH int not null,
	MaSV char(6) not null,
	SoTienConLai money,
	primary key (MaHKNH, MaSV)
)


-- CREATE CHECK CONSTRAINTS
alter table SINHVIEN add constraint CHECK_GIOITINH check (GioiTinh in (N'Nam', N'Nữ'))
alter table DOITUONG add constraint CHECK_TILE check (TiLeGiamHP >= 0)



-- CREATE FOREIGN KEY CONSTRAINTS
alter table HUYEN add constraint FK_HUYEN_TINH foreign key (MaTinh) references TINH(MaTinh)	 
alter table SINHVIEN add constraint FK_SV_HUYEN foreign key (MaHuyen) references HUYEN(MaHuyen)
alter table SINHVIEN add constraint FK_SV_NGANH foreign key (MaNganh) references NGANH(MaNganh)
alter table SINHVIEN add constraint FK_SV_DOITUONG foreign key (MaDoiTuong) references DOITUONG(MaDoiTuong)
alter table NGANH add constraint FK_NGANH_KHOA foreign key (MaKhoa) references KHOA(MaKhoa)

