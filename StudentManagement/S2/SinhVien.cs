using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExampleCAdvance.Entities
{
    public class SinhVien
    {
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        [RegularExpression("SV[0-9]{6}",
            ErrorMessage = "Mã sinh viên phải có định dạng SVxxxxxx, trong đó xxxxxx là 6 chữ số.")]
        public string MaSV { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [RegularExpression("CSE[0-9]{4}",
            ErrorMessage = "Mã lớp phải có định dạng CSExxxx, trong đó xxxx là 4 chữ số.")]
        public string MaLop { get; set; }

        public DateTime NgaySinh { get; set; }

        public string GioiTinh { get; set; }

        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        public string TrangThai { get; set; }

        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        public string SoDienThoai { get; set; }

        [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng 0 đến 10")]
        public double Diem { get; set; }

        public List<ValidationResult> Validate()
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            return results;
        }

        public bool IsValid()
        {
            return Validate().Count == 0;
        }
    }
}
