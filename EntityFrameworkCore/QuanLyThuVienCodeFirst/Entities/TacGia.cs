
using System.ComponentModel.DataAnnotations;

namespace QuanLyThuVienCodeFirst.Entities
{
    public class TacGia
    {
        [Key]
        public int MaTacGia { get; set; }

        [Required]
        [MaxLength(100)]
        public string TenTacGia { get; set; } = string.Empty;

        [MaxLength(50)]
        public string QuocTich { get; set; } = string.Empty;
    }
}
