using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCAdvance.Hinh
{
    public abstract class Hinh
    {
        public abstract double TinhDienTich();
        public abstract double TinhChuVi();
    }

    public class HinhTron : Hinh
    {
        private double banKinh;

        public double BanKinh
        {
            get { return banKinh; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Ban kinh hinh tron khong duoc am.");
                }
                banKinh = value;
            }
        }

        public HinhTron(double banKinh)
        {
            BanKinh = banKinh;
        }

        public double TinhDienTich()
        {
            return Math.PI * BanKinh * BanKinh;
        }

        public double TinhChuVi()
        {
            return 2 * Math.PI * BanKinh;
        }
    }
}
