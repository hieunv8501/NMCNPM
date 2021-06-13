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
	HeSoChia int,
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
	TongTCLT int DEFAULT 0,
	TongTCTH int DEFAULT 0,
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

-- TẠO CÁC TABLE CHO CHỨC NĂNG PHÂN QUYỀN

--Table CHUCNANG
CREATE TABLE CHUCNANG
(
	MaChucNang char(10) primary key,
	TenChucNang nvarchar(50),
	TenManHinhDuocLoad nvarchar(20)
)

/*Dữ liệu của Table này do người xây dựng hệ thống nhập đầy đủ cho nó
trước khi triển khai hệ thống cho người dùng cuối sử dụng.
Dữ liệu của các Table còn lại đều có thể được xem/thêm/xoá/sửa bởi
người quản trị hệ thống (người có quyền cao nhất)*/

--Table NHOMNGUOIDUNG
CREATE TABLE NHOMNGUOIDUNG
(
	MaNhom int primary key,
	TenNhom nvarchar(30)
)

--Table PHANQUYEN
CREATE TABLE PHANQUYEN
(
	MaNhom int not null,
	MaChucNang char(10) not null,
	primary key(MaNhom, MaChucNang)
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
alter table PHANQUYEN add constraint FK_PHANQUYEN_NHOMNGUOIDUNG foreign key (MaNhom) references NHOMNGUOIDUNG(MaNhom)
alter table PHANQUYEN add constraint FK_PHANQUYEN_CHUCNANG foreign key (MaChucNang) references CHUCNANG(MaChucNang)
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

--Tự động xóa tài khoản sinh viên ở bảng người dùng khi xóa sinh viên ở bảng sinh viên
CREATE TRIGGER SINHVIEN_NGUOIDUNG2
ON SINHVIEN
FOR DELETE
AS
BEGIN
	DECLARE @TenDangNhap char(6)

	SELECT @TenDangNhap = MaSV FROM deleted

	delete from NGUOIDUNG where TenDangNhap = @TenDangNhap
END

--Trigger khi update đối tượng sẽ thay đổi tiền học phí.
CREATE TRIGGER TG_SV_TTPD 
ON SINHVIEN
FOR UPDATE
AS
BEGIN
	IF(SELECT MaDoiTuong FROM DELETED) <> (SELECT MaDoiTuong FROM INSERTED)
		BEGIN
		DECLARE @SoPhieuDKHP INT, @TiLeGiamHocPhi INT, @MaDoiTuong char(4)
		SELECT @TiLeGiamHocPhi = TiLeGiamHocPhi FROM DOITUONG, INSERTED WHERE DOITUONG.MaDoiTuong = INSERTED.MaDoiTuong 
		DECLARE CUR_SV CURSOR FOR SELECT SoPhieuDKHP FROM PHIEU_DKHP, INSERTED WHERE PHIEU_DKHP.MaSV = INSERTED.MaSV
		OPEN CUR_SV
		FETCH NEXT FROM CUR_SV INTO @SoPhieuDKHP
		WHILE(@@FETCH_STATUS = 0)
		BEGIN
			UPDATE PHIEU_DKHP SET TongTienPhaiDong = TongTienDangKy - TongTienDangKy*@TiLeGiamHocPhi/100 WHERE SoPhieuDKHP = @SoPhieuDKHP
			UPDATE PHIEU_DKHP SET SoTienConLai = TongTienPhaiDong WHERE SoPhieuDKHP = @SoPhieuDKHP
			FETCH NEXT FROM CUR_SV INTO @SoPhieuDKHP
		END
		CLOSE CUR_SV
		DEALLOCATE CUR_SV
	END
END
GO

--TRIGGER ON MONHOC
-- Trigger tính số tín chỉ của MONHOC
GO
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

--TRIGGER ON LOAIMONHOC
-- Trigger tính số tín chỉ của MONHOC khi update LOAIMONHOC
CREATE TRIGGER TG_LMH_STC 
ON LOAIMONHOC
FOR UPDATE
AS
BEGIN
	DECLARE @MaLoaiMon char(2), @HeSoChia int, @MaMonHoc char(7)
	SELECT @MaLoaiMon = MaLoaiMon, @HeSoChia = HeSoChia FROM INSERTED 
	DECLARE CUR_MMH CURSOR FOR SELECT MaMonHoc FROM MONHOC WHERE MaLoaiMon = @MaLoaiMon
	OPEN CUR_MMH
	FETCH NEXT FROM CUR_MMH INTO @MaMonHoc
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		UPDATE MONHOC SET SoTinChi = (SELECT SoTiet FROM MONHOC WHERE MaMonHoc = @MaMonHoc)/@HeSoChia WHERE MaMonHoc = @MaMonHoc
		FETCH NEXT FROM CUR_MMH INTO @MaMonHoc
	END
	CLOSE CUR_MMH
	DEALLOCATE CUR_MMH
END
GO

--TRIGGER ON LOAIMONHOC 
--Trigger khi update tiền một tín chỉ ở loại môn học sẽ thay đổi tiền học phí chung.
CREATE TRIGGER TG_LMH_TTDK_TTPD 
ON LOAIMONHOC
FOR UPDATE
AS
BEGIN
	DECLARE @SoTienMotTinChi_LT money, @SoTienMotTinChi_TH money, @TongTCLT INT, @TongTCTH INT, @SoPhieuDKHP INT, @TiLeGiamHocPhi INT
	DECLARE CUR_PDK CURSOR FOR SELECT SoPhieuDKHP FROM PHIEU_DKHP
	SELECT @SoTienMotTinChi_LT = SoTienMotTinChi FROM INSERTED WHERE MaLoaiMon = 'LT' 
	SELECT @SoTienMotTinChi_TH = SoTienMotTinChi FROM INSERTED WHERE MaLoaiMon = 'TH'
	OPEN CUR_PDK
	FETCH NEXT FROM CUR_PDK INTO @SoPhieuDKHP
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @TongTCLT = TongTCLT, @TongTCTH = TongTCTH FROM PHIEU_DKHP WHERE SoPhieuDKHP = @SoPhieuDKHP
		--Update tổng tiền đăng ký
		UPDATE PHIEU_DKHP SET TongTienDangKy = @TongTCLT*@SoTienMotTinChi_LT + @TongTCTH*@SoTienMotTinChi_TH WHERE SoPhieuDKHP = @SoPhieuDKHP
		SELECT @TiLeGiamHocPhi = TiLeGiamHocPhi FROM DOITUONG, PHIEU_DKHP, SINHVIEN 
				WHERE DOITUONG.MaDoiTuong = SINHVIEN.MaDoiTuong AND SINHVIEN.MaSV = PHIEU_DKHP.MaSV AND SoPhieuDKHP = @SoPhieuDKHP
		--Update tổng tiền phải đóng
		UPDATE PHIEU_DKHP SET TongTienPhaiDong = TongTienDangKy - TongTienDangKy*@TiLeGiamHocPhi/100 WHERE SoPhieuDKHP = @SoPhieuDKHP
		UPDATE PHIEU_DKHP SET SoTienConLai = TongTienPhaiDong WHERE SoPhieuDKHP = @SoPhieuDKHP
		FETCH NEXT FROM CUR_PDK INTO @SoPhieuDKHP
	END
	CLOSE CUR_PDK
	DEALLOCATE CUR_PDK
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

--Tính tổng tiền đăng ký và phải đóng trên PHIEU_DKHP
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
	UPDATE PHIEU_DKHP SET TongTienPhaiDong = TongTienDangKy - TongTienDangKy*@TiLeGiamHocPhi/100 WHERE SoPhieuDKHP = @SoPhieuDKHP
	UPDATE PHIEU_DKHP SET SoTienConLai = TongTienPhaiDong - TongTienDaDong WHERE SoPhieuDKHP = @SoPhieuDKHP
END
GO

--TRIGGER ON CT_PHIEU_DKHP
--Trigger tính số tín chỉ môn học sau khi thêm, sửa 1 vài CT_PHIEU_DKHP////////////////////////////////////////////
CREATE TRIGGER TRIG_TONGTINCHI 
ON CT_PHIEU_DKHP
FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @SOPHIEU INT, @MAMO CHAR(11), @SOTINCHILT INT, @SOTINCHITH INT
	--LẤY THÔNG TIN CỦA CT_PHIEU_DKHP MỚI THÊM VÀO HOẶC MỚI UPDATE
	SELECT @SOPHIEU = SoPhieuDKHP, @MAMO = MaMo FROM inserted
	--TÌM SỐ TÍN CHỈ CỦA MÔN HỌC MỚI THÊM
	SET @SOTINCHILT = 0
	SET @SOTINCHITH = 0
	SELECT @SOTINCHILT = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'LT' AND B.MaMo = @MAMO
	SELECT @SOTINCHITH = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'TH' AND B.MaMo = @MAMO
	--Tiến hành update lại số tín chỉ
	UPDATE PHIEU_DKHP SET TongTCLT = TongTCLT + @SOTINCHILT WHERE SoPhieuDKHP = @SOPHIEU 
	UPDATE PHIEU_DKHP SET TongTCTH = TongTCTH + @SOTINCHITH WHERE SoPhieuDKHP = @SOPHIEU 
END
GO

--Trigger tính số tín chỉ môn học sau khi xóa 1 vài CT_PHIEU_DKHP////////////////////////////////////
alter TRIGGER TRIG_TONGTINCHI2 
ON CT_PHIEU_DKHP
FOR DELETE
AS
BEGIN
	DECLARE @SOPHIEU INT, @MAMO CHAR(11), @SOTINCHILT INT, @SOTINCHITH INT
	--LẤY THÔNG TIN CỦA CT_PHIEU_DKHP MỚI XÓA
	SELECT @SOPHIEU = SoPhieuDKHP, @MAMO = MaMo FROM deleted
	--TÌM SỐ TÍN CHỈ CỦA MÔN HỌC MỚI XÓA
	SELECT @SOTINCHILT = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'LT' AND B.MaMo = @MAMO
	SELECT @SOTINCHITH = SoTinChi from (CT_PHIEU_DKHP A join DS_MONHOC_MO B ON A.MaMO = B.MaMo) JOIN MONHOC C ON B.MaMonHoc = C.MaMonHoc
						WHERE C.MaLoaiMon = 'TH' AND B.MaMo = @MAMO
	--Tiến hành update lại số tín chỉ
	DECLARE @TONGTCLT_CU INT, @TONGTCTH_CU INT
	SELECT @TONGTCLT_CU = TongTCLT from PHIEU_DKHP where SoPhieuDKHP = @SOPHIEU 
	SELECT @TONGTCTH_CU = TongTCTH from PHIEU_DKHP where SoPhieuDKHP = @SOPHIEU
	IF(@TONGTCLT_CU = @SOTINCHILT)
		UPDATE PHIEU_DKHP SET TongTCLT = 0 WHERE SoPhieuDKHP = @SOPHIEU
	ELSE UPDATE PHIEU_DKHP SET TongTCLT = TongTCLT - @SOTINCHILT WHERE SoPhieuDKHP = @SOPHIEU
	IF(@TONGTCTH_CU = @SOTINCHITH)
		UPDATE PHIEU_DKHP SET TongTCTH = 0 WHERE SoPhieuDKHP = @SOPHIEU
	ELSE UPDATE PHIEU_DKHP SET TongTCTH = TongTCTH - @SOTINCHITH WHERE SoPhieuDKHP = @SOPHIEU 
END
GO

--TRIGGER ON DOITUONG 
--Trigger khi update tỉ lệ giảm học phí ở DOITUONG sẽ thay dổi tiền học phí của sinh viên.
CREATE TRIGGER TG_DT_TTPD 
ON DOITUONG
FOR UPDATE
AS
BEGIN
	DECLARE @SoPhieuDKHP INT, @TiLeGiamHocPhi INT, @MaDoiTuong char(4)
	SELECT @MaDoiTuong = MaDoiTuong, @TiLeGiamHocPhi = TiLeGiamHocPhi FROM INSERTED 
	DECLARE CUR_DT CURSOR FOR SELECT SoPhieuDKHP FROM PHIEU_DKHP, SINHVIEN WHERE PHIEU_DKHP.MaSV = SINHVIEN.MaSV AND MaDoiTuong = @MaDoiTuong
	OPEN CUR_DT
	FETCH NEXT FROM CUR_DT INTO @SoPhieuDKHP
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		UPDATE PHIEU_DKHP SET TongTienPhaiDong = TongTienDangKy - TongTienDangKy*@TiLeGiamHocPhi/100 WHERE SoPhieuDKHP = @SoPhieuDKHP
		UPDATE PHIEU_DKHP SET SoTienConLai = TongTienPhaiDong WHERE SoPhieuDKHP = @SoPhieuDKHP
		FETCH NEXT FROM CUR_DT INTO @SoPhieuDKHP
	END
	CLOSE CUR_DT
	DEALLOCATE CUR_DT
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
		SET  TongTienDaDong+= @SoTienThuDuoc
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
GO