using StudentSubjectManagement.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ExampleCAdvance
{
    public class QuanLyManager
    {
        private List<LopHoc> dsLopHoc;
        private List<SinhVien> dsSinhVien;

        public QuanLyManager(List<LopHoc> dsLopHoc, List<SinhVien> dsSinhVien)
        {
            this.dsLopHoc = dsLopHoc;
            this.dsSinhVien = dsSinhVien;
        }

        public List<LopHoc> GetAllLopHoc() => dsLopHoc;

        public List<SinhVien> GetAllSinhVien() => dsSinhVien;

        public SinhVien GetSinhVienByMaSV(string maSV)
        {
            return dsSinhVien.FirstOrDefault(sv => sv.MaSV == maSV);
        }

        public LopHoc GetLopHocByMaLop(string maLop)
        {
            return dsLopHoc.FirstOrDefault(l => l.MaLop == maLop);
        }

        public bool MaLopTonTai(string maLop)
        {
            return GetLopHocByMaLop(maLop) != null;
        }

        public void ThemSinhVien(SinhVien sv)
        {
            if (!sv.IsValid())
                throw new System.InvalidOperationException(
                    string.Join("\n", sv.Validate().Select(r => r.ErrorMessage)));

            if (!MaLopTonTai(sv.MaLop))
                throw new System.InvalidOperationException($"Mã lớp {sv.MaLop} không tồn tại");

            if (GetSinhVienByMaSV(sv.MaSV) != null)
                throw new System.InvalidOperationException($"Mã sinh viên {sv.MaSV} đã tồn tại");

            dsSinhVien.Add(sv);
        }
    }
}