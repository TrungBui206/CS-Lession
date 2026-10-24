
-- Thêm lớp học
INSERT INTO dbo.LopHoc (MaLop, TenLop)
VALUES
(N'CSE0001', N'Khoa học máy tính 1'),
(N'CSE0002', N'Khoa học máy tính 2'),
(N'CSE0003', N'Khoa học máy tính 3');

-- Thêm sinh viên
INSERT INTO dbo.SinhVien
(
    MaSV, HoTen, MaLop, NgaySinh,
    GioiTinh, Email, TrangThai,
    SoDienThoai, DiaChi, Diem
)
VALUES
(
    N'SV000001', N'Nguyễn Văn An', N'CSE0001',
    '2005-05-15', N'Nam', N'an@gmail.com',
    N'Đang học', '0912345678', N'Hà Nội', 8.5
),
(
    N'SV000002', N'Trần Thị Bình', N'CSE0001',
    '2005-08-20', N'Nữ', N'binh@gmail.com',
    N'Đang học', '0987654321', N'Hải Phòng', 9.0
),
(
    N'SV000003', N'Lê Văn Cường', N'CSE0002',
    '2004-12-10', N'Nam', N'cuong@gmail.com',
    N'Đang học', '0934567890', N'Hải Dương', 7.5
);

-- Kiểm tra dữ liệu
SELECT * FROM dbo.LopHoc;
SELECT * FROM dbo.SinhVien;
