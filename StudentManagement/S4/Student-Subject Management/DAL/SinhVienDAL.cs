
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.DAL
{
    public class SinhVienDAL
    {
        private readonly DataHelper dataHelper = new DataHelper();

        // 1. Chuyển một dòng dữ liệu thành đối tượng SinhVien
        private SinhVien ConvertToSinhVien(DataRow row)
        {
            return new SinhVien
            {
                MaSV = row["MaSV"].ToString() ?? "",
                HoTen = row["HoTen"].ToString() ?? "",
                MaLop = row["MaLop"].ToString() ?? "",
                NgaySinh = Convert.ToDateTime(row["NgaySinh"]),
                GioiTinh = row["GioiTinh"].ToString() ?? "",
                Email = row["Email"].ToString() ?? "",
                TrangThai = row["TrangThai"].ToString() ?? "",
                SoDienThoai = row["SoDienThoai"].ToString() ?? "",
                DiaChi = row["DiaChi"].ToString() ?? "",
                Diem = Convert.ToDouble(row["Diem"])
            };
        }

        // 2. Lấy toàn bộ sinh viên
        public List<SinhVien> GetAllSinhVien()
        {
            List<SinhVien> danhSach = new List<SinhVien>();

            string sql = @"SELECT *
                           FROM dbo.SinhVien
                           ORDER BY MaSV";

            DataTable dt = dataHelper.GetDataTable(sql);

            foreach (DataRow row in dt.Rows)
            {
                danhSach.Add(ConvertToSinhVien(row));
            }

            return danhSach;
        }

        // 3. Tìm sinh viên theo mã
        public SinhVien? GetSinhVienById(string maSV)
        {
            string sql = @"SELECT *
                           FROM dbo.SinhVien
                           WHERE MaSV = @MaSV";

            DataTable dt = dataHelper.GetDataTable(
                sql,
                new SqlParameter("@MaSV", maSV)
            );

            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return ConvertToSinhVien(dt.Rows[0]);
        }

        // 4. Lấy sinh viên theo mã lớp
        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            List<SinhVien> danhSach = new List<SinhVien>();

            string sql = @"SELECT *
                           FROM dbo.SinhVien
                           WHERE MaLop = @MaLop
                           ORDER BY MaSV";

            DataTable dt = dataHelper.GetDataTable(
                sql,
                new SqlParameter("@MaLop", maLop)
            );

            foreach (DataRow row in dt.Rows)
            {
                danhSach.Add(ConvertToSinhVien(row));
            }

            return danhSach;
        }

        // 5. Tạo danh sách tham số cho thêm và sửa
        private SqlParameter[] CreateParameters(SinhVien sv)
        {
            return new SqlParameter[]
            {
                new SqlParameter("@MaSV", SqlDbType.NVarChar, 8)
                {
                    Value = sv.MaSV
                },
                new SqlParameter("@HoTen", SqlDbType.NVarChar, 100)
                {
                    Value = sv.HoTen
                },
                new SqlParameter("@MaLop", SqlDbType.NVarChar, 7)
                {
                    Value = sv.MaLop
                },
                new SqlParameter("@NgaySinh", SqlDbType.Date)
                {
                    Value = sv.NgaySinh.Date
                },
                new SqlParameter("@GioiTinh", SqlDbType.NVarChar, 10)
                {
                    Value = sv.GioiTinh
                },
                new SqlParameter("@Email", SqlDbType.NVarChar, 254)
                {
                    Value = sv.Email
                },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar, 30)
                {
                    Value = sv.TrangThai
                },
                new SqlParameter("@SoDienThoai", SqlDbType.VarChar, 10)
                {
                    Value = sv.SoDienThoai
                },
                new SqlParameter("@DiaChi", SqlDbType.NVarChar, 255)
                {
                    Value = sv.DiaChi
                },
                new SqlParameter("@Diem", SqlDbType.Float)
                {
                    Value = sv.Diem
                }
            };
        }

        // 6. Thêm sinh viên
        public void AddSinhVien(SinhVien sv)
        {
            if (GetSinhVienById(sv.MaSV) != null)
            {
                throw new InvalidOperationException(
                    "Mã sinh viên đã tồn tại!"
                );
            }

            string sql = @"
                INSERT INTO dbo.SinhVien
                (
                    MaSV, HoTen, MaLop, NgaySinh,
                    GioiTinh, Email, TrangThai,
                    SoDienThoai, DiaChi, Diem
                )
                VALUES
                (
                    @MaSV, @HoTen, @MaLop, @NgaySinh,
                    @GioiTinh, @Email, @TrangThai,
                    @SoDienThoai, @DiaChi, @Diem
                )";

            dataHelper.ExecuteNonQuery(
                sql,
                CreateParameters(sv)
            );
        }

        // 7. Sửa thông tin sinh viên
        public void UpdateSinhVien(SinhVien sv)
        {
            string sql = @"
                UPDATE dbo.SinhVien
                SET
                    HoTen = @HoTen,
                    MaLop = @MaLop,
                    NgaySinh = @NgaySinh,
                    GioiTinh = @GioiTinh,
                    Email = @Email,
                    TrangThai = @TrangThai,
                    SoDienThoai = @SoDienThoai,
                    DiaChi = @DiaChi,
                    Diem = @Diem
                WHERE MaSV = @MaSV";

            int soDong = dataHelper.ExecuteNonQuery(
                sql,
                CreateParameters(sv)
            );

            if (soDong == 0)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần sửa!"
                );
            }
        }

        // 8. Xóa sinh viên
        public void DeleteSinhVien(string maSV)
        {
            string sql = @"DELETE FROM dbo.SinhVien
                           WHERE MaSV = @MaSV";

            int soDong = dataHelper.ExecuteNonQuery(
                sql,
                new SqlParameter("@MaSV", maSV)
            );

            if (soDong == 0)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần xóa!"
                );
            }
        }
    }
}
