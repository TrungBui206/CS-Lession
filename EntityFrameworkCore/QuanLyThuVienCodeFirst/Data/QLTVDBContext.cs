
using Microsoft.EntityFrameworkCore;
using QuanLyThuVienCodeFirst.Entities;

namespace QuanLyThuVienCodeFirst.Data
{
    public class QLTVDBContext : DbContext
    {
        public DbSet<TacGia> TacGias { get; set; }

        public DbSet<Sach> Sachs { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\MSSQLLocalDB;
                      Database=QLTVCodeFirst;
                      Trusted_Connection=True;
                      TrustServerCertificate=True;");
            }
        }
    }
}
