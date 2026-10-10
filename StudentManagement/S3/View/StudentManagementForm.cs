
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using StudentSubjectManagement.BUL;
using StudentSubjectManagement.Entities;

namespace StudentSubjectManagement
{
    public partial class StudentManagementForm : Form
    {
        private readonly QuanLySinhVien svb;
        private readonly LopBUL lb;
        private readonly ErrorProvider errorProvider;
        private bool dangHienThi = false;

        public StudentManagementForm()
        {
            InitializeComponent();

            svb = new QuanLySinhVien();
            lb = new LopBUL();

            errorProvider = new ErrorProvider();
            errorProvider.ContainerControl = this;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;

            txtMaSV.TextChanged += txtMaSV_TextChanged;
            cmbLopHoc.SelectedIndexChanged += cmbLopHoc_SelectedIndexChanged;
            dgvDanhSach.CellClick += dgvDanhSach_CellClick;

            LoadLopHoc();
            LoadTrangThai();
            LoadDataToGrid();
        }

        private void LoadLopHoc()
        {
            dangHienThi = true;

            cmbLopHoc.DisplayMember = "TenLop";
            cmbLopHoc.ValueMember = "MaLop";
            cmbLopHoc.DataSource = lb.GetAllLopHoc();
            cmbLopHoc.SelectedIndex = -1;
            cmbLopHoc.DropDownStyle = ComboBoxStyle.DropDownList;

            dangHienThi = false;
        }

