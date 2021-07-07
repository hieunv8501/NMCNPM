create database CNPM_db
go
use CNPM_db
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
	MaHuyen char(4) not null
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
	MaLoaiMon char(2) primary key,
	TenLoaiMon nvarchar(10),
	SoTietMonTinChi int,
	SoTienMotTinChi money
)

--table MONHOC
CREATE TABLE MONHOC
(
	MaMonHoc char(7) primary key,
	TenMonHoc nvarchar(50),
	MaLoaiMon char(2) not null,
	SoTiet int,
	SoTinChi int,
)

--table CHUONGTRINHHOC
CREATE TABLE CHUONGTRINHHOC	
(
	MaNganh	char(4) not null,
	MaMonHoc char(7) not null,
	HocKy int not null,
	GhiChu nvarchar(50),
	primary key (MaNganh, MaMonHoc)
)

--table DS_MONHOC_MO
CREATE TABLE DS_MONHOC_MO
(
	MaMo char(11) primary key,
	MaHKNH int not null,
	MaMonHoc char(7) not null
)

--table PHIEU_DKHP
CREATE TABLE PHIEU_DKHP
(
	SoPhieuDKHP int primary key,
	MaSV char(6) not null,
	NgayLap smalldatetime not null,
	MaHKNH int not null,
	TongTCLT int DEFAULT 0 not null,
	TongTCTH int DEFAULT 0 not null,
	TongTienDangKy money DEFAULT 0 not null,
	TongTienPhaiDong money DEFAULT 0 not null,
	TongTienDaDong money DEFAULT 0 not null,
	SoTienConLai money DEFAULT 0 not null
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
	NgayLap smalldatetime not null,
	SoTienThu money not null
)

--table HKNH
CREATE TABLE HKNH
(
	MaHKNH int primary key,
	HocKy int not null,
	Nam1 int not null,
	Nam2 int not null,
	HanDongHocPhi smalldatetime not null
)

--table DSSV_CHUAHOANTHANH_HP
CREATE TABLE DSSV_CHUAHOANTHANH_HP
(
	MaHKNH int not null,
	MaSV char(6) not null,
	SoTienConLai money not null,
	primary key (MaHKNH, MaSV)
)

-- TẠO CÁC TABLE CHO CHỨC NĂNG PHÂN QUYỀN CỐ ĐỊNH
--Table NHOMNGUOIDUNG
CREATE TABLE NHOMNGUOIDUNG
(
	MaNhom int primary key,
	TenNhom nvarchar(30)
)

