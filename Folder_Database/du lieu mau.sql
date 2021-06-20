﻿
--BỘ DỮ LIỆU MẪU CHO CÁC TABLES
use CNPM_db
set DATEFORMAT DMY
-- du lieu table SINHVIEN
insert into SINHVIEN values ('191512', N'Trương Quốc Vinh', '11/09/2001', N'Nam', 'KTPM', 'DT01', '4901')
insert into SINHVIEN values ('181108', N'Lê Thanh Bình', '24/07/2000', N'Nam', 'CNTT', 'DT02', '3906')
insert into SINHVIEN values ('191634', N'Lê Thị Hạnh', '09/02/2001', N'Nữ', 'KHDL', 'DT04', '4105')
insert into SINHVIEN values ('192447', N'Trương Thảo Mai', '01/08/2001', N'Nữ', 'HTTT', 'DT06', '5004')
insert into SINHVIEN values ('192566', N'Nguyễn Thị Nguyệt', '24/05/2001', N'Nữ', 'MMTT', 'DT01', '0218')
insert into SINHVIEN values ('200121', N'Trần Thế Gia Bảo', '19/11/2002', N'Nam', 'KHMT', 'DT06', '1011')
insert into SINHVIEN values ('171499', N'Trần Bích Vân', '01/10/1999', N'Nữ', 'KHMT', 'DT06', '6107')
insert into SINHVIEN values ('182016', N'Cao Thị Thanh', '29/03/2000', N'Nữ', 'TMDT', 'DT01', '4008')
insert into SINHVIEN values ('200021', N'Hứa Vĩ Văn', '25/06/2002', N'Nam', 'HTTT', 'DT05', '3706')
insert into SINHVIEN values ('192207', N'Hoàng Bình An', '31/12/2001', N'Nam', 'CNTT', 'DT03', '5503')
insert into SINHVIEN values ('191537', N'Chung Gia Khánh', '04/04/2001', N'Nam', 'KHDL', 'DT02', '0315')
insert into SINHVIEN values ('201011', N'Lê Ngọc Minh Khôi', '07/02/2002', N'Nam', 'ATTT', 'DT06', '0106')


-- du lieu table TINH
insert into TINH values ('0001', N'Thành phố Hà Nội')
insert into TINH values ('0002', N'Thành phố Hồ Chí Minh')
insert into TINH values ('0003', N'Thành phố Hải Phòng')
insert into TINH values ('0004', N'Thành phố Đà Nẵng')
insert into TINH values ('0005', N'Hà Giang')
insert into TINH values ('0006', N'Cao Bằng')
insert into TINH values ('0007', N'Lai Châu')
insert into TINH values ('0008', N'Lào Cai')
insert into TINH values ('0009', N'Tuyên Quang')
insert into TINH values ('0010', N'Lạng Sơn')
insert into TINH values ('0012', N'Thái Nguyên')
insert into TINH values ('0021', N'Hải Dương')
insert into TINH values ('0026', N'Thái Bình')
insert into TINH values ('0028', N'Thanh Hóa')
insert into TINH values ('0034', N'Quảng Nam')
insert into TINH values ('0037', N'Bình Định')
insert into TINH values ('0038', N'Gia Lai')
insert into TINH values ('0039', N'Phú Yên')
insert into TINH values ('0040', N'Đắk Lắk')
insert into TINH values ('0041', N'Khánh Hòa')
insert into TINH values ('0044', N'Bình Dương')
insert into TINH values ('0046', N'Tây Ninh')
insert into TINH values ('0048', N'Đồng Nai')
insert into TINH values ('0049', N'Long An')
insert into TINH values ('0050', N'Đồng Tháp')
insert into TINH values ('0052', N'Bà Rịa - Vũng Tàu')
insert into TINH values ('0053', N'Tiền Giang')
insert into TINH values ('0055', N'Cần Thơ')
insert into TINH values ('0056', N'Bến Tre')
insert into TINH values ('0061', N'Cà Mau')

