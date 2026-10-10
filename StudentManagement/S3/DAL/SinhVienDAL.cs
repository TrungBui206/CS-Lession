
using System;
using System.Collections.Generic;
using System.Linq;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.DAL
{
    public class SinhVienDAL
    {
        // Danh sách dùng chung trong suốt quá trình chạy
        private static readonly List<SinhVien> sinhViens
            = new List<SinhVien>();

        // Chỉ khởi tạo dữ liệu mẫu một lần
        static SinhVienDAL()
        {
            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000001",
                HoTen = "Nguyễn Văn A",
                MaLop = "CSE0001",
                NgaySinh = new DateTime(2006, 8, 15),
                GioiTinh = "Nam",
                DiaChi = "Hà Nội",
                SoDienThoai = "0912345678",
                Email = "a.nguyen@vju.ac.vn",
                TrangThai = "Đang học",
                Diem = 8.5
            });

            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000002",
                HoTen = "Trần Thị B",
                MaLop = "CSE0002",
                NgaySinh = new DateTime(2006, 1, 22),
                GioiTinh = "Nữ",
                DiaChi = "Hải Phòng",
                SoDienThoai = "0987654321",
                Email = "b.tran@vju.ac.vn",
                TrangThai = "Đang học",
                Diem = 9.0
            });

            sinhViens.Add(new SinhVien
            {
                MaSV = "SV000003",
                HoTen = "Lê Văn C",
                MaLop = "CSE0001",
                NgaySinh = new DateTime(2005, 5, 10),
                GioiTinh = "Nam",
                DiaChi = "Hải Dương",
                SoDienThoai = "0901234567",
                Email = "c.le@vju.ac.vn",
                TrangThai = "Đang học",
                Diem = 7.5
            });
        }

        // 1. Lấy toàn bộ danh sách sinh viên
        public List<SinhVien> GetAllSinhVien()
        {
            return sinhViens.ToList();
        }

        // 2. Tìm sinh viên theo mã
        public SinhVien? GetSinhVienById(string maSV)
        {
            return sinhViens.FirstOrDefault(
                sv => sv.MaSV == maSV
            );
        }

        // 3. Lọc sinh viên theo mã lớp
        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            return sinhViens
                .Where(sv => sv.MaLop == maLop)
                .ToList();
        }

        // 4. Thêm sinh viên
        public void AddSinhVien(SinhVien sv)
        {
            if (GetSinhVienById(sv.MaSV) != null)
            {
                throw new InvalidOperationException(
                    "Mã sinh viên đã tồn tại!"
                );
            }

            sinhViens.Add(sv);
        }

        // 5. Sửa thông tin sinh viên
        public void UpdateSinhVien(SinhVien sv)
        {
            SinhVien? existingSV =
                GetSinhVienById(sv.MaSV);

            if (existingSV == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần sửa!"
                );
            }

            existingSV.HoTen = sv.HoTen;
            existingSV.NgaySinh = sv.NgaySinh;
            existingSV.GioiTinh = sv.GioiTinh;
            existingSV.DiaChi = sv.DiaChi;
            existingSV.SoDienThoai = sv.SoDienThoai;
            existingSV.Email = sv.Email;
            existingSV.MaLop = sv.MaLop;
            existingSV.TrangThai = sv.TrangThai;
            existingSV.Diem = sv.Diem;
        }

        // 6. Xóa sinh viên
        public void DeleteSinhVien(string maSV)
        {
            SinhVien? sv = GetSinhVienById(maSV);

            if (sv == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần xóa!"
                );
            }

            sinhViens.Remove(sv);
        }
    }
}
