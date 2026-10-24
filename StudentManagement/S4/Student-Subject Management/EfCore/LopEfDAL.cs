
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.EfCore
{
    public class LopEfDAL
    {
        // 1. Lấy tất cả lớp học
        public List<LopHoc> GetAllLopHoc()
        {
            using var db = new QLSVDbContext();

            return db.LopHocs
                .AsNoTracking()
                .OrderBy(l => l.MaLop)
                .ToList();
        }

        // 2. Tìm lớp học theo mã
        public LopHoc? GetLopHocById(string maLop)
        {
            using var db = new QLSVDbContext();

            return db.LopHocs
                .AsNoTracking()
                .FirstOrDefault(l => l.MaLop == maLop);
        }

        // 3. Thêm lớp học
        public void AddLopHoc(LopHoc lh)
        {
            ArgumentNullException.ThrowIfNull(lh);

            using var db = new QLSVDbContext();

            if (db.LopHocs.Any(l => l.MaLop == lh.MaLop))
            {
                throw new InvalidOperationException(
                    "Mã lớp đã tồn tại!"
                );
            }

            db.LopHocs.Add(lh);
            db.SaveChanges();
        }

        // 4. Sửa lớp học
        public void UpdateLopHoc(LopHoc lh)
        {
            ArgumentNullException.ThrowIfNull(lh);

            using var db = new QLSVDbContext();

            LopHoc? existing = db.LopHocs.Find(lh.MaLop);

            if (existing == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần sửa!"
                );
            }

            existing.TenLop = lh.TenLop;

            db.SaveChanges();
        }

        // 5. Xóa lớp học
        public void DeleteLopHoc(string maLop)
        {
            using var db = new QLSVDbContext();

            LopHoc? lop = db.LopHocs.Find(maLop);

            if (lop == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần xóa!"
                );
            }

            // Không cho xóa lớp đang có sinh viên
            bool coSinhVien = db.SinhViens
                .Any(s => s.MaLop == maLop);

            if (coSinhVien)
            {
                throw new InvalidOperationException(
                    "Không thể xóa lớp đang có sinh viên!"
                );
            }

            db.LopHocs.Remove(lop);
            db.SaveChanges();
        }
    }
}
