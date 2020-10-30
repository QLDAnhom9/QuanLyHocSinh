use [master]
go
-- drop database QuanLyTruongHoc
create database QuanLyTruongHoc
go
use QuanLyTruongHoc

go

create table HocKy(
	maHK  int identity(1, 1) primary key,
	tenHK nvarchar(100)
)

insert into HocKy values('Ky 1')
insert into HocKy values('Ky 2')

create table GiaoVien(
	maGV int identity(1,1) primary key,
	taiKhoan nvarchar(100),
	matKhau nvarchar(100),
	ten nvarchar(100),
	gioiTinh nvarchar(10),
	ngaySinh datetime,
	diaChi nvarchar(100),
	sdt nvarchar(20),
	bangCap nvarchar(100),
	tgBatDau datetime,
	tgKetThuc datetime,
	trangThai bit
)

create table Lop(
	maLop int identity(1, 1) primary key,
	tenLop nvarchar(100),
	maGVCN int,
	nam datetime,
	trangThai bit,
	constraint PK_Lop_GiaoVien foreign key(maGVCN) references GiaoVien(maGV) on update cascade on delete cascade
)

create table HocSinh(
	maHS int identity(202000,1) primary key,
	taiKhoan nvarchar(100),
	matKhau nvarchar(100),
	ten nvarchar(100),
	gioiTinh nvarchar(10),
	ngaySinh datetime,
	diaChi nvarchar(100),
	sdt nvarchar(20),
	tgBatDau datetime,
	tgKetThuc datetime,
	trangThai bit,
	maLop int,
	constraint PK_Lop_HocSinh foreign key(maLop) references Lop(maLop) on update cascade on delete cascade
)

create table Mon(
	maMon int identity(1,1) primary key,
	tenMon nvarchar(100),
	gvDay int,
	maLop int,
	trangThai bit,
	maHK int,
	constraint PK_Mon_GiaoVien foreign key(gvDay) references GiaoVien(maGV) on update cascade on delete cascade,
	constraint PK_Mon_Lop foreign key(maLop) references Lop(maLop),
	constraint PK_Mon_HocKy foreign key(maHK) references HocKy(maHK) on update cascade on delete cascade
)

create table Diem(
	maDiem int identity(1,1) primary key,
	maHS int,
	maMon int,
	diemGiuaKy float,
	diemCuoiKy float,
	tgNhap datetime,
	tgXoa datetime,
	danhGia nvarchar(100),
	trangThai bit,
	constraint PK_Diem_Mon foreign key(maMon) references Mon(maMon) on update cascade on delete cascade,
	constraint PK_Diem_HocSinh foreign key(maHS) references HocSinh(maHS)
)

GO
insert into GiaoVien values('admin', 'admin123', 'admin', '', '', '', '', '', '', '', 0)
-- 5 bản ghi giáo viên
INSERT INTO [dbo].[GIAOVIEN] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Hà Thị Hương', N'Nữ', '1/1/1988')
INSERT INTO [dbo].[GIAOVIEN] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Nguyễn Thị Trà', N'Nữ' , '2/2/1989')
INSERT INTO [dbo].[GIAOVIEN] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Uông Văn Toàn', N'Nam' , '3/3/1979')
INSERT INTO [dbo].[GIAOVIEN] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Trần Thị Hiền', N'Nữ' , '4/4/1981')
INSERT INTO [dbo].[GIAOVIEN] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Mai Như Tuyết', N'Nữ' , '5/5/1983')
-- 5 bản ghi học sinh
INSERT INTO [dbo].[HOCSINH] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Vũ Linh Nhi', N'Nữ' , '1/1/2011')
INSERT INTO [dbo].[HOCSINH] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Trần Tuấn Sơn', N'Nam' , '2/2/2011')
INSERT INTO [dbo].[HOCSINH] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Hoàng Văn Thắng', N'Nam' , '3/3/2013')
INSERT INTO [dbo].[HOCSINH] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Phan Thị Hiền', N'Nữ' , '4/4/2012')
INSERT INTO [dbo].[HOCSINH] ([ten], [gioiTinh], [ngaySinh] ) VALUES(N'Như Thị Tuyết Mai', N'Nữ' , '5/5/2011')
-- bản ghi lớp
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'1A')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'1B')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'1C')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'2A')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'2B')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'2C')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'3A')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'3B')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'3C')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'4A')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'4B')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'4C')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'5A')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'5B')
INSERT INTO [dbo].[LOP] ([tenLop]) VALUES(N'5C')
-- bản ghi môn học
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Tiếng Việt')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Toán')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Tiếng Anh')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Âm nhạc')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Thể dục')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Đạo đức')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Mỹ thuật')
INSERT INTO [dbo].[MON] ([tenMon]) VALUES(N'Tự nhiên-Xã hội')