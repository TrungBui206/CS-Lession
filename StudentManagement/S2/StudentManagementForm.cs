using ExampleCAdvance;
using ExampleCAdvance.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StudentSubjectManagement
{
    public partial class StudentManagementForm : Form
    {
        private StudentManager manager;

        public StudentManagementForm()
        {
            InitializeComponent();
            manager = new StudentManager(StudentData.GetSampleStudents());
            LoadDataToGrid();
        }

        private void LoadDataToGrid()
        {
            dgvDanhSach.DataSource = null;
            dgvDanhSach.DataSource = manager.GetAllStudents().ToList();
        }

        private void StudentManagementForm_Load(object sender, EventArgs e)
        {

        }

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

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) || string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ Mã sinh viên và Họ tên");
                return;
            }

            try
            {
                var student = new Student
                {
                    StuID = txtMaSV.Text.Trim(),
                    Name = txtHoTen.Text.Trim(),
                    MidPoint = (double)nudDiem.Value,
                    FinalPoint = (double)nudDiem.Value,
                    Email = txtEmail.Text.Trim()
                };

                manager.AddStudent(student);
                LoadDataToGrid();
                btnLamMoi_Click(sender, e);
                MessageBox.Show("Thêm sinh viên thành công");
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần sửa từ danh sách");
                return;
            }

            var student = manager.GetStudentById(txtMaSV.Text.Trim());
            if (student == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên với mã này");
                return;
            }

            manager.UpdateStudent(
                txtMaSV.Text.Trim(),
                txtHoTen.Text.Trim(),
                (double)nudDiem.Value,
                (double)nudDiem.Value,
                txtEmail.Text.Trim()
            );

            LoadDataToGrid();
            btnLamMoi_Click(sender, e);
            MessageBox.Show("Cập nhật thành công");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSV.Text))
            {
                MessageBox.Show("Vui lòng chọn sinh viên cần xóa");
                return;
            }

            var student = manager.GetStudentById(txtMaSV.Text.Trim());
            if (student == null)
            {
                MessageBox.Show("Không tìm thấy sinh viên");
                return;
            }

            var confirm = MessageBox.Show(
                $"Xác nhận xóa sinh viên {student.Name}?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                manager.RemoveStudent(txtMaSV.Text.Trim());
                LoadDataToGrid();
                btnLamMoi_Click(sender, e);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSV.Clear();
            txtHoTen.Clear();
            txtEmail.Clear();
            txtDienThoai.Clear();
            nudDiem.Value = 0;
            dgvDanhSach.ClearSelection();
        }
    }
}
