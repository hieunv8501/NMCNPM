--create database QLDKMH
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

--table PHIEUTHU
CREATE TABLE PHIEUTHU
(
	MaPhieuThu int primary key,
	MaPhieuDKHP int not null, --References to DKHP(SoPhieuDKHP),
	NgayLap smalldatetime not null ,
	SoTienThu money not null
)

--table HOCKY_NAMHOC
CREATE TABLE HOCKY_NAMHOC
(
	MaHKNH int primary key,
	HocKy int not null, --References to DKHP(HocKy),
	NamNK1 int,
	NamNK2 int,
	HanDongHocPhi smalldatetime not null
)

--table DSSV_CHUAHOANTHANH_HP
CREATE TABLE DSSV_CHUAHOANTHANH_HP
(
	STT int,
	MaHKNH int not null,
	MaSV char(6) not null,--References SINHVIEN(MaSV)
	SoTienConLai money not null,
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
--Table cho chuc nang phan quyen.
--Table Chuc Nang
CREATE TABLE CHUCNANG(MaChucNang VARCHAR(30) primary key ,TenChucNang NVARCHAR(50),TenManHinhDuocLoad CHAR(20))
/*Dữ liệu của Table này do người xây dựng hệ thống nhập đầy đủ cho nó
trước khi triển khai hệ thống cho người dùng cuối sử dụng.
Dữ liệu của các Table còn lại đều có thể được xem/thêm/xoá/sửa bởi
người quản trị hệ thống (người có quyền cao nhất)*/
CREATE TABLE NHOMNGUOIDUNG(MaNhom CHAR(10) primary key,TenNhom NVARCHAR(50))
CREATE TABLE PHANQUYEN(MaNhom CHAR(10) references NHOMNGUOIDUNG(MaNhom),MaChucNang VARCHAR(30) REFERENCES CHUCNANG(MaChucNang))
CREATE TABLE NGUOIDUNG(TenDangNhap VARCHAR(50),MatKhau VARCHAR(20),MaNhom CHAR(10)REFERENCES NHOMNGUOIDUNG(MaNhom))



