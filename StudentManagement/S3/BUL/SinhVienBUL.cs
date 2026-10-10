
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using StudentSubjectManagement.DAL;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.BUL
{
    public class QuanLySinhVien
    {
        private readonly SinhVienDAL svd;
        private readonly LopDAL ld;

        public QuanLySinhVien()
        {
            svd = new SinhVienDAL();
            ld = new LopDAL();
        }

        public List<SinhVien> GetAllSinhVien()
        {
            return svd.GetAllSinhVien();
        }

        public SinhVien? GetSinhVienById(string maSV)
        {
            return svd.GetSinhVienById(maSV.Trim());
        }

        public List<SinhVien> GetSinhVienByLop(string maLop)
        {
            return svd.GetSinhVienByLop(maLop);
        }

        public List<ValidationResult> ValidateSinhVien(SinhVien sv)
        {
            var errors = sv.Validate();

            if (!string.IsNullOrWhiteSpace(sv.MaLop) &&
                ld.GetLopHocById(sv.MaLop) == null)
            {
                errors.Add(new ValidationResult(
                    "Lớp học không tồn tại!",
                    new[] { nameof(SinhVien.MaLop) }
                ));
            }

            if (sv.NgaySinh.Date > DateTime.Today)
            {
                errors.Add(new ValidationResult(
                    "Ngày sinh không được lớn hơn ngày hiện tại!",
                    new[] { nameof(SinhVien.NgaySinh) }
                ));
            }

            if (string.IsNullOrWhiteSpace(sv.GioiTinh))
            {
                errors.Add(new ValidationResult(
                    "Vui lòng chọn giới tính!",
                    new[] { nameof(SinhVien.GioiTinh) }
                ));
            }

            return errors;
        }

        public void AddSinhVien(SinhVien sv)
        {
            var errors = ValidateSinhVien(sv);

            if (svd.GetSinhVienById(sv.MaSV) != null)
            {
                errors.Add(new ValidationResult(
                    "Mã sinh viên đã tồn tại!",
                    new[] { nameof(SinhVien.MaSV) }
                ));
            }

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n", errors.Select(e => e.ErrorMessage))
                );
            }

            sv.HoTen = Regex.Replace(sv.HoTen.Trim(), @"\s+", " ");
            sv.Email = sv.Email.Trim();
            sv.SoDienThoai = sv.SoDienThoai.Trim();

            svd.AddSinhVien(sv);
        }

        public void UpdateSinhVien(SinhVien sv)
        {
            var errors = ValidateSinhVien(sv);

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n", errors.Select(e => e.ErrorMessage))
                );
            }

            if (svd.GetSinhVienById(sv.MaSV) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần sửa!"
                );
            }

            sv.HoTen = Regex.Replace(sv.HoTen.Trim(), @"\s+", " ");
            sv.Email = sv.Email.Trim();
            sv.SoDienThoai = sv.SoDienThoai.Trim();

            svd.UpdateSinhVien(sv);
        }

        public void DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new InvalidOperationException(
                    "Vui lòng nhập mã sinh viên cần xóa!"
                );
            }

            svd.DeleteSinhVien(maSV.Trim());
        }
    }
}
