using StudentSubjectManagement.Student_Subject_Management;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExampleCAdvance.Entities
{
    public class LopHoc
    {
        [Required(ErrorMessage = "Mã lớp không được để trống")]
        [RegularExpression("CSE[0-9]{4}",
            ErrorMessage = "Mã lớp phải có định dạng CSExxxx, trong đó xxxx là 4 chữ số.")]
        public string MaLop { get; set; }

        [Required(ErrorMessage = "Tên lớp không được để trống")]
        public string TenLop { get; set; }

        // Quan hệ 1 - n: 1 LopHoc có nhiều SinhVien
        public List<SinhVien> DanhSachSinhVien { get; set; } = new List<SinhVien>();

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
