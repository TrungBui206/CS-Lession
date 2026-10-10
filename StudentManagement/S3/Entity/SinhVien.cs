
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSubjectManagement.Entities
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [RegularExpression(@"SV[0-9]{6}",
            ErrorMessage = "Mã sinh viên phải có dạng SVxxxxxx")]
        public string MaSV { get; set; } = "";

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; } = "";

        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [RegularExpression(@"CSE[0-9]{4}",
            ErrorMessage = "Mã lớp phải có dạng CSExxxx")]
        public string MaLop { get; set; } = "";

        public DateTime NgaySinh { get; set; } = DateTime.Today;

        public string GioiTinh { get; set; } = "";

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; } = "";

        public string TrangThai { get; set; } = "";

        [RegularExpression(@"^0[0-9]{9}$",
            ErrorMessage = "Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0")]
        public string SoDienThoai { get; set; } = "";

        public string DiaChi { get; set; } = "";

        [Range(0, 10, ErrorMessage = "Điểm phải từ 0 đến 10")]
        public double Diem { get; set; }

        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);

            Validator.TryValidateObject(
                this,
                context,
                results,
                true
            );

            return results;
        }

        public bool IsValid()
        {
            return Validate().Count == 0;
        }
    }
}
