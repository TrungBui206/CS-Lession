
using System;
using System.Collections.Generic;
using System.Linq;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.DAL
{
    public class LopDAL
    {
        private static readonly List<LopHoc> lopHocs
            = new List<LopHoc>();

        // Khởi tạo dữ liệu mẫu
        static LopDAL()
        {
            lopHocs.Add(new LopHoc
            {
                MaLop = "CSE0001",
                TenLop = "Khoa học máy tính 01"
            });

            lopHocs.Add(new LopHoc
            {
                MaLop = "CSE0002",
                TenLop = "Khoa học máy tính 02"
            });

            lopHocs.Add(new LopHoc
            {
                MaLop = "CSE0003",
                TenLop = "Khoa học máy tính 03"
            });
        }

        // 1. Lấy tất cả lớp học
        public List<LopHoc> GetAllLopHoc()
        {
            return lopHocs.ToList();
        }

        // 2. Tìm lớp học theo mã
        public LopHoc? GetLopHocById(string maLop)
        {
            return lopHocs.FirstOrDefault(
                lh => lh.MaLop == maLop
            );
        }

        // 3. Thêm lớp học
        public void AddLopHoc(LopHoc lh)
        {
            if (GetLopHocById(lh.MaLop) != null)
            {
                throw new InvalidOperationException(
                    "Mã lớp đã tồn tại!"
                );
            }

            lopHocs.Add(lh);
        }

        // 4. Sửa thông tin lớp học
        public void UpdateLopHoc(LopHoc lh)
        {
            LopHoc? existingLop =
                GetLopHocById(lh.MaLop);

            if (existingLop == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần sửa!"
                );
            }

            existingLop.TenLop = lh.TenLop;
        }

        // 5. Xóa lớp học
        public void DeleteLopHoc(string maLop)
        {
            LopHoc? lh = GetLopHocById(maLop);

            if (lh == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần xóa!"
                );
            }

            // Không xóa lớp khi vẫn còn sinh viên
            SinhVienDAL svDAL = new SinhVienDAL();

            if (svDAL.GetSinhVienByLop(maLop).Count > 0)
            {
                throw new InvalidOperationException(
                    "Không thể xóa lớp đang có sinh viên!"
                );
            }

            lopHocs.Remove(lh);
        }
    }
}
