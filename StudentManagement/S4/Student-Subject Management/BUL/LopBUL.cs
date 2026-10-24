
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using StudentSubjectManagement.DAL;
using StudentSubjectManagement.EfCore;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.BUL
{
    public class LopBUL
    {
        private readonly bool suDungEfCore;

        private readonly LopDAL lopAdo = new();
        private readonly SinhVienDAL sinhVienAdo = new();

        private readonly LopEfDAL lopEf = new();
        private readonly SinhVienEfDAL sinhVienEf = new();

        // Mặc định dùng Entity Framework Core
        public LopBUL(bool suDungEfCore = true)
        {
            this.suDungEfCore = suDungEfCore;
        }

        // 1. Lấy tất cả lớp học
        public List<LopHoc> GetAllLopHoc()
        {
            return suDungEfCore
                ? lopEf.GetAllLopHoc()
                : lopAdo.GetAllLopHoc();
        }

        // 2. Tìm lớp học theo mã
        public LopHoc? GetLopHocById(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
                return null;

            maLop = maLop.Trim();

            return suDungEfCore
                ? lopEf.GetLopHocById(maLop)
                : lopAdo.GetLopHocById(maLop);
        }

        // 3. Lấy sinh viên theo lớp
        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
            {
                throw new ValidationException(
                    "Mã lớp không được để trống!"
                );
            }

            maLop = maLop.Trim();

            return suDungEfCore
                ? sinhVienEf.GetSinhVienByLop(maLop)
                : sinhVienAdo.GetSinhVienByLop(maLop);
        }

        // 4. Chuẩn hóa dữ liệu lớp học
        private void ChuanHoaDuLieu(LopHoc lh)
        {
            lh.MaLop = lh.MaLop?.Trim() ?? "";

            lh.TenLop = Regex.Replace(
                lh.TenLop?.Trim() ?? "",
                @"\s+", " "
            );
        }

        // 5. Kiểm tra dữ liệu lớp học
        public List<ValidationResult> ValidateLopHoc(LopHoc lh)
        {
            ArgumentNullException.ThrowIfNull(lh);

            ChuanHoaDuLieu(lh);

            List<ValidationResult> errors = lh.Validate();

            if (lh.TenLop.Length > 100)
            {
                errors.Add(new ValidationResult(
                    "Tên lớp không được vượt quá 100 ký tự!",
                    new[] { nameof(LopHoc.TenLop) }
                ));
            }

            return errors;
        }

        // 6. Thêm lớp học
        public void AddLopHoc(LopHoc lh)
        {
            ArgumentNullException.ThrowIfNull(lh);

            var errors = ValidateLopHoc(lh);

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n",
                        errors.Select(e => e.ErrorMessage))
                );
            }

            if (GetLopHocById(lh.MaLop) != null)
            {
                throw new ValidationException(
                    "Mã lớp đã tồn tại!"
                );
            }

            if (suDungEfCore)
                lopEf.AddLopHoc(lh);
            else
                lopAdo.AddLopHoc(lh);
        }

        // 7. Sửa lớp học
        public void UpdateLopHoc(LopHoc lh)
        {
            ArgumentNullException.ThrowIfNull(lh);

            var errors = ValidateLopHoc(lh);

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n",
                        errors.Select(e => e.ErrorMessage))
                );
            }

            if (GetLopHocById(lh.MaLop) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần sửa!"
                );
            }

            if (suDungEfCore)
                lopEf.UpdateLopHoc(lh);
            else
                lopAdo.UpdateLopHoc(lh);
        }

        // 8. Xóa lớp học
        public void DeleteLopHoc(string maLop)
        {
            if (string.IsNullOrWhiteSpace(maLop))
            {
                throw new ValidationException(
                    "Vui lòng nhập mã lớp cần xóa!"
                );
            }

            maLop = maLop.Trim();

            if (GetLopHocById(maLop) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy lớp học cần xóa!"
                );
            }

            if (GetSinhVienByLop(maLop).Count > 0)
            {
                throw new InvalidOperationException(
                    "Không thể xóa lớp đang có sinh viên!"
                );
            }

            if (suDungEfCore)
                lopEf.DeleteLopHoc(maLop);
            else
                lopAdo.DeleteLopHoc(maLop);
        }
    }
}
