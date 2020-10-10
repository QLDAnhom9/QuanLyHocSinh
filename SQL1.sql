use master
go
-- drop database QuanLyTruongHoc
create database QuanLyTruongHoc

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

insert into GiaoVien values('admin', 'admin123', 'admin', '', '', '', '', '', '', '', 0)

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


select * from Lop