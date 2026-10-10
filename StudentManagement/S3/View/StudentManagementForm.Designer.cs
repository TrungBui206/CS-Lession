namespace StudentSubjectManagement
{
    partial class StudentManagementForm
    {
        
        private System.ComponentModel.IContainer components = null;

        
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dateTimePicker1 = new DateTimePicker();
            txtDienThoai = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtEmail = new TextBox();
            txtMaSV = new TextBox();
            rbNam = new RadioButton();
            rbNu = new RadioButton();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            cmbLopHoc = new ComboBox();
            cmbTrangThai = new ComboBox();
            txtHoTen = new TextBox();
            label11 = new Label();
            dgvDanhSach = new DataGridView();
            nudDiem = new NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            SuspendLayout();
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(110, 146);
            dateTimePicker1.MaxDate = new DateTime(2026, 10, 25, 23, 59, 59, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(202, 27);
            dateTimePicker1.TabIndex = 3;
            // 
            // txtDienThoai
            // 
            txtDienThoai.Location = new Point(521, 232);
            txtDienThoai.Name = "txtDienThoai";
            txtDienThoai.Size = new Size(125, 27);
            txtDienThoai.TabIndex = 6;
            txtDienThoai.TextChanged += txtDienThoai_TextChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(397, 239);
            label6.Name = "label6";
            label6.Size = new Size(78, 20);
            label6.TabIndex = 7;
            label6.Text = "Điện thoại";
            label6.Click += label6_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(397, 151);
            label5.Name = "label5";
            label5.Size = new Size(65, 20);
            label5.TabIndex = 4;
            label5.Text = "Giới tính";
            label5.Click += label5_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 239);
            label4.Name = "label4";
            label4.Size = new Size(56, 20);
            label4.TabIndex = 5;
            label4.Text = "Email *";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 151);
            label3.Name = "label3";
            label3.Size = new Size(74, 20);
            label3.TabIndex = 4;
            label3.Text = "Ngày sinh";
            label3.Click += label3_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 73);
            label2.Name = "label2";
            label2.Size = new Size(61, 20);
            label2.TabIndex = 3;
            label2.Text = "Mã SV *";
            label2.Click += label2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(194, 26);
            label1.TabIndex = 9;
            label1.Text = "Thông tin sinh viên";
            label1.Click += label1_Click;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(110, 232);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(125, 27);
            txtEmail.TabIndex = 5;
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(110, 66);
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(125, 27);
            txtMaSV.TabIndex = 0;
            txtMaSV.TextChanged += textBox1_TextChanged;
            // 
            // rbNam
            // 
            rbNam.AutoSize = true;
            rbNam.Location = new Point(485, 149);
            rbNam.Name = "rbNam";
            rbNam.Size = new Size(62, 24);
            rbNam.TabIndex = 10;
            rbNam.TabStop = true;
            rbNam.Text = "Nam";
            rbNam.UseVisualStyleBackColor = true;
            rbNam.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // rbNu
            // 
            rbNu.AutoSize = true;
            rbNu.Location = new Point(616, 149);
            rbNu.Name = "rbNu";
            rbNu.Size = new Size(50, 24);
            rbNu.TabIndex = 11;
            rbNu.TabStop = true;
            rbNu.Text = "Nữ";
            rbNu.TextAlign = ContentAlignment.BottomRight;
            rbNu.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(397, 73);
            label7.Name = "label7";
            label7.Size = new Size(66, 20);
            label7.TabIndex = 12;
            label7.Text = "Lớp học ";
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(12, 329);
            label8.Name = "label8";
            label8.Size = new Size(0, 20);
            label8.TabIndex = 14;
            label8.Click += label8_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(12, 329);
            label9.Name = "label9";
            label9.Size = new Size(45, 20);
            label9.TabIndex = 15;
            label9.Text = "Điểm";
            label9.Click += label9_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(397, 329);
            label10.Name = "label10";
            label10.Size = new Size(75, 20);
            label10.TabIndex = 17;
            label10.Text = "Trạng thái";
            label10.Click += label10_Click;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(85, 355);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(94, 29);
            btnThem.TabIndex = 9;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(253, 355);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(94, 29);
            btnSua.TabIndex = 10;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(447, 355);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(94, 29);
            btnXoa.TabIndex = 11;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(639, 355);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(94, 29);
            btnLamMoi.TabIndex = 12;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // cmbLopHoc
            // 
            cmbLopHoc.FormattingEnabled = true;
            cmbLopHoc.Location = new Point(515, 73);
            cmbLopHoc.Name = "cmbLopHoc";
            cmbLopHoc.Size = new Size(151, 28);
            cmbLopHoc.TabIndex = 2;
            // 
            // cmbTrangThai
            // 
            cmbTrangThai.FormattingEnabled = true;
            cmbTrangThai.Location = new Point(515, 321);
            cmbTrangThai.Name = "cmbTrangThai";
            cmbTrangThai.Size = new Size(151, 28);
            cmbTrangThai.TabIndex = 8;
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(368, 26);
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(125, 27);
            txtHoTen.TabIndex = 1;
            txtHoTen.TextChanged += txtHoTen_TextChanged;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(254, 29);
            label11.Name = "label11";
            label11.Size = new Size(64, 20);
            label11.TabIndex = 25;
            label11.Text = "Họ tên *";
            // 
            // dgvDanhSach
            // 
            dgvDanhSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDanhSach.Location = new Point(3, 232);
            dgvDanhSach.Name = "dgvDanhSach";
            dgvDanhSach.RowHeadersWidth = 51;
            dgvDanhSach.Size = new Size(801, 222);
            dgvDanhSach.TabIndex = 26;
            // 
            // nudDiem
            // 
            nudDiem.DecimalPlaces = 1;
            nudDiem.Increment = new decimal(new int[] { 1, 0, 0, 65536 });
            nudDiem.Location = new Point(110, 322);
            nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(150, 27);
            nudDiem.TabIndex = 7;
            // 
            // StudentManagementForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvDanhSach);
            Controls.Add(label11);
            Controls.Add(txtHoTen);
            Controls.Add(cmbTrangThai);
            Controls.Add(cmbLopHoc);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(label10);
            Controls.Add(nudDiem);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(rbNu);
            Controls.Add(rbNam);
            Controls.Add(dateTimePicker1);
            Controls.Add(txtDienThoai);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtEmail);
            Controls.Add(txtMaSV);
            Name = "StudentManagementForm";
            Text = "StudentManagementForm";
            Load += StudentManagementForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDanhSach).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DateTimePicker dateTimePicker1;
        private TextBox txtDienThoai;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox txtEmail;
        private TextBox txtMaSV;
        private RadioButton rbNam;
        private RadioButton rbNu;
        private Label label7;
        private Label label8;
        private Label label9;
        private NumericUpDown nudDiem;
        private Label label10;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private ComboBox cmbLopHoc;
        private ComboBox cmbTrangThai;
        private TextBox txtHoTen;
        private Label label11;
        private DataGridView dgvDanhSach;
    }
}
