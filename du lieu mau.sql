
--BỘ DỮ LIỆU MẪU CHO CÁC TABLES

-- du lieu table SINHVIEN
insert into SINHVIEN values ('191512', N'Trương Quốc Vinh', '11/09/2001', N'Nam', 'KTPM', 'DT01', '4901')
insert into SINHVIEN values ('181108', N'Lê Thanh Bình', '24/07/2000', N'Nam', 'CNTT', 'DT02', '3906')
insert into SINHVIEN values ('191634', N'Lê Thị Hạnh', '09/02/2001', N'Nữ', 'KHDL', 'DT04', '4105')
insert into SINHVIEN values ('192447', N'Trương Thảo Mai', '01/08/2001', N'Nữ', 'HTTT', 'DT06', '5004')
insert into SINHVIEN values ('192566', N'Nguyễn Thị Nguyệt', '24/05/2001', N'Nữ', 'MMTT', 'DT01', '0218')
insert into SINHVIEN values ('200021', N'Trần Thế Gia Bảo', '19/11/2002', N'Nam', 'KHMT', 'DT06', '1107')
insert into SINHVIEN values ('171499', N'Trần Bích Vân', '01/10/1999', N'Nữ', 'KHMT', 'DT06', '6107')
insert into SINHVIEN values ('182016', N'Cao Thị Thanh', '29/03/2000', N'Nữ', 'TMDT', 'DT01', '4008')
insert into SINHVIEN values ('200021', N'Hứa Vĩ Văn', '25/06/2002', N'Nam', 'HTTT', 'DT05', '3706')
insert into SINHVIEN values ('192207', N'Hoàng Bình An', '31/12/2001', N'Nam', 'CNTT', 'DT03', '5503')
insert into SINHVIEN values ('191537', N'Chung Gia Khánh', '04/04/2001', N'Nam', 'KHDL', 'DT02', '0315')
insert into SINHVIEN values ('201011', N'Lê Ngọc Minh Khôi', '07/02/2002', N'Nam', 'ATTT', 'DT06', '0106')

-- du lieu table TINH
insert into TINH values ('0001', N'Thành phố Hà Nội')
insert into	TINH values ('0002', N'Thành phố Hồ Chí Minh')
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
insert into HUYEN values ('1107', N'Đồng Hỷ', '0011', 0)
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
insert into MONHOC values ('IT001', N'Nhập môn lập trình', 'LT', '45', '3')
insert into MONHOC values ('IT002', N'Lập trình hướng đối tượng', 'LT', '45', '3')
insert into MONHOC values ('IT003', N'Cấu trúc dữ liệu và giải thuật', 'LT', '45', '3')
insert into MONHOC values ('IT004', N'Cơ sở dữ liệu', 'LT', '45', '3')
insert into MONHOC values ('IT005', N'Nhập môn mạng máy tính', 'LT', '45', '3')
insert into MONHOC values ('IT012', N'Tổ chức Cấu trúc máy tính II', 'LT', '45', '4')
insert into MONHOC values ('MA003', N'Xác suất thống kê', 'LT', '45', '3')
insert into MONHOC values ('MA004', N'Cấu trúc rời rạc', 'LT', '60', '4')
insert into MONHOC values ('SS004', N'Kỹ năng nghề nghiệp', 'LT', '30', '2')
insert into MONHOC values ('SS007', N'Triết học Mác-Lênin', 'LT', '45', '3')
insert into MONHOC values ('SE104', N'Nhập môn công nghệ phần mềm', 'LT', '60', '4')

-- du lieu table LOAIMONHOC
insert into LOAIMONHOC values ('LT', N'Lý thuyết', 15, 27000)
insert into LOAIMONHOC values ('TH', N'Thực hành', 30, 37000)

-- du lieu table CHUONGTRINHHOC
insert into CHUONGTRINHHOC values ('CNTT', N'IT005', 1, N'Nhóm ngành - Bắt buộc')
insert into CHUONGTRINHHOC values ('KTPM', N'SE104', 1, N'Cơ sở ngành - Không bắt buộc')

-- du lieu table HKNH
insert into HKNH values (11920, 1, 2019, 2020, '21/12/2019')
insert into HKNH values (21920, 2, 2019, 2020, '20/06/2020')
insert into HKNH values (12021, 1, 2020, 2021, '30/12/2020')
insert into HKNH values (22021, 2, 2020, 2021, '26/06/2021')
insert into HKNH values (12122, 1, 2021, 2022, '29/12/2021')
insert into HKNH values (22122, 2, 2021, 2022, '02/07/2022')

-- du lieu table DS_MONHOC_MO
insert into DS_MONHOC_MO values ('IT003.L27', 21920, 'IT003')
insert into DS_MONHOC_MO values ('SS004.L20', 21920, 'SS004')
insert into DS_MONHOC_MO values ('IT004.L22', 12021, 'IT004')
insert into DS_MONHOC_MO values ('SE104.L22', 22021, 'SE104')

-- du lieu table PHIEU_DKHP
insert into PHIEU_DKHP values (1, '191512', '25/12/2019', 21920, 15, 4, 553000, 0, 0, 0)
insert into PHIEU_DKHP values (2, '201011', '01/07/2020', 12021, 20, 4, 688000, 688000, 0, 688000)

-- du lieu table CT_PHIEU_DKHP
insert into CT_PHIEU_DKHP values (1, 'IT003.L27', null)
insert into CT_PHIEU_DKHP values (1, 'SS004.L20', null)

-- du lieu table PHIEU_THU
insert into PHIEU_THU values (1, 1, '25/12/2019', 0)
insert into PHIEU_THU values (2, 2, '01/07/2020', 0)

-- du lieu table DSSV_CHUAHOANTHANH_HP
insert into DSSV_CHUAHOANTHANH_HP values (21920, '191512', 553000)
insert into DSSV_CHUAHOANTHANH_HP values (12021, '201011', 688000)

--PHANQUYEN
-- du lieu mau table CHUCNANG
insert into CHUCNANG values ('CN01', N'Chức năng 1', 'Login')
insert into CHUCNANG values ('CN02', N'Chức năng 2', 'PhanQuyen')
insert into CHUCNANG values ('CN03', N'Chức năng 3', 'XemDanhSachSinhVien')
insert into CHUCNANG values ('CN04', N'Chức năng 4', 'DangKyMonHoc')

-- du lieu table NHOMNGUOIDUNG
insert into NHOMNGUOIDUNG values ('Admin', N'Admin')
insert into NHOMNGUOIDUNG values ('PDT', N'Phòng đào tạo')
insert into NHOMNGUOIDUNG values ('SV', N'Sinh viên')

-- du lieu table NGUOIDUNG
insert into NGUOIDUNG values ('admin01', 'admin01', 'Admin')
insert into NGUOIDUNG values ('pdt01', 'pdt01', 'PDT')
insert into NGUOIDUNG values ('191512', '191512', 'SV')
insert into NGUOIDUNG values ('180216', '180216', 'SV')
insert into NGUOIDUNG values ('200021', '200021', 'SV')

-- du lieu table NGUOIDUNG
insert into PHANQUYEN values('Admin', 'CN01')
insert into PHANQUYEN values('Admin', 'CN02')
insert into PHANQUYEN values('Admin', 'CN03')
insert into PHANQUYEN values('PDT', 'CN01')
insert into PHANQUYEN values('PDT', 'CN03')
insert into PHANQUYEN values('SV', 'CN04')
