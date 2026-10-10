
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentSubjectManagement.Entities
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [RegularExpression(@"CSE[0-9]{4}",
            ErrorMessage = "Mã lớp phải có định dạng CSExxxx")]
        public string MaLop { get; set; } = "";

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        public string TenLop { get; set; } = "";

        // Một lớp học có thể chứa nhiều sinh viên
        public List<SinhVien> DanhSachSinhVien { get; set; }
            = new List<SinhVien>();

        // Kiểm tra tính hợp lệ của dữ liệu lớp học
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

        // Trả về true nếu thông tin lớp học hợp lệ
        public bool IsValid()
        {
            return Validate().Count == 0;
        }
    }
}