-- du lieu table HUYEN
insert into HUYEN values ('0106', N'Quận Cầu Giấy', '0001', 0)
insert into HUYEN values ('0124', N'Thanh Oai', '0001', 0)
insert into HUYEN values ('0201', N'Quận 1', '0002', 0)
insert into HUYEN values ('0218', N'Quận Thủ Đức', '0002', 0)
insert into HUYEN values ('0303', N'Quận Ngô Quyền', '0003', 0)
insert into HUYEN values ('0315', N'Dương Kinh', '0003', 0)
insert into HUYEN values ('0403', N'Quận Sơn Trà', '0004', 0)
insert into HUYEN values ('0504', N'Yên Minh', '0005', 1)
insert into HUYEN values ('0610', N'Thạch An', '0006', 1)
insert into HUYEN values ('0706', N'Than Uyên', '0007', 1)
insert into HUYEN values ('0804', N'Bắc Hà', '0008', 0)
insert into HUYEN values ('0907', N'Sơn Dương', '0009', 0)
insert into HUYEN values ('1011', N'Hữu Lũng', '0010', 0)
insert into HUYEN values ('2103', N'Nam Sách', '0021', 1)
insert into HUYEN values ('2602', N'Quỳnh Phụ', '0026', 1)
insert into HUYEN values ('2604', N'Đông Hưng', '0026', 1)
insert into HUYEN values ('2818', N'Triệu Sơn', '0028', 1)
insert into HUYEN values ('3402', N'Hội An', '0034', 1)
insert into HUYEN values ('3701', N'Quy Nhơn', '0037', 0)
insert into HUYEN values ('3705', N'Phù Mỹ', '0037', 1)
insert into HUYEN values ('3706', N'Phù Cát', '0037', 0)
insert into HUYEN values ('3801', N'TP. PleiKu', '0038', 0)
insert into HUYEN values ('3901', N'Tuy Hòa', '0039', 0)
insert into HUYEN values ('3903', N'Sông Cầu', '0039', 0)
insert into HUYEN values ('3906', N'Sông Hinh', '0039', 1)
insert into HUYEN values ('4001', N'TP. Buôn Mê Thuột', '0040', 0)
insert into HUYEN values ('4008', N'Eakar', '0040', 1)
insert into HUYEN values ('4101', N'TP. Nha Trang', '0041', 0)
insert into HUYEN values ('4105', N'Khánh Vĩnh', '0041', 1)
insert into HUYEN values ('4106', N'TP. Cam Ranh', '0041', 0)
insert into HUYEN values ('4901', N'TP. Tân An', '0049', 0)
insert into HUYEN values ('4909', N'Thủ Thừa', '0049', 0)
insert into HUYEN values ('4915', N'TX Kiến Tường', '0049', 1)
insert into HUYEN values ('5002', N'Lai Vung', '0050', 1)
insert into HUYEN values ('5004', N'TP. Sa Đéc', '0050', 0)
insert into HUYEN values ('5006', N'TP. Cao Lãnh', '0050', 0)
insert into HUYEN values ('5205', N'Côn Đảo', '0052', 1)
insert into HUYEN values ('5302', N'TX Gò Công', '0053', 0)
insert into HUYEN values ('5503', N'Quận Cái Răng', '0055', 0)
insert into HUYEN values ('5603', N'TP. Bến Tre', '0056', 0)
insert into HUYEN values ('6107', N'Ngọc Hiển', '0061', 1)

-- du lieu table DOITUONG
insert into DOITUONG values ('DT01',N'Con thương binh', 100)
insert into DOITUONG values ('DT02',N'Dân tộc thiểu số', 30)
insert into DOITUONG values ('DT03',N'Khuyết tật', 70)
insert into DOITUONG values ('DT04',N'Vùng sâu vùng xa', 50)
insert into DOITUONG values ('DT05',N'Hộ nghèo', 50)
insert into DOITUONG values ('DT06',N'Bình thường', 0)

