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
	MaLoaiMon char(4) primary key,
	TenLoaiMon nvarchar(10),
	TỉLeChia int,
	SoTienTC smallmoney
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

--table HOCKY
CREATE TABLE HOCKY
(
	MaHocKy	char(3) primary key,
	TenHocKy nvarchar(20)
)

--table CHUONGTRINHHOC
CREATE TABLE CHUONGTRINHHOC	
(
	MaNganh	char(4) not null,
	MaMonHoc char(5) not null,
	MaHocKy	char(3),
	GhiChu nvarchar(50),
	primary key (MaNganh, MaMonHoc)
)


--table PHIEUTHU
CREATE TABLE PHIEUTHU
(
	MaPhieuThu int primary key,
	MaPhieuDKHP int not null, 
	NgayLap smalldatetime,
	SoTienThu smallmoney
)

--table HKNH
CREATE TABLE HKNH
(
	MaHKNH int primary key,
	HocKy int not null,
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
	TenDangNhap varchar(50),
	MatKhau varchar(30),
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
alter table CHUONGTRINHHOC add constraint FK_CTH_HOCKY foreign key (MaHocKy) references HOCKY(MaHocKy)
alter table PHIEUTHU add constraint FK_PHIEUTHU_PHIEUDKHP foreign key (MaPhieuDKHP) references PHIEU_DKHP(MaPhieuDKHP)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__HKNH foreign key (MaHKNH) references HKNH(MaHKNH)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__SINHVIEN foreign key (MaSV) references SINHVIEN(MaSV)

-- TẠO CÁC RÀNG BUỘC CHECK
alter table SINHVIEN add constraint CHECK_GIOITINH check (GioiTinh in (N'Nam', N'Nữ'))
alter table DOITUONG add constraint CHECK_TILE check (TiLeGiamHP >= 0)
alter table LOAIMONHOC add constraint CHECK_TILECHIA check (TiLeChia > 0)
alter table LOAIMONHOC add constraint CHECK_SOTIENTC check (SoTienTC > 0)
alter table MONHOC add constraint CHECK_SOTIET check (SoTiet > 0)
alter table MONHOC add constraint CHECK_SOTC check (SoTinChi > 0)