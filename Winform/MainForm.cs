using System;
using System.Windows.Forms;
using System.Drawing;

namespace StudentSubjectManagement.Student_Subject_Management
{
    public partial class MainForm : Form
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnCong;
        private Button btnTru;
        private Button btnNhan;
        private Button btnChia;
        private TextBox txtSoA;
        private TextBox txtSoB;
        private TextBox txtKetQua;
        private ContextMenuStrip contextMenuStrip1;
        private Label lblSoA;
        private Label lblSoB;
        private Label lblKetQua;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            btnCong = new Button();
            btnTru = new Button();
            btnNhan = new Button();
            btnChia = new Button();
            txtSoA = new TextBox();
            txtSoB = new TextBox();
            txtKetQua = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip(this.components);
            lblSoA = new Label();
            lblSoB = new Label();
            lblKetQua = new Label();
            SuspendLayout();

            btnCong.Location = new Point(482, 94);
            btnCong.Name = "btnCong";
            btnCong.Size = new Size(94, 51);
            btnCong.TabIndex = 0;
            btnCong.Text = "Cộng";
            btnCong.UseVisualStyleBackColor = true;
            btnCong.Click += btnCong_Click;

            btnTru.Location = new Point(482, 176);
            btnTru.Name = "btnTru";
            btnTru.Size = new Size(94, 51);
            btnTru.TabIndex = 1;
            btnTru.Text = "Trừ";
            btnTru.UseVisualStyleBackColor = true;
            btnTru.Click += btnTru_Click;

            btnNhan.Location = new Point(609, 94);
            btnNhan.Name = "btnNhan";
            btnNhan.Size = new Size(94, 51);
            btnNhan.TabIndex = 2;
            btnNhan.Text = "Nhân";
            btnNhan.UseVisualStyleBackColor = true;
            btnNhan.Click += btnNhan_Click;

            btnChia.Location = new Point(609, 176);
            btnChia.Name = "btnChia";
            btnChia.Size = new Size(94, 51);
            btnChia.TabIndex = 3;
            btnChia.Text = "Chia";
            btnChia.UseVisualStyleBackColor = true;
            btnChia.Click += btnChia_Click;

            txtSoA.Location = new Point(145, 106);
            txtSoA.Name = "txtSoA";
            txtSoA.Size = new Size(231, 27);
            txtSoA.TabIndex = 4;

            txtSoB.Location = new Point(145, 200);
            txtSoB.Name = "txtSoB";
            txtSoB.Size = new Size(231, 27);
            txtSoB.TabIndex = 5;

            txtKetQua.Location = new Point(145, 306);
            txtKetQua.Name = "txtKetQua";
            txtKetQua.Size = new Size(231, 27);
            txtKetQua.TabIndex = 6;

            contextMenuStrip1.ImageScalingSize = new Size(20, 20);
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);

            lblSoA.AutoSize = true;
            lblSoA.Location = new Point(57, 113);
            lblSoA.Name = "lblSoA";
            lblSoA.Size = new Size(40, 20);
            lblSoA.TabIndex = 8;
            lblSoA.Text = "Số A";

            lblSoB.AutoSize = true;
            lblSoB.Location = new Point(57, 207);
            lblSoB.Name = "lblSoB";
            lblSoB.Size = new Size(39, 20);
            lblSoB.TabIndex = 9;
            lblSoB.Text = "Số B";

            lblKetQua.AutoSize = true;
            lblKetQua.Location = new Point(57, 309);
            lblKetQua.Name = "lblKetQua";
            lblKetQua.Size = new Size(60, 20);
            lblKetQua.TabIndex = 10;
            lblKetQua.Text = "Kết quả";

            ClientSize = new Size(782, 553);
            Controls.Add(lblKetQua);
            Controls.Add(lblSoB);
            Controls.Add(lblSoA);
            Controls.Add(txtKetQua);
            Controls.Add(txtSoB);
            Controls.Add(txtSoA);
            Controls.Add(btnChia);
            Controls.Add(btnNhan);
            Controls.Add(btnTru);
            Controls.Add(btnCong);
            Name = "MainForm";
            Text = "Student Subject Management";
            ResumeLayout(false);
            PerformLayout();
        }

        private void TinhToan(Func<double, double, double> phepTinh)
        {
            if (!double.TryParse(txtSoA.Text, out double a) ||
                !double.TryParse(txtSoB.Text, out double b))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ");
                return;
            }

            double ketQua = phepTinh(a, b);
            if (!double.IsNaN(ketQua))
            {
                txtKetQua.Text = ketQua.ToString();
            }
        }

        private void btnCong_Click(object sender, EventArgs e) => TinhToan((a, b) => a + b);
        private void btnTru_Click(object sender, EventArgs e) => TinhToan((a, b) => a - b);
        private void btnNhan_Click(object sender, EventArgs e) => TinhToan((a, b) => a * b);

        private void btnChia_Click(object sender, EventArgs e)
        {
            TinhToan((a, b) =>
            {
                if (b == 0)
                {
                    MessageBox.Show("Không thể chia cho 0");
                    return double.NaN;
                }
                return a / b;
            });
        }
    }
}