-- du lieu table KHOA
insert into KHOA values ('CNPM', N'Công nghệ phần mềm')
insert into KHOA values ('KTTT', N'Khoa học & Kỹ thuật thông tin')
insert into KHOA values ('KTMT', N'Kỹ thuật máy tính')
insert into KHOA values ('HTTT', N'Hệ thống thông tin')
insert into KHOA values ('KHMT', N'Khoa học máy tính')
insert into KHOA values ('MMTT', N'Mạng máy tính và Truyền thông')

-- du lieu table NGANH
insert into NGANH values ('KHDL', N'Khoa học dữ liệu', 'KTTT')
insert into NGANH values ('CNTT', N'Công nghệ thông tin', 'KTTT')
insert into NGANH values ('KHMT', N'Khoa học máy tính', 'KHMT')
insert into NGANH values ('KTPM', N'Kỹ thuật phần mềm', 'CNPM')
insert into NGANH values ('KTMT', N'Kỹ thuật máy tính', 'KTMT')
insert into NGANH values ('HTTT', N'Hệ thống thông tin', 'HTTT')
insert into NGANH values ('TMDT', N'Thương mại điện tử', 'HTTT')
insert into NGANH values ('MMTT', N'Mạng máy tính và Truyền thông dữ liệu', 'MMTT')
insert into NGANH values ('ATTT', N'An toàn thông tin', 'MMTT')

--du lieu mau table MONHOC
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT001LT', N'Nhập môn lập trình', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT001TH', N'Nhập môn lập trình', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT002LT', N'Lập trình hướng đối tượng', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT002TH', N'Lập trình hướng đối tượng', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT003LT', N'Cấu trúc dữ liệu và giải thuật', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT003TH', N'Cấu trúc dữ liệu và giải thuật', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT004LT', N'Cơ sở dữ liệu', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT004TH', N'Cơ sở dữ liệu', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT005LT', N'Nhập môn mạng máy tính', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT005TH', N'Nhập môn mạng máy tính', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT012LT', N'Tổ chức Cấu trúc máy tính II', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT012TH', N'Tổ chức Cấu trúc máy tính II', 'TH', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('MA006LT', N'Giải tích', 'LT', '60')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('MA003LT', N'Đại số tuyến tính', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('MA004LT', N'Cấu trúc rời rạc', 'LT', '60')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('MA005LT', N'Xác suất thống kê', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS004LT', N'Kỹ năng nghề nghiệp', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('IT009LT', N'Giới thiệu ngành', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS007LT', N'Triết học Mác-Lênin', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS008LT', N'Kinh tế chính trị Mác-Lênin', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS009LT', N'Chủ nghĩa xã hội khoa học', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS003LT', N'Tư tưởng Hồ Chí Minh', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SS010LT', N'Lịch sử Đảng Cộng Sản Việt Nam', 'LT', '30')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SE104LT', N'Nhập môn công nghệ phần mềm', 'LT', '45')
insert into MONHOC(MaMonHoc, TenMonHoc, MaLoaiMon, SoTiet) values ('SE104TH', N'Nhập môn công nghệ phần mềm', 'TH', '30')

-- du lieu table LOAIMONHOC
insert into LOAIMONHOC values ('LT', N'Lý thuyết', 15, 27000)
insert into LOAIMONHOC values ('TH', N'Thực hành', 30, 37000)

-- du lieu table CHUONGTRINHHOC
insert into CHUONGTRINHHOC values ('CNTT', N'IT005LT', 1, N'Nhóm ngành - Bắt buộc')
insert into CHUONGTRINHHOC values ('KTPM', N'SE104LT', 1, N'Cơ sở ngành - Không bắt buộc')

-- du lieu table HKNH
insert into HKNH values (11920, 1, 2019, 2020, '21/12/2019')
insert into HKNH values (21920, 2, 2019, 2020, '20/06/2020')
insert into HKNH values (12021, 1, 2020, 2021, '30/12/2020')
insert into HKNH values (22021, 2, 2020, 2021, '26/06/2021')
insert into HKNH values (12122, 1, 2021, 2022, '29/12/2021')
insert into HKNH values (22122, 2, 2021, 2022, '02/07/2022')

-- du lieu table DS_MONHOC_MO
insert into DS_MONHOC_MO values ('IT003LT_L27', 21920, 'IT003LT')
insert into DS_MONHOC_MO values ('IT003TH_L27', 21920, 'IT003TH')
insert into DS_MONHOC_MO values ('IT003LT_L28', 21920, 'IT003LT')
insert into DS_MONHOC_MO values ('IT003TH_L28', 21920, 'IT003TH')
insert into DS_MONHOC_MO values ('SS004LT_L20', 21920, 'SS004LT')
insert into DS_MONHOC_MO values ('IT004LT_L22', 12021, 'IT004LT')
insert into DS_MONHOC_MO values ('IT004TH_L22', 12021, 'IT004TH')
insert into DS_MONHOC_MO values ('SE104LT_L22', 22021, 'SE104LT')
insert into DS_MONHOC_MO values ('SE104TH_L22', 22021, 'SE104TH')

-- du lieu table PHIEU_DKHP
insert into PHIEU_DKHP(SoPhieuDKHP, MaSV, NgayLap, MaHKNH) values(1, 181108, '01/07/2020', '12021')
insert into PHIEU_DKHP(SoPhieuDKHP, MaSV, NgayLap, MaHKNH) values(2, 192447, '01/04/2020', '21920')

-- du lieu table CT_PHIEU_DKHP
insert into CT_PHIEU_DKHP values (1, 'IT003LT_L27', null)
insert into CT_PHIEU_DKHP values (1, 'SS004LT_L20', null)
insert into CT_PHIEU_DKHP values (1, 'IT003TH_L27', null)
insert into CT_PHIEU_DKHP values (1, 'IT003LT_L28', null)
insert into CT_PHIEU_DKHP values (2, 'IT004LT_L22', null)
insert into CT_PHIEU_DKHP values (2, 'SE104LT_L22', null)

-- du lieu table PHIEU_THU
insert into PHIEUTHU values (1, 1, '25/12/2019', 10000)
insert into PHIEUTHU values (2, 1, '01/03/2020', 100000)
insert into PHIEUTHU values (3, 2, '31/12/2020', 100000)
insert into PHIEUTHU values (3, 1, '25/12/2019', 10000)

-- du lieu table DSSV_CHUAHOANTHANH_HP


--PHANQUYEN
-- du lieu mau table CHUCNANG
insert into CHUCNANG values ('CN01', N'Chức năng chung', N'Đăng nhập')
insert into CHUCNANG values ('CN02', N'Chức năng Admin', N'Thêm/xóa/sửa nhóm người dùng')
insert into CHUCNANG values ('CN03', N'Chức năng Admin', N'Thêm/xóa/sửa người dùng')
insert into CHUCNANG values ('CN04', N'Chức năng Admin', N'Thêm/xóa/sửa chức năng')
insert into CHUCNANG values ('CN05', N'Chức năng người dùng' , N'Thêm sinh viên')
insert into CHUCNANG values ('CN06', N'Chức năng người dùng' , N'Xóa sinh viên')
insert into CHUCNANG values ('CN07', N'Chức năng người dùng' , N'Sửa sinh viên')
insert into CHUCNANG values ('CN08', N'Chức năng người dùng' , N'Thêm tỉnh')
insert into CHUCNANG values ('CN09', N'Chức năng người dùng' , N'Xóa tỉnh')
insert into CHUCNANG values ('CN10', N'Chức năng người dùng' , N'Sửa tỉnh')
insert into CHUCNANG values ('CN11', N'Chức năng người dùng' , N'Thêm đối tượng')
insert into CHUCNANG values ('CN12', N'Chức năng người dùng' , N'Xóa đối tượng')
insert into CHUCNANG values ('CN13', N'Chức năng người dùng' , N'Sửa đối tượng')
insert into CHUCNANG values ('CN14', N'Chức năng người dùng' , N'Thêm môn học')
insert into CHUCNANG values ('CN15', N'Chức năng người dùng' , N'Xóa môn học')
insert into CHUCNANG values ('CN16', N'Chức năng người dùng' , N'Sửa môn học')
insert into CHUCNANG values ('CN17', N'Chức năng người dùng' , N'Thêm loại môn học')
insert into CHUCNANG values ('CN18', N'Chức năng người dùng' , N'Xóa loại môn học')
insert into CHUCNANG values ('CN19', N'Chức năng người dùng' , N'Sửa loại môn học')
insert into CHUCNANG values ('CN20', N'Chức năng người dùng' , N'Thêm môn học mở')
insert into CHUCNANG values ('CN21', N'Chức năng người dùng' , N'Xóa môn học mở')
insert into CHUCNANG values ('CN22', N'Chức năng người dùng' , N'Sửa môn học mở')
insert into CHUCNANG values ('CN23', N'Chức năng người dùng' , N'Thêm phiếu ĐKHP')
insert into CHUCNANG values ('CN24', N'Chức năng người dùng' , N'Sửa phiếu ĐKHP')
insert into CHUCNANG values ('CN25', N'Chức năng người dùng' , N'Xóa phiếu ĐKHP')
insert into CHUCNANG values ('CN26', N'Chức năng người dùng' , N'Thêm chi tiết phiếu ĐKHP')
insert into CHUCNANG values ('CN27', N'Chức năng người dùng' , N'Xóa chi tiết phiếu ĐKHP')
insert into CHUCNANG values ('CN28', N'Chức năng người dùng' , N'Sửa chi tiết phiếu ĐKHP')
insert into CHUCNANG values ('CN29', N'Chức năng người dùng' , N'Thêm phiếu thu học phí')
insert into CHUCNANG values ('CN30', N'Chức năng người dùng' , N'Sửa phiếu thu học phí')
insert into CHUCNANG values ('CN31', N'Chức năng người dùng' , N'Xóa phiếu thu học phí')
insert into CHUCNANG values ('CN32', N'Chức năng người dùng' , N'Thêm sinh viên nợ học phí')
insert into CHUCNANG values ('CN33', N'Chức năng người dùng' , N'Xóa sinh viên nợ học phí')
insert into CHUCNANG values ('CN34', N'Chức năng người dùng' , N'Sửa sinh viên nợ học phí')
insert into CHUCNANG values ('CN35', N'Chức năng người dùng' , N'Thêm ngành')
insert into CHUCNANG values ('CN36', N'Chức năng người dùng' , N'Xóa ngành')
insert into CHUCNANG values ('CN37', N'Chức năng người dùng' , N'Sửa ngành')
insert into CHUCNANG values ('CN38', N'Chức năng người dùng' , N'Thêm huyện')
insert into CHUCNANG values ('CN39', N'Chức năng người dùng' , N'Xóa huyện')
insert into CHUCNANG values ('CN40', N'Chức năng người dùng' , N'Sửa huyện')
insert into CHUCNANG values ('CN41', N'Chức năng người dùng' , N'Thêm học kỳ - năm học')
insert into CHUCNANG values ('CN42', N'Chức năng người dùng' , N'Xóa học kỳ - năm học')
insert into CHUCNANG values ('CN43', N'Chức năng người dùng' , N'Sửa học kỳ - năm học')
insert into CHUCNANG values ('CN44', N'Chức năng người dùng' , N'Tra cứu phiếu ĐKHP')
insert into CHUCNANG values ('CN45', N'Chức năng người dùng' , N'Thêm chương trình học')
insert into CHUCNANG values ('CN46', N'Chức năng người dùng' , N'Xóa chương trình học')
insert into CHUCNANG values ('CN47', N'Chức năng người dùng' , N'Sửa chương trình học')
insert into CHUCNANG values ('CN48', N'Chức năng Admin', N'Trang quản trị hệ thống')
insert into CHUCNANG values ('CN49', N'Chức năng người dùng' , N'Trang chủ phòng đào tạo')
insert into CHUCNANG values ('CN50', N'Chức năng người dùng' , N'Trang chủ sinh viên')
insert into CHUCNANG values ('CN51', N'Chức năng người dùng' , N'Xem báo cáo nợ học phí')

-- du lieu table NHOMNGUOIDUNG
insert into NHOMNGUOIDUNG values ('1', N'Admin')
insert into NHOMNGUOIDUNG values ('2', N'Phòng đào tạo')
insert into NHOMNGUOIDUNG values ('3', N'Sinh viên')

-- du lieu table NGUOIDUNG
insert into NGUOIDUNG values ('admin01', '1', '1')
insert into NGUOIDUNG values ('pdt01', '1', '2')


-- du lieu table NGUOIDUNG
insert into PHANQUYEN values('1', 'CN01')
insert into PHANQUYEN values('1', 'CN02')
insert into PHANQUYEN values('1', 'CN03')
insert into PHANQUYEN values('1', 'CN04')
insert into PHANQUYEN values('1', 'CN48')

insert into PHANQUYEN values('2', 'CN01')
insert into PHANQUYEN values('2', 'CN05')
insert into PHANQUYEN values('2', 'CN06')
insert into PHANQUYEN values('2', 'CN07')
insert into PHANQUYEN values('2', 'CN08')
insert into PHANQUYEN values('2', 'CN09')
insert into PHANQUYEN values('2', 'CN10')
insert into PHANQUYEN values('2', 'CN11')
insert into PHANQUYEN values('2', 'CN12')
insert into PHANQUYEN values('2', 'CN13')
insert into PHANQUYEN values('2', 'CN14')
insert into PHANQUYEN values('2', 'CN15')
insert into PHANQUYEN values('2', 'CN16')
insert into PHANQUYEN values('2', 'CN17')
insert into PHANQUYEN values('2', 'CN18')
insert into PHANQUYEN values('2', 'CN19')
insert into PHANQUYEN values('2', 'CN20')
insert into PHANQUYEN values('2', 'CN21')
insert into PHANQUYEN values('2', 'CN22')
insert into PHANQUYEN values('2', 'CN23')
insert into PHANQUYEN values('2', 'CN24')
insert into PHANQUYEN values('2', 'CN25')
insert into PHANQUYEN values('2', 'CN26')
insert into PHANQUYEN values('2', 'CN27')
insert into PHANQUYEN values('2', 'CN28')
insert into PHANQUYEN values('2', 'CN29')
insert into PHANQUYEN values('2', 'CN30')
insert into PHANQUYEN values('2', 'CN31')
insert into PHANQUYEN values('2', 'CN32')
insert into PHANQUYEN values('2', 'CN33')
insert into PHANQUYEN values('2', 'CN34')
insert into PHANQUYEN values('2', 'CN35')
insert into PHANQUYEN values('2', 'CN36')
insert into PHANQUYEN values('2', 'CN37')
insert into PHANQUYEN values('2', 'CN38')
insert into PHANQUYEN values('2', 'CN39')
insert into PHANQUYEN values('2', 'CN40')
insert into PHANQUYEN values('2', 'CN41')
insert into PHANQUYEN values('2', 'CN42')
insert into PHANQUYEN values('2', 'CN43')
insert into PHANQUYEN values('2', 'CN44')
insert into PHANQUYEN values('2', 'CN45')
insert into PHANQUYEN values('2', 'CN46')
insert into PHANQUYEN values('2', 'CN47')
insert into PHANQUYEN values('2', 'CN49')
insert into PHANQUYEN values('2', 'CN50')
insert into PHANQUYEN values('2', 'CN51')

insert into PHANQUYEN values('3', 'CN01')


select a.TenDangNhap, a.MaNhom, c.TenNhom, d.TenChucNang,d.TenManHinhDuocLoad
from NGUOIDUNG a, PHANQUYEN b, NHOMNGUOIDUNG c, CHUCNANG d
where a.MaNhom = b.MaNhom and b.MaChucNang = d.MaChucNang and b.MaNhom = c.MaNhom