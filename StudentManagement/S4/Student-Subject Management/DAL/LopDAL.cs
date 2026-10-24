
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.DAL
{
    public class LopDAL
    {
        private readonly DataHelper dataHelper = new DataHelper();

        // 1. Lấy tất cả lớp học từ SQL Server
        public List<LopHoc> GetAllLopHoc()
        {
            List<LopHoc> danhSach = new List<LopHoc>();

            string sql = @"SELECT MaLop, TenLop
                           FROM dbo.LopHoc
                           ORDER BY MaLop";

            DataTable dt = dataHelper.GetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                LopHoc lop = new LopHoc
                {
                    MaLop = row["MaLop"].ToString() ?? "",
                    TenLop = row["TenLop"].ToString() ?? ""
                };

                danhSach.Add(lop);
            }

            return danhSach;
        }

        // 2. Tìm lớp học theo mã
        public LopHoc? GetLopHocById(string maLop)
        {
            string sql = @"SELECT MaLop, TenLop
                           FROM dbo.LopHoc
                           WHERE MaLop = @MaLop";

            DataTable dt = dataHelper.GetDataTable(
                sql,
                new SqlParameter("@MaLop", maLop)
            );

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            DataRow row = dt.Rows[0];

            return new LopHoc
            {
                MaLop = row["MaLop"].ToString() ?? "",
                TenLop = row["TenLop"].ToString() ?? ""
            };
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

            string sql = @"INSERT INTO dbo.LopHoc
                           (MaLop, TenLop)
                           VALUES (@MaLop, @TenLop)";

            dataHelper.ExecuteNonQuery(
                sql,
                new SqlParameter("@MaLop", lh.MaLop),
                new SqlParameter("@TenLop", lh.TenLop)
            );
        }

        // 4. Sửa thông tin lớp học
        public void UpdateLopHoc(LopHoc lh)
        {
            string sql = @"UPDATE dbo.LopHoc
                           SET TenLop = @TenLop
                           WHERE MaLop = @MaLop";

            int soDong = dataHelper.ExecuteNonQuery(
                sql,
                new SqlParameter("@TenLop", lh.TenLop),
                new SqlParameter("@MaLop", lh.MaLop)
            );

            if (soDong == 0)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần sửa!"
                );
            }
        }

        // 5. Xóa lớp học
        public void DeleteLopHoc(string maLop)
        {
            if (GetLopHocById(maLop) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần xóa!"
                );
            }

            // Kiểm tra lớp có sinh viên không
            string sqlKiemTra =
                @"SELECT COUNT(*)
                  FROM dbo.SinhVien
                  WHERE MaLop = @MaLop";

            object? ketQua = dataHelper.ExecuteScalar(
                sqlKiemTra,
                new SqlParameter("@MaLop", maLop)
            );

            int soSinhVien = Convert.ToInt32(ketQua);

            if (soSinhVien > 0)
            {
                throw new InvalidOperationException(
                    "Không thể xóa lớp đang có sinh viên!"
                );
            }

            string sqlXoa =
                @"DELETE FROM dbo.LopHoc
                  WHERE MaLop = @MaLop";

            int soDong = dataHelper.ExecuteNonQuery(
                sqlXoa,
                new SqlParameter("@MaLop", maLop)
            );

            if (soDong == 0)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần xóa!"
                );
            }
        }
    }
}
