
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVienCodeFirst.Entities
{
    public class Sach
    {
        [Key]
        public int MaSach { get; set; }

        [Required]
        [MaxLength(200)]
        public string TenSach { get; set; } = string.Empty;

        public int NamXuatBan { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Gia { get; set; }

        public int MaTacGia { get; set; }

        [ForeignKey(nameof(MaTacGia))]
        public TacGia TacGia { get; set; } = null!;
    }
}