        private void LoadTrangThai()
        {
            cmbTrangThai.Items.Clear();
            cmbTrangThai.Items.Add("Đang học");
            cmbTrangThai.Items.Add("Bảo lưu");
            cmbTrangThai.Items.Add("Đã tốt nghiệp");
            cmbTrangThai.SelectedIndex = 0;
            cmbTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadDataToGrid()
        {
            dgvDanhSach.DataSource = null;

            if (cmbLopHoc.SelectedValue is string maLop)
            {
                dgvDanhSach.DataSource = svb.GetSinhVienByLop(maLop);
            }
            else
            {
                dgvDanhSach.DataSource = svb.GetAllSinhVien();
            }

            dgvDanhSach.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvDanhSach.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvDanhSach.MultiSelect = false;
            dgvDanhSach.ReadOnly = true;
            dgvDanhSach.AllowUserToAddRows = false;
            dgvDanhSach.ClearSelection();
        }

        private void cmbLopHoc_SelectedIndexChanged(
            object? sender, EventArgs e)
        {
            if (dangHienThi)
                return;

            LoadDataToGrid();
        }

        private void txtMaSV_TextChanged(
            object? sender, EventArgs e)
        {
            if (dangHienThi)
                return;

            string maSV = txtMaSV.Text.Trim();

            if (string.IsNullOrWhiteSpace(maSV))
            {
                ClearThongTin();
                return;
            }

            SinhVien? sv = svb.GetSinhVienById(maSV);

            if (sv != null)
            {
                HienThiSinhVien(sv);
            }
            else
            {
                ClearThongTin();
            }
        }

        private void HienThiSinhVien(SinhVien sv)
        {
            dangHienThi = true;

            txtMaSV.Text = sv.MaSV;
            txtHoTen.Text = sv.HoTen;
            txtEmail.Text = sv.Email;
            txtDienThoai.Text = sv.SoDienThoai;

            dateTimePicker1.Value = sv.NgaySinh;

            rbNam.Checked = sv.GioiTinh == "Nam";
            rbNu.Checked = sv.GioiTinh == "Nữ";

            cmbLopHoc.SelectedValue = sv.MaLop;
            cmbTrangThai.SelectedItem = sv.TrangThai;

            nudDiem.Value = (decimal)sv.Diem;

            dangHienThi = false;
            LoadDataToGrid();
        }

        private void ClearThongTin()
        {
            dangHienThi = true;

            txtHoTen.Clear();
            txtEmail.Clear();
            txtDienThoai.Clear();

            dateTimePicker1.Value = DateTime.Today;

            rbNam.Checked = false;
            rbNu.Checked = false;

            cmbLopHoc.SelectedIndex = -1;
            cmbTrangThai.SelectedIndex = 0;

            nudDiem.Value = 0;
            errorProvider.Clear();

            dangHienThi = false;
            LoadDataToGrid();
        }

        private SinhVien LayThongTinSinhVien()
        {
            return new SinhVien
            {
                MaSV = txtMaSV.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                NgaySinh = dateTimePicker1.Value,
                GioiTinh = rbNam.Checked ? "Nam" :
                           rbNu.Checked ? "Nữ" : "",
                Email = txtEmail.Text.Trim(),
                SoDienThoai = txtDienThoai.Text.Trim(),
                MaLop = cmbLopHoc.SelectedValue?.ToString() ?? "",
                TrangThai = cmbTrangThai.SelectedItem?.ToString() ?? "",
                Diem = (double)nudDiem.Value
            };
        }

        private bool KiemTraDuLieu(SinhVien sv)
        {
            errorProvider.Clear();

            List<ValidationResult> errors =
                svb.ValidateSinhVien(sv);

            if (errors.Count == 0)
                return true;

            foreach (var error in errors)
            {
                string fieldName =
                    error.MemberNames.FirstOrDefault() ?? "";

                string message =
                    error.ErrorMessage ?? "Dữ liệu không hợp lệ";

                switch (fieldName)
                {
                    case "MaSV":
                        errorProvider.SetError(txtMaSV, message);
                        break;

                    case "HoTen":
                        errorProvider.SetError(txtHoTen, message);
                        break;

                    case "NgaySinh":
                        errorProvider.SetError(dateTimePicker1, message);
                        break;

                    case "GioiTinh":
                        errorProvider.SetError(rbNam, message);
                        break;

                    case "Email":
                        errorProvider.SetError(txtEmail, message);
                        break;

                    case "SoDienThoai":
                        errorProvider.SetError(txtDienThoai, message);
                        break;

                    case "MaLop":
                        errorProvider.SetError(cmbLopHoc, message);
                        break;

                    case "TrangThai":
                        errorProvider.SetError(cmbTrangThai, message);
                        break;

                    case "Diem":
                        errorProvider.SetError(nudDiem, message);
                        break;

                    default:
                        MessageBox.Show(message, "Lỗi dữ liệu",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        break;
                }
            }

            return false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SinhVien sv = LayThongTinSinhVien();

            if (!KiemTraDuLieu(sv))
                return;

            try
            {
                svb.AddSinhVien(sv);
                LoadDataToGrid();

                MessageBox.Show(
                    "Thêm sinh viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                btnLamMoi_Click(sender, e);
            }
            catch (Exception ex)
            {
                errorProvider.SetError(txtMaSV, ex.Message);
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            SinhVien sv = LayThongTinSinhVien();

            if (!KiemTraDuLieu(sv))
                return;

            try
            {
                svb.UpdateSinhVien(sv);
                LoadDataToGrid();

                MessageBox.Show(
                    "Cập nhật sinh viên thành công!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
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
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    svb.DeleteSinhVien(maSV);
                    LoadDataToGrid();
                    btnLamMoi_Click(sender, e);

                    MessageBox.Show("Xóa sinh viên thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dangHienThi = true;
            txtMaSV.Clear();
            dangHienThi = false;

            ClearThongTin();
        }

        private void dgvDanhSach_CellClick(
            object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (dgvDanhSach.Rows[e.RowIndex].DataBoundItem
                is SinhVien sv)
            {
                HienThiSinhVien(sv);
            }
        }

        private void StudentManagementForm_Load(
            object sender, EventArgs e)
        { }

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

        private void radioButton1_CheckedChanged(
            object sender, EventArgs e)
        { }

        private void button3_Click(object sender, EventArgs e) { }

        private void textBox1_TextChanged(
            object sender, EventArgs e)
        { }

        private void textBox1_TextChanged_1(
            object sender, EventArgs e)
        { }

        private void txtDienThoai_TextChanged(
            object sender, EventArgs e)
        { }

        private void txtHoTen_TextChanged(
            object sender, EventArgs e)
        { }
    }
}