--TABLE NGUOIDUNG
CREATE TABLE NGUOIDUNG
(
	TenDangNhap varchar(30) primary key,
	MatKhau varchar(30) not null,
	MaNhom int not null
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
alter table DS_MONHOC_MO add constraint FK_MAHKNH_HKNH FOREIGN KEY (MaHKNH) references HKNH(MaHKNH)
alter table DS_MONHOC_MO add constraint FK_DS_MONHOCMO_MONHOC foreign key (MaMonHoc) references MONHOC(MaMonHoc)
alter table PHIEU_DKHP add constraint FK_MASV_SINHVIEN FOREIGN KEY (MaSV) REFERENCES SINHVIEN(MaSV)
alter table PHIEU_DKHP add constraint FK_PHIEUDKHP_HKNH foreign key (MaHKNH) references HKNH(MaHKNH)
alter table CT_PHIEU_DKHP ADD CONSTRAINT FK_MaMo_DS_MONHOC_MO FOREIGN KEY (MaMo) references DS_MONHOC_MO(MaMo)
alter table CT_PHIEU_DKHP ADD CONSTRAINT FK_SoPhieuDKHP_PHIEU_DKHP FOREIGN KEY (SoPhieuDKHP) REFERENCES PHIEU_DKHP(SoPhieuDKHP)
alter table PHIEUTHU add constraint FK_PHIEUTHU_PHIEUDKHP foreign key (SoPhieuDKHP) references PHIEU_DKHP(SoPhieuDKHP)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__HKNH foreign key (MaHKNH) references HKNH(MaHKNH)
alter table DSSV_CHUAHOANTHANH_HP add constraint FK_DSSV_CHUAHOANTHANH_HP__SINHVIEN foreign key (MaSV) references SINHVIEN(MaSV)
alter table NGUOIDUNG add constraint FK_NGUOIDUNG_NHOMNGUOIDUNG foreign key (MaNhom) references NHOMNGUOIDUNG(MaNhom)

-- TẠO CÁC RÀNG BUỘC CHECK
alter table SINHVIEN add constraint CHECK_GIOITINH check (GioiTinh in (N'Nam', N'Nữ'))
alter table DOITUONG add constraint CHECK_TILE check (TiLeGiamHocPhi >= 0)
alter table LOAIMONHOC add constraint CHECK_HESOCHIA check (HeSoChia > 0)
alter table LOAIMONHOC add constraint CHECK_SOTIENMOTINCHI check (SoTienMotTinChi > 0)
alter table MONHOC add constraint CHECK_SOTIET check (SoTiet > 0)
alter table MONHOC add constraint CHECK_SOTINCHI check (SoTinChi > 0)
alter table PHIEU_DKHP add constraint CHECK_TONGTIENDANGKY check (TongTienDangKy >= 0)
alter table PHIEU_DKHP add constraint CHECK_TONGTIENPHAIDONG check (TongTienPhaiDong >= 0)
alter table PHIEU_DKHP add constraint CHECK_TONGTIENDADONG check (TongTienDaDong >= 0)
alter table PHIEU_DKHP add constraint CHECK_SOTIENCONLAI check (SoTienConLai >= 0)
alter table DSSV_CHUAHOANTHANH_HP add constraint CHECK_TIENHOCPHI check (SoTienConLai > 0)
GO

--TẠO CÁC TRIGGERS	
--TRIGGER ON SINHVIEN
--Tự động thêm tài khoản sinh viên vào bảng người dùng khi thêm mới sinh viên
CREATE TRIGGER TRG_SINHVIEN_NGUOIDUNG
ON SINHVIEN
FOR INSERT
AS
BEGIN
	DECLARE @TenDangNhap char(6)

	SELECT @TenDangNhap = MaSV 
	FROM inserted

	INSERT INTO NGUOIDUNG VALUES (@TenDangNhap, @TenDangNhap, '3')
END
GO

--Tự động xóa tài khoản sinh viên ở bảng người dùng đăng nhập khi xóa sinh viên ở bảng sinh viên
CREATE TRIGGER SINHVIEN_NGUOIDUNG2
ON SINHVIEN
FOR DELETE
AS
BEGIN
	DECLARE @TenDangNhap char(6)

	SELECT @TenDangNhap = MaSV FROM deleted

	delete from NGUOIDUNG where TenDangNhap = @TenDangNhap
END
GO

--TRIGGER ON MONHOC
-- Trigger tính số tín chỉ của MONHOC
CREATE TRIGGER TG_MH_STC
ON MONHOC
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SoTiet INT, @HeSoChia INT, @MaMonHoc char(7)
	SELECT @SoTiet = SoTiet, @HeSoChia = HeSoChia, @MaMonHoc = MaMonHoc FROM INSERTED, LOAIMONHOC WHERE INSERTED.MaLoaiMon = LOAIMONHOC.MaLoaiMon
	UPDATE MONHOC SET SoTinChi = @SoTiet/@HeSoChia WHERE MaMonHoc = @MaMonHoc
END
GO 

--TRIGGER ON PHIEU_DKHP
--Sinh viên chỉ có thể DKMH 1 lần/1 kỳ học.
CREATE TRIGGER TRG_DKMH_1LAN1KY 
ON PHIEU_DKHP
FOR INSERT, UPDATE 
AS
BEGIN 
	IF (
		SELECT COUNT(*) 
		FROM PHIEU_DKHP p, inserted 
		WHERE p.MaSV = inserted.MaSV and p.MaHKNH = inserted.MaHKNH
	) >= 2
	BEGIN
		PRINT N'Sinh viên chỉ có thể đăng ký môn học tối đa 1 lần/1 học kỳ'
		ROLLBACK TRANSACTION
	END
END
GO

--Trigger tính lại tổng tiền đăng ký và phải đóng, số tiền còn lại trên PHIEU_DKHP khi thêm/sửa
 CREATE TRIGGER TG_PDKHP_TTDK_TTPD 
ON PHIEU_DKHP
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SoTienMotTinChi_LT money, @SoTienMotTinChi_TH money, @TongTCLT INT, @TongTCTH INT, @SoPhieuDKHP INT, @TiLeGiamHocPhi INT
	SELECT @SoTienMotTinChi_LT = SoTienMotTinChi FROM LOAIMONHOC WHERE MaLoaiMon = 'LT' 
	SELECT @SoTienMotTinChi_TH = SoTienMotTinChi FROM LOAIMONHOC WHERE MaLoaiMon = 'TH'
	SELECT @TongTCLT = TongTCLT, @TongTCTH = TongTCTH, @SoPhieuDKHP = SoPhieuDKHP FROM INSERTED
	SELECT @TiLeGiamHocPhi = TiLeGiamHocPhi FROM DOITUONG, INSERTED, SINHVIEN 
			WHERE DOITUONG.MaDoiTuong = SINHVIEN.MaDoiTuong AND SINHVIEN.MaSV = INSERTED.MaSV AND SoPhieuDKHP = @SoPhieuDKHP

	UPDATE PHIEU_DKHP SET TongTienDangKy = @TongTCLT*@SoTienMotTinChi_LT + @TongTCTH*@SoTienMotTinChi_TH WHERE SoPhieuDKHP = @SoPhieuDKHP
	UPDATE PHIEU_DKHP SET TongTienPhaiDong = TongTienDangKy - (TongTienDangKy*@TiLeGiamHocPhi/100) WHERE SoPhieuDKHP = @SoPhieuDKHP
	UPDATE PHIEU_DKHP SET SoTienConLai = TongTienPhaiDong - TongTienDaDong WHERE SoPhieuDKHP = @SoPhieuDKHP
END
GO

--TRIGGER ON CT_PHIEU_DKHP
--Trigger tính số tín chỉ môn học sau khi thêm CT_PHIEU_DKHP (Đăng ký môn học mới)
CREATE TRIGGER TRIG_TONGTINCHI 
ON CT_PHIEU_DKHP
FOR INSERT
AS
BEGIN
	DECLARE @SOPHIEU INT, @MAMO CHAR(11), @SOTINCHILT INT, @SOTINCHITH INT
	--LẤY THÔNG TIN CỦA CT_PHIEU_DKHP MỚI THÊM VÀO HOẶC MỚI UPDATE
	SELECT @SOPHIEU = SoPhieuDKHP, @MAMO = MaMo FROM inserted
	--TÌM SỐ TÍN CHỈ CỦA MÔN HỌC MỚI THÊM
	SET @SOTINCHILT = 0
	SET @SOTINCHITH = 0
	SELECT @SOTINCHILT = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMo = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'LT' AND B.MaMo = @MAMO
	SELECT @SOTINCHITH = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMo = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'TH' AND B.MaMo = @MAMO
	--Tiến hành update lại số tín chỉ
	UPDATE PHIEU_DKHP SET TongTCLT = TongTCLT + @SOTINCHILT WHERE SoPhieuDKHP = @SOPHIEU 
	UPDATE PHIEU_DKHP SET TongTCTH = TongTCTH + @SOTINCHITH WHERE SoPhieuDKHP = @SOPHIEU 
END
GO

--Trigger tính số tín chỉ môn học sau khi xóa CT_PHIEU_DKHP (Xóa đăng ký môn học)
CREATE TRIGGER TRIG_TONGTINCHI2 
ON CT_PHIEU_DKHP
FOR DELETE
AS
BEGIN
	DECLARE @SOPHIEU INT, @MAMO CHAR(11), @SOTINCHI INT, @MALOAIMON CHAR(2), @TONGTCCU INT
	--LẤY THÔNG TIN CỦA CT_PHIEU_DKHP MỚI XÓA
	SET @SOPHIEU = (SELECT SoPhieuDKHP FROM deleted)
	SET @MAMO = (SELECT MaMo FROM deleted)
	--TÌM SỐ TÍN CHỈ CỦA MÔN HỌC MỚI XÓA
	set @MALOAIMON = (select b.MaLoaiMon from DS_MONHOC_MO a join MONHOC b on a.MaMonHoc = b.MaMonHoc where a.MaMo = @MAMO)
	SET @SOTINCHI = (SELECT SoTinChi from DS_MONHOC_MO a join MONHOC b on a.MaMonHoc = b.MaMonHoc where a.MaMo = @MAMO)

	IF(@MALOAIMON = 'LT')
	BEGIN
		SET @TONGTCCU = (SELECT TongTCLT from PHIEU_DKHP where SoPhieuDKHP = @SOPHIEU)
		IF(@TONGTCCU = @SOTINCHI)
		BEGIN
			UPDATE PHIEU_DKHP SET TongTCLT = 0 WHERE SoPhieuDKHP = @SOPHIEU
		END
		ELSE UPDATE PHIEU_DKHP SET TongTCLT = TongTCLT - @SOTINCHI WHERE SoPhieuDKHP = @SOPHIEU
	END
	ELSE
	BEGIN
		SET @TONGTCCU = (SELECT TongTCTH from PHIEU_DKHP where SoPhieuDKHP = @SOPHIEU)
		IF(@TONGTCCU = @SOTINCHI)
		BEGIN
			UPDATE PHIEU_DKHP SET TongTCTH = 0 WHERE SoPhieuDKHP = @SOPHIEU
		END
		ELSE UPDATE PHIEU_DKHP SET TongTCTH = TongTCTH - @SOTINCHI WHERE SoPhieuDKHP = @SOPHIEU
	END
END
GO


--TRIGGER ON PHIEUTHU
--Ngày lập phiếu thu phải <= hạn đóng học phí của đợt đăng ký học phần đó.
CREATE TRIGGER PhieuThu_NgayLap
ON PHIEUTHU
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SoPhieu int, @HanDong smalldatetime, @NgayLap smalldatetime
	SELECT @SoPhieu = SoPhieuDKHP, @NgayLap = NgayLap FROM inserted;

	SELECT @HanDong = HKNH.HanDongHocPhi 
	FROM PHIEU_DKHP JOIN HKNH 
	ON PHIEU_DKHP.MaHKNH = HKNH.MaHKNH 
	WHERE PHIEU_DKHP.SoPhieuDKHP = @SoPhieu
	
	IF(@NgayLap>@HanDong)
	BEGIN
		PRINT N'Không thể lập phiếu thu vì đã quá hạn đóng học phí.'
		ROLLBACK TRANSACTION
	END
END
GO

--Trigger tự động cập nhật số tiền còn lại của bảng PHIEU_DKHP khi sinh viên nộp tiền (Lập phiếu thu học phí)
CREATE TRIGGER PHIEUTHU_INSERT_PHIEU_DKHP_SOTIENCONLAI
ON PHIEUTHU
FOR INSERT
AS
BEGIN
	DECLARE @SoPhieu int , @SoTienThuDuoc money
	SELECT @SoPhieu = SoPhieuDKHP, @SoTienThuDuoc = SoTienThu FROM inserted;
	IF (@SoTienThuDuoc > (SELECT SoTienConLai FROM PHIEU_DKHP WHERE SoPhieuDKHP = @SoPhieu) OR @SoTienThuDuoc <= 0)
	BEGIN
		PRINT N'Không thể lập phiếu thu vì số tiền thu lớn hơn số tiền còn lại hoặc số tiền thu vừa nhập vào đã <= 0'
		ROLLBACK TRANSACTION
	END
	ELSE
	BEGIN
		UPDATE PHIEU_DKHP
		SET  TongTienDaDong += @SoTienThuDuoc
		WHERE PHIEU_DKHP.SoPhieuDKHP = @SoPhieu
	END
END
GO

--Trigger tự động cập nhật số tiền còn lại của bảng PHIEU_DKHP khi sửa phiếu thu học phí. (Sửa phiếu thu học phí)
CREATE TRIGGER PHIEUTHU_UPDATE_PHIEU_DKHP_SOTIENCONLAI
ON PHIEUTHU
FOR UPDATE
AS
BEGIN
	DECLARE @SoPhieu int, @SoTienMoi money,@SoTienCu money
	SELECT @SoPhieu = SoPhieuDKHP, @SoTienMoi = inserted.SoTienThu FROM inserted
	SELECT @SoTienCu=deleted.SoTienThu FROM deleted WHERE deleted.SoPhieuDKHP=@SoPhieu
	IF (@SoTienMoi > (@SoTienCu+(SELECT SoTienConLai FROM PHIEU_DKHP WHERE SoPhieuDKHP = @SoPhieu)) OR @SoTienMoi< 0)
	BEGIN
		PRINT N'Không thể cập nhật phiếu thu vì số tiền thu mới hơn số tiền cần phải đóng hoặc số tiền thu mới < 0'
		ROLLBACK TRANSACTION
	END
	ELSE
	BEGIN
		UPDATE PHIEU_DKHP
		SET TongTienDaDong += (@SoTienMoi-@SoTienCu)
		WHERE PHIEU_DKHP.SoPhieuDKHP = @SoPhieu
	END
END
GO

--Trigger tự động cập nhật số tiền còn lại của bảng PHIEU_DKHP khi xóa phiếu thu học phí. (Xóa phiếu thu học phí)
CREATE TRIGGER PHIEUTHU_DELETE
ON PHIEUTHU
FOR DELETE
AS
BEGIN
	DECLARE @SoPhieu int, @SoTienThuDuoc money
	SELECT @SoPhieu = SoPhieuDKHP, @SoTienThuDuoc = SoTienthu 
	FROM DELETED
	
	UPDATE PHIEU_DKHP
	SET TongTienDaDong = TongTienDaDong - @SoTienThuDuoc
	WHERE PHIEU_DKHP.SoPhieuDKHP = @SoPhieu
END
