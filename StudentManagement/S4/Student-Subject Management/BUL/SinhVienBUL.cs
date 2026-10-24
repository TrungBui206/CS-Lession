
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
    public class QuanLySinhVien
    {
        private readonly bool suDungEfCore;

        private readonly SinhVienDAL sinhVienAdo = new();
        private readonly LopDAL lopAdo = new();

        private readonly SinhVienEfDAL sinhVienEf = new();
        private readonly LopEfDAL lopEf = new();

        // Mặc định sử dụng EF Core
        public QuanLySinhVien(bool suDungEfCore = true)
        {
            this.suDungEfCore = suDungEfCore;
        }

        // 1. Lấy toàn bộ sinh viên
        public List<SinhVien> GetAllSinhVien()
        {
            return suDungEfCore
                ? sinhVienEf.GetAllSinhVien()
                : sinhVienAdo.GetAllSinhVien();
        }

        // 2. Tìm sinh viên theo mã
        public SinhVien? GetSinhVienById(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
                return null;

            maSV = maSV.Trim();

            return suDungEfCore
                ? sinhVienEf.GetSinhVienById(maSV)
                : sinhVienAdo.GetSinhVienById(maSV);
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

        // 4. Kiểm tra lớp có tồn tại
        private bool LopHocTonTai(string maLop)
        {
            return suDungEfCore
                ? lopEf.GetLopHocById(maLop) != null
                : lopAdo.GetLopHocById(maLop) != null;
        }

        // 5. Chuẩn hóa dữ liệu
        private void ChuanHoaDuLieu(SinhVien sv)
        {
            sv.MaSV = sv.MaSV?.Trim() ?? "";
            sv.MaLop = sv.MaLop?.Trim() ?? "";

            sv.HoTen = Regex.Replace(
                sv.HoTen?.Trim() ?? "",
                @"\s+", " "
            );

            sv.Email = sv.Email?.Trim() ?? "";
            sv.SoDienThoai = sv.SoDienThoai?.Trim() ?? "";
            sv.GioiTinh = sv.GioiTinh?.Trim() ?? "";
            sv.TrangThai = sv.TrangThai?.Trim() ?? "";
            sv.DiaChi = sv.DiaChi?.Trim() ?? "";
        }

        // 6. Kiểm tra dữ liệu sinh viên
        public List<ValidationResult> ValidateSinhVien(
            SinhVien sv)
        {
            ArgumentNullException.ThrowIfNull(sv);

            ChuanHoaDuLieu(sv);

            List<ValidationResult> errors = sv.Validate();

            if (Regex.IsMatch(
                    sv.MaLop,
                    @"^CSE[0-9]{4}$") &&
                !LopHocTonTai(sv.MaLop))
            {
                errors.Add(new ValidationResult(
                    "Lớp học không tồn tại!",
                    new[] { nameof(SinhVien.MaLop) }
                ));
            }

            if (sv.NgaySinh.Date > DateTime.Today)
            {
                errors.Add(new ValidationResult(
                    "Ngày sinh không được lớn hơn hiện tại!",
                    new[] { nameof(SinhVien.NgaySinh) }
                ));
            }

            if (sv.GioiTinh != "Nam" &&
                sv.GioiTinh != "Nữ")
            {
                errors.Add(new ValidationResult(
                    "Giới tính không hợp lệ!",
                    new[] { nameof(SinhVien.GioiTinh) }
                ));
            }

            if (sv.TrangThai != "Đang học" &&
                sv.TrangThai != "Bảo lưu" &&
                sv.TrangThai != "Đã tốt nghiệp")
            {
                errors.Add(new ValidationResult(
                    "Trạng thái không hợp lệ!",
                    new[] { nameof(SinhVien.TrangThai) }
                ));
            }

            return errors;
        }

        // 7. Thêm sinh viên
        public void AddSinhVien(SinhVien sv)
        {
            ArgumentNullException.ThrowIfNull(sv);

            var errors = ValidateSinhVien(sv);

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n",
                        errors.Select(e => e.ErrorMessage))
                );
            }

            if (GetSinhVienById(sv.MaSV) != null)
            {
                throw new ValidationException(
                    "Mã sinh viên đã tồn tại!"
                );
            }

            if (suDungEfCore)
                sinhVienEf.AddSinhVien(sv);
            else
                sinhVienAdo.AddSinhVien(sv);
        }

        // 8. Sửa sinh viên
        public void UpdateSinhVien(SinhVien sv)
        {
            ArgumentNullException.ThrowIfNull(sv);

            var errors = ValidateSinhVien(sv);

            if (errors.Count > 0)
            {
                throw new ValidationException(
                    string.Join("\n",
                        errors.Select(e => e.ErrorMessage))
                );
            }

            if (GetSinhVienById(sv.MaSV) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần sửa!"
                );
            }

            if (suDungEfCore)
                sinhVienEf.UpdateSinhVien(sv);
            else
                sinhVienAdo.UpdateSinhVien(sv);
        }

        // 9. Xóa sinh viên
        public void DeleteSinhVien(string maSV)
        {
            if (string.IsNullOrWhiteSpace(maSV))
            {
                throw new ValidationException(
                    "Vui lòng nhập mã sinh viên cần xóa!"
                );
            }

            maSV = maSV.Trim();

            if (GetSinhVienById(maSV) == null)
            {
                throw new InvalidOperationException(
                    "Không tìm thấy sinh viên cần xóa!"
                );
            }

            if (suDungEfCore)
                sinhVienEf.DeleteSinhVien(maSV);
            else
                sinhVienAdo.DeleteSinhVien(maSV);
        }
    }
}
