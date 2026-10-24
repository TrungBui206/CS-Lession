
using System;
using System.Linq;
using QuanLyThuVienCodeFirst.Data;
using QuanLyThuVienCodeFirst.Entities;

namespace QuanLyThuVienCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new QLTVDBContext();

            // Thêm tác giả nếu chưa tồn tại
            var tacGia = db.TacGias
                .FirstOrDefault(t => t.TenTacGia == "Nguyen Van A");

            if (tacGia == null)
            {
                tacGia = new TacGia
                {
                    TenTacGia = "Nguyen Van A",
                    QuocTich = "Viet Nam"
                };

                db.TacGias.Add(tacGia);
                db.SaveChanges();
            }

            // Thêm sách thứ nhất
            if (!db.Sachs.Any(s => s.TenSach == "Sach A"
                                  && s.MaTacGia == tacGia.MaTacGia))
            {
                db.Sachs.Add(new Sach
                {
                    TenSach = "Sach A",
                    NamXuatBan = 2024,
                    Gia = 85000,
                    MaTacGia = tacGia.MaTacGia
                });
            }

            // Thêm sách thứ hai
            if (!db.Sachs.Any(s => s.TenSach == "Sach B"
                                  && s.MaTacGia == tacGia.MaTacGia))
            {
                db.Sachs.Add(new Sach
                {
                    TenSach = "Sach B",
                    NamXuatBan = 2025,
                    Gia = 95000,
                    MaTacGia = tacGia.MaTacGia
                });
            }

            db.SaveChanges();

            // Hiển thị danh sách sách
            Console.WriteLine("DANH SACH SACH:");

            foreach (var sach in db.Sachs.ToList())
            {
                Console.WriteLine(
                    $"{sach.MaSach} - {sach.TenSach} - {sach.Gia:N0} VND"
                );
            }
        }
    }
}
