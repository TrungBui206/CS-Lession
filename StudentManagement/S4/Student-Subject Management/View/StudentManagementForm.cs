using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentSubjectManagement.BUL;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement
{
    public partial class StudentManagementForm : Form
    {
        private QuanLySinhVien svb;
        private LopBUL lb;
        private readonly ErrorProvider errorProvider;
        private bool dangHienThi;
        private bool dangDoiCongNghe;
        private int cheDoHienTai = 0; // 0: EF Core; 1: ADO.NET

        // Tạo bằng code để không phải sửa file Designer.
        private readonly ComboBox cmbCongNghe = new ComboBox();
        private readonly TextBox txtDiaChi = new TextBox();

        public StudentManagementForm()
        {
            InitializeComponent();

            svb = new QuanLySinhVien(true);
            lb = new LopBUL(true);

            errorProvider = new ErrorProvider
            {
                ContainerControl = this,
                BlinkStyle = ErrorBlinkStyle.NeverBlink
            };

            TaoBoChonCongNgheVaDiaChi();

            txtMaSV.TextChanged += txtMaSV_TextChanged;
            cmbLopHoc.SelectedIndexChanged += cmbLopHoc_SelectedIndexChanged;
            dgvDanhSach.CellClick += dgvDanhSach_CellClick;
        }

        private void TaoBoChonCongNgheVaDiaChi()
        {
            // Chừa vị trí cho các trường và các nút phía trên bảng.
            ClientSize = new Size(820, 710);
            dateTimePicker1.MaxDate = DateTime.Today;
            MinimumSize = new Size(830, 740);
            dgvDanhSach.Location = new Point(12, 405);
            dgvDanhSach.Size = new Size(792, 285);
            dgvDanhSach.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                  | AnchorStyles.Left | AnchorStyles.Right;

            var lblCongNghe = new Label
            {
                Text = "Truy cập dữ liệu:",
                AutoSize = true,
                Location = new Point(516, 16)
            };

            cmbCongNghe.Name = "cmbCongNghe";
            cmbCongNghe.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCongNghe.Location = new Point(640, 10);
            cmbCongNghe.Size = new Size(164, 28);
            cmbCongNghe.Items.AddRange(new object[] { "EF Core", "ADO.NET" });
            cmbCongNghe.SelectedIndex = 0;
            cmbCongNghe.SelectedIndexChanged += cmbCongNghe_SelectedIndexChanged;

            var lblDiaChi = new Label
            {
                Text = "Địa chỉ",
                AutoSize = true,
                Location = new Point(12, 286)
            };

            txtDiaChi.Name = "txtDiaChi";
            txtDiaChi.Location = new Point(110, 279);
            txtDiaChi.Size = new Size(550, 27);

            Controls.Add(lblCongNghe);
            Controls.Add(cmbCongNghe);
            Controls.Add(lblDiaChi);
            Controls.Add(txtDiaChi);

            Text = "Quản lý sinh viên - EF Core / ADO.NET";
        }

        // Đổi công nghệ nhưng vẫn sử dụng cùng database QLSV.
        private void cmbCongNghe_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangDoiCongNghe || cmbCongNghe.SelectedIndex == cheDoHienTai)
                return;

            int cheDoMoi = cmbCongNghe.SelectedIndex;
            bool suDungEfCore = cheDoMoi == 0;
            QuanLySinhVien svbCu = svb;
            LopBUL lbCu = lb;
            int cheDoCu = cheDoHienTai;

            try
            {
                // Thử truy vấn bằng công nghệ mới trước khi chuyển.
                var svbMoi = new QuanLySinhVien(suDungEfCore);
                var lbMoi = new LopBUL(suDungEfCore);
                List<LopHoc> dsLop = lbMoi.GetAllLopHoc();
                List<SinhVien> dsSinhVien = svbMoi.GetAllSinhVien();

                svb = svbMoi;
                lb = lbMoi;
                cheDoHienTai = cheDoMoi;

                bool trangThaiCu = dangHienThi;
                dangHienThi = true;
                try
                {
                    cmbLopHoc.DataSource = null;
                    cmbLopHoc.DisplayMember = nameof(LopHoc.TenLop);
                    cmbLopHoc.ValueMember = nameof(LopHoc.MaLop);
                    cmbLopHoc.DataSource = dsLop;
                    cmbLopHoc.SelectedIndex = -1;

                    txtMaSV.Clear();
                    ClearThongTin();
                    dgvDanhSach.DataSource = dsSinhVien;
                }
                finally
                {
                    dangHienThi = trangThaiCu;
                }
            }
            catch (Exception ex)
            {
                // Nếu kết nối thất bại, giữ lại công nghệ cũ.
                svb = svbCu;
                lb = lbCu;
                cheDoHienTai = cheDoCu;
                dangDoiCongNghe = true;
                try
                {
                    cmbCongNghe.SelectedIndex = cheDoCu;
                }
                finally
                {
                    dangDoiCongNghe = false;
                }

                ThongBaoLoi(ex);
            }
        }

        private void StudentManagementForm_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTrangThai();
                LoadLopHoc();
                LoadDataToGrid();
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void LoadLopHoc()
        {
            bool trangThaiCu = dangHienThi;
            dangHienThi = true;
            try
            {
                cmbLopHoc.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbLopHoc.DisplayMember = nameof(LopHoc.TenLop);
                cmbLopHoc.ValueMember = nameof(LopHoc.MaLop);
                cmbLopHoc.DataSource = lb.GetAllLopHoc();
                cmbLopHoc.SelectedIndex = -1;
            }
            finally
            {
                dangHienThi = trangThaiCu;
            }
        }

        private void LoadTrangThai()
        {
            cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.AddRange(new object[]
            {
                "Đang học", "Bảo lưu", "Đã tốt nghiệp"
            });
            cmbTrangThai.SelectedIndex = 0;
        }

        private void LoadDataToGrid()
        {
            dgvDanhSach.DataSource = null;
            dgvDanhSach.DataSource = cmbLopHoc.SelectedValue is string maLop
                ? svb.GetSinhVienByLop(maLop)
                : svb.GetAllSinhVien();

            dgvDanhSach.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDanhSach.MultiSelect = false;
            dgvDanhSach.ReadOnly = true;
            dgvDanhSach.AllowUserToAddRows = false;
            dgvDanhSach.ClearSelection();
        }

        private void cmbLopHoc_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (dangHienThi) return;

            try
            {
                LoadDataToGrid();
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void txtMaSV_TextChanged(object? sender, EventArgs e)
        {
            if (dangHienThi) return;

            try
            {
                string maSV = txtMaSV.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                {
                    ClearThongTin();
                    LoadDataToGrid();
                    return;
                }

                // Chỉ tìm trên SQL Server khi nhập đủ mã sinh viên.
                if (maSV.Length != 8)
                {
                    ClearThongTin();
                    return;
                }

                SinhVien? sv = svb.GetSinhVienById(maSV);
                if (sv == null) ClearThongTin();
                else HienThiSinhVien(sv);
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void HienThiSinhVien(SinhVien sv)
        {
            bool trangThaiCu = dangHienThi;
            dangHienThi = true;
            try
            {
                txtMaSV.Text = sv.MaSV;
                txtHoTen.Text = sv.HoTen;
                txtEmail.Text = sv.Email;
                txtDienThoai.Text = sv.SoDienThoai;
                txtDiaChi.Text = sv.DiaChi;
                dateTimePicker1.Value = sv.NgaySinh.Date;
                rbNam.Checked = sv.GioiTinh == "Nam";
                rbNu.Checked = sv.GioiTinh == "Nữ";
                cmbLopHoc.SelectedValue = sv.MaLop;
                cmbTrangThai.SelectedItem = sv.TrangThai;
                nudDiem.Value = Math.Min(nudDiem.Maximum,
                    Math.Max(nudDiem.Minimum, (decimal)sv.Diem));
                errorProvider.Clear();
            }
            finally
            {
                dangHienThi = trangThaiCu;
            }
        }

        private void ClearThongTin()
        {
            bool trangThaiCu = dangHienThi;
            dangHienThi = true;
            try
            {
                txtHoTen.Clear();
                txtEmail.Clear();
                txtDienThoai.Clear();
                txtDiaChi.Clear();
                dateTimePicker1.Value = DateTime.Today;
                rbNam.Checked = false;
                rbNu.Checked = false;
                cmbLopHoc.SelectedIndex = -1;
                if (cmbTrangThai.Items.Count > 0)
                    cmbTrangThai.SelectedIndex = 0;
                nudDiem.Value = 0;
                errorProvider.Clear();
            }
            finally
            {
                dangHienThi = trangThaiCu;
            }
        }

        private SinhVien LayThongTinSinhVien()
        {
            return new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dateTimePicker1.Value.Date,
                GioiTinh = rbNam.Checked ? "Nam" : rbNu.Checked ? "Nữ" : "",
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtDienThoai.Text.Trim(),
                MaLop = cmbLopHoc.SelectedValue?.ToString() ?? "",
                TrangThai = cmbTrangThai.SelectedItem?.ToString() ?? "",
                Diem = (double)nudDiem.Value,
                DiaChi = txtDiaChi.Text.Trim()
            };
        }

        private bool KiemTraDuLieu(SinhVien sv)
        {
            errorProvider.Clear();
            List<ValidationResult> errors = svb.ValidateSinhVien(sv);

            foreach (ValidationResult error in errors)
            {
                string fieldName = error.MemberNames.FirstOrDefault() ?? "";
                string message = error.ErrorMessage ?? "Dữ liệu không hợp lệ";

                switch (fieldName)
                {
                    case nameof(SinhVien.MaSV):
                        errorProvider.SetError(txtMaSV, message); break;
                    case nameof(SinhVien.HoTen):
                        errorProvider.SetError(txtHoTen, message); break;
                    case nameof(SinhVien.NgaySinh):
                        errorProvider.SetError(dateTimePicker1, message); break;
                    case nameof(SinhVien.GioiTinh):
                        errorProvider.SetError(rbNam, message); break;
                    case nameof(SinhVien.Email):
                        errorProvider.SetError(txtEmail, message); break;
                    case nameof(SinhVien.SoDienThoai):
                        errorProvider.SetError(txtDienThoai, message); break;
                    case nameof(SinhVien.MaLop):
                        errorProvider.SetError(cmbLopHoc, message); break;
                    case nameof(SinhVien.TrangThai):
                        errorProvider.SetError(cmbTrangThai, message); break;
                    case nameof(SinhVien.Diem):
                        errorProvider.SetError(nudDiem, message); break;
                    case nameof(SinhVien.DiaChi):
                        errorProvider.SetError(txtDiaChi, message); break;
                    default:
                        MessageBox.Show(message, "Lỗi dữ liệu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
            }

            return errors.Count == 0;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVien sv = LayThongTinSinhVien();
                if (!KiemTraDuLieu(sv)) return;

                svb.AddSinhVien(sv);
                LamMoiForm();
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                SinhVien sv = LayThongTinSinhVien();
                if (!KiemTraDuLieu(sv)) return;

                svb.UpdateSinhVien(sv);
                LoadDataToGrid();
                MessageBox.Show("Cập nhật sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maSV = txtMaSV.Text.Trim();
                if (string.IsNullOrWhiteSpace(maSV))
                {
                    MessageBox.Show("Vui lòng nhập mã sinh viên cần xóa!");
                    return;
                }

                SinhVien? sv = svb.GetSinhVienById(maSV);
                if (sv == null)
                {
                    MessageBox.Show("Không tìm thấy sinh viên!");
                    return;
                }

                DialogResult result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa sinh viên {sv.HoTen}?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result != DialogResult.Yes) return;

                svb.DeleteSinhVien(maSV);
                LamMoiForm();
                MessageBox.Show("Xóa sinh viên thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void LamMoiForm()
        {
            bool trangThaiCu = dangHienThi;
            dangHienThi = true;
            try
            {
                txtMaSV.Clear();
                ClearThongTin();
            }
            finally
            {
                dangHienThi = trangThaiCu;
            }
            LoadDataToGrid();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            try
            {
                LamMoiForm();
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void dgvDanhSach_CellClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (dgvDanhSach.Rows[e.RowIndex].DataBoundItem is not SinhVien sv)
                return;

            try
            {
                HienThiSinhVien(sv);
            }
            catch (Exception ex)
            {
                ThongBaoLoi(ex);
            }
        }

        private void ThongBaoLoi(Exception ex)
        {
            MessageBox.Show(ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Giữ các handler được Designer gắn sẵn.
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label7_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label9_Click(object sender, EventArgs e) { }
        private void label10_Click(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged_1(object sender, EventArgs e) { }
        private void txtDienThoai_TextChanged(object sender, EventArgs e) { }
        private void txtHoTen_TextChanged(object sender, EventArgs e) { }
    }
}
