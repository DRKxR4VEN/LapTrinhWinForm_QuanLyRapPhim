#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormDangNhap
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel bangChinh;
        private Label nhanTaiKhoan;
        private Label nhanMatKhau;
        private Label goiY;
        private TextBox oTaiKhoan;
        private TextBox oMatKhau;
        private CheckBox oGhiNho;
        private Button nutDangNhap;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bangChinh = new TableLayoutPanel();
            nhanTaiKhoan = new Label();
            nhanMatKhau = new Label();
            goiY = new Label();
            oTaiKhoan = new TextBox();
            oMatKhau = new TextBox();
            oGhiNho = new CheckBox();
            nutDangNhap = new Button();
            bangChinh.SuspendLayout();
            SuspendLayout();

            // bangChinh
            bangChinh.ColumnCount = 2;
            bangChinh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            bangChinh.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            bangChinh.Controls.Add(nhanTaiKhoan, 0, 0);
            bangChinh.Controls.Add(oTaiKhoan, 1, 0);
            bangChinh.Controls.Add(nhanMatKhau, 0, 1);
            bangChinh.Controls.Add(oMatKhau, 1, 1);
            bangChinh.Controls.Add(oGhiNho, 1, 2);
            bangChinh.Controls.Add(nutDangNhap, 1, 3);
            bangChinh.Dock = DockStyle.Fill;
            bangChinh.BackColor = Color.FromArgb(245, 247, 250);
            bangChinh.Padding = new Padding(18);
            bangChinh.RowCount = 4;
            bangChinh.RowStyles.Add(new RowStyle());
            bangChinh.RowStyles.Add(new RowStyle());
            bangChinh.RowStyles.Add(new RowStyle());
            bangChinh.RowStyles.Add(new RowStyle());
            bangChinh.Size = new Size(384, 211);

            // nhanTaiKhoan
            nhanTaiKhoan.AutoSize = true;
            nhanTaiKhoan.ForeColor = Color.FromArgb(35, 45, 60);
            nhanTaiKhoan.Margin = new Padding(5, 8, 3, 3);
            nhanTaiKhoan.Text = "Tài khoản";
            nhanTaiKhoan.TextAlign = ContentAlignment.MiddleLeft;

            // oTaiKhoan
            oTaiKhoan.BackColor = Color.White;
            oTaiKhoan.ForeColor = Color.FromArgb(35, 45, 60);
            oTaiKhoan.Margin = new Padding(3, 4, 3, 4);
            oTaiKhoan.Size = new Size(216, 25);

            // nhanMatKhau
            nhanMatKhau.AutoSize = true;
            nhanMatKhau.ForeColor = Color.FromArgb(35, 45, 60);
            nhanMatKhau.Margin = new Padding(5, 8, 3, 3);
            nhanMatKhau.Text = "Mật khẩu";
            nhanMatKhau.TextAlign = ContentAlignment.MiddleLeft;

            // oMatKhau
            oMatKhau.BackColor = Color.White;
            oMatKhau.ForeColor = Color.FromArgb(35, 45, 60);
            oMatKhau.Margin = new Padding(3, 4, 3, 4);
            oMatKhau.PasswordChar = '●';
            oMatKhau.Size = new Size(216, 25);

            // oGhiNho
            oGhiNho.AutoSize = true;
            oGhiNho.BackColor = Color.FromArgb(245, 247, 250);
            oGhiNho.ForeColor = Color.FromArgb(35, 45, 60);
            oGhiNho.Margin = new Padding(3, 6, 3, 0);
            oGhiNho.Text = "Ghi nhớ đăng nhập";

            // nutDangNhap
            nutDangNhap.AutoSize = true;
            nutDangNhap.BackColor = Color.FromArgb(35, 110, 220);
            nutDangNhap.Cursor = Cursors.Hand;
            nutDangNhap.FlatStyle = FlatStyle.Flat;
            nutDangNhap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            nutDangNhap.ForeColor = Color.White;
            nutDangNhap.Margin = new Padding(3, 8, 3, 0);
            nutDangNhap.Padding = new Padding(10, 6, 10, 6);
            nutDangNhap.Text = "Đăng nhập";
            nutDangNhap.UseVisualStyleBackColor = false;
            nutDangNhap.Click += nutDangNhap_Click;

            // goiY
            goiY.BackColor = Color.FromArgb(245, 247, 250);
            goiY.Dock = DockStyle.Bottom;
            goiY.Font = new Font("Segoe UI", 9F);
            goiY.ForeColor = Color.FromArgb(35, 45, 60);
            goiY.Height = 30;
            goiY.Text = "Mặc định: admin/123 hoặc nhanvien/123";
            goiY.TextAlign = ContentAlignment.MiddleCenter;

            // FormDangNhap
            AcceptButton = nutDangNhap;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(384, 241);
            Controls.Add(bangChinh);
            Controls.Add(goiY);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(35, 45, 60);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Quản lý rạp chiếu phim";
            bangChinh.ResumeLayout(false);
            bangChinh.PerformLayout();
            ResumeLayout(false);
        }
    }
}
