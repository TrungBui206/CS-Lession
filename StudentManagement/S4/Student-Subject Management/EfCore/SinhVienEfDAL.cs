
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.EfCore
{
    public class SinhVienEfDAL
    {
        // 1. Lấy toàn bộ sinh viên
        public List<SinhVien> GetAllSinhVien()
        {
            using var db = new QLSVDbContext();

            return db.SinhViens
                .AsNoTracking()
                .OrderBy(s => s.MaSV)
                .ToList();
        }

        // 2. Tìm sinh viên theo mã
        public SinhVien? GetSinhVienById(string maSV)
        {
            using var db = new QLSVDbContext();

            return db.SinhViens
                .AsNoTracking()
                .FirstOrDefault(s => s.MaSV == maSV);
        }

        // 3. Lấy sinh viên theo lớp
        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            using var db = new QLSVDbContext();

            return db.SinhViens
                .AsNoTracking()
                .Where(s => s.MaLop == maLop)
                .OrderBy(s => s.MaSV)
                .ToList();
        }

        // 4. Thêm sinh viên
        public void AddSinhVien(SinhVien sv)
        {
            ArgumentNullException.ThrowIfNull(sv);

            using var db = new QLSVDbContext();

            if (db.SinhViens.Any(s => s.MaSV == sv.MaSV))
            {
                throw new InvalidOperationException(
                    "Mã sinh viên đã tồn tại!"
                );
            }

            db.SinhViens.Add(sv);
            db.SaveChanges();
        }

        // 5. Sửa sinh viên
        public void UpdateSinhVien(SinhVien sv)
        {
            ArgumentNullException.ThrowIfNull(sv);

            using var db = new QLSVDbContext();

            SinhVien? existing =
                db.SinhViens.Find(sv.MaSV);

            if (existing == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần sửa!"
                );
            }

            existing.HoTen = sv.HoTen;
            existing.MaLop = sv.MaLop;
            existing.NgaySinh = sv.NgaySinh;
            existing.GioiTinh = sv.GioiTinh;
            existing.Email = sv.Email;
            existing.TrangThai = sv.TrangThai;
            existing.SoDienThoai = sv.SoDienThoai;
            existing.DiaChi = sv.DiaChi;
            existing.Diem = sv.Diem;

            db.SaveChanges();
        }

        // 6. Xóa sinh viên
        public void DeleteSinhVien(string maSV)
        {
            using var db = new QLSVDbContext();

            SinhVien? sv = db.SinhViens.Find(maSV);

            if (sv == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần xóa!"
                );
            }

            db.SinhViens.Remove(sv);
            db.SaveChanges();
        }
    }
}
