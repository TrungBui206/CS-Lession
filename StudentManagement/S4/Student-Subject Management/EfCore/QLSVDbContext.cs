
using Microsoft.EntityFrameworkCore;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement.EfCore
{
    public class QLSVDbContext : DbContext
    {
        public DbSet<SinhVien> SinhViens => Set<SinhVien>();

        public DbSet<LopHoc> LopHocs => Set<LopHoc>();

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;
                      Database=QLSV;
                      Integrated Security=True;
                      TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Bảng LopHoc
            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.ToTable("LopHoc", "dbo");

                entity.HasKey(l => l.MaLop);

                entity.Property(l => l.MaLop)
                    .HasMaxLength(7)
                    .IsRequired();

                entity.Property(l => l.TenLop)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasMany(l => l.DanhSachSinhVien)
                    .WithOne()
                    .HasForeignKey(s => s.MaLop)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            // Bảng SinhVien
            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.ToTable("SinhVien", "dbo");

                entity.HasKey(s => s.MaSV);

                entity.Property(s => s.MaSV)
                    .HasMaxLength(8)
                    .IsRequired();

                entity.Property(s => s.HoTen)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(s => s.MaLop)
                    .HasMaxLength(7)
                    .IsRequired();

                entity.Property(s => s.NgaySinh)
                    .HasColumnType("date");

                entity.Property(s => s.GioiTinh)
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(s => s.Email)
                    .HasMaxLength(254)
                    .IsRequired();

                entity.Property(s => s.TrangThai)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(s => s.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired();

                entity.Property(s => s.DiaChi)
                    .HasMaxLength(255)
                    .IsRequired();

                entity.Property(s => s.Diem)
                    .HasColumnType("float");
            });
        }
    }
}
