#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel panel;
        private Label lblUser;
        private Label lblPass;
        private Label hintLabel;
        private TextBox user;
        private TextBox pass;
        private CheckBox remember;
        private Button loginButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panel = new TableLayoutPanel();
            lblUser = new Label();
            user = new TextBox();
            lblPass = new Label();
            pass = new TextBox();
            remember = new CheckBox();
            loginButton = new Button();
            hintLabel = new Label();
            panel.SuspendLayout();
            SuspendLayout();
            // 
            // panel
            // 
            panel.BackColor = Color.FromArgb(245, 247, 250);
            panel.ColumnCount = 2;
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65F));
            panel.Controls.Add(lblUser, 0, 0);
            panel.Controls.Add(user, 1, 0);
            panel.Controls.Add(lblPass, 0, 1);
            panel.Controls.Add(pass, 1, 1);
            panel.Controls.Add(remember, 1, 2);
            panel.Controls.Add(loginButton, 1, 3);
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 0);
            panel.Name = "panel";
            panel.Padding = new Padding(18);
            panel.RowCount = 4;
            panel.RowStyles.Add(new RowStyle());
            panel.RowStyles.Add(new RowStyle());
            panel.RowStyles.Add(new RowStyle());
            panel.RowStyles.Add(new RowStyle());
            panel.Size = new Size(384, 211);
            panel.TabIndex = 0;
            // 
            // lblUser
            // 
            lblUser.AutoSize = true;
            lblUser.ForeColor = Color.FromArgb(35, 45, 60);
            lblUser.Location = new Point(23, 26);
            lblUser.Margin = new Padding(5, 8, 3, 3);
            lblUser.Name = "lblUser";
            lblUser.Size = new Size(82, 23);
            lblUser.TabIndex = 0;
            lblUser.Text = "Tài khoản";
            lblUser.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // user
            // 
            user.BackColor = Color.White;
            user.ForeColor = Color.FromArgb(35, 45, 60);
            user.Location = new Point(142, 22);
            user.Margin = new Padding(3, 4, 3, 4);
            user.Name = "user";
            user.Size = new Size(216, 30);
            user.TabIndex = 1;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.ForeColor = Color.FromArgb(35, 45, 60);
            lblPass.Location = new Point(23, 64);
            lblPass.Margin = new Padding(5, 8, 3, 3);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(82, 23);
            lblPass.TabIndex = 2;
            lblPass.Text = "Mật khẩu";
            lblPass.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // pass
            // 
            pass.BackColor = Color.White;
            pass.ForeColor = Color.FromArgb(35, 45, 60);
            pass.Location = new Point(142, 60);
            pass.Margin = new Padding(3, 4, 3, 4);
            pass.Name = "pass";
            pass.PasswordChar = '●';
            pass.Size = new Size(216, 30);
            pass.TabIndex = 3;
            // 
            // remember
            // 
            remember.AutoSize = true;
            remember.BackColor = Color.FromArgb(245, 247, 250);
            remember.ForeColor = Color.FromArgb(35, 45, 60);
            remember.Location = new Point(142, 100);
            remember.Margin = new Padding(3, 6, 3, 0);
            remember.Name = "remember";
            remember.Size = new Size(181, 27);
            remember.TabIndex = 4;
            remember.Text = "Ghi nhớ đăng nhập";
            remember.UseVisualStyleBackColor = false;
            // 
            // loginButton
            // 
            loginButton.AutoSize = true;
            loginButton.BackColor = Color.FromArgb(35, 110, 220);
            loginButton.Cursor = Cursors.Hand;
            loginButton.FlatStyle = FlatStyle.Flat;
            loginButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            loginButton.ForeColor = Color.White;
            loginButton.Location = new Point(142, 135);
            loginButton.Margin = new Padding(3, 8, 3, 0);
            loginButton.Name = "loginButton";
            loginButton.Padding = new Padding(10, 6, 10, 6);
            loginButton.Size = new Size(130, 47);
            loginButton.TabIndex = 5;
            loginButton.Text = "Đăng nhập";
            loginButton.UseVisualStyleBackColor = false;
            loginButton.Click += loginButton_Click;
            // 
            // hintLabel
            // 
            hintLabel.BackColor = Color.FromArgb(245, 247, 250);
            hintLabel.Dock = DockStyle.Bottom;
            hintLabel.Font = new Font("Segoe UI", 9F);
            hintLabel.ForeColor = Color.FromArgb(35, 45, 60);
            hintLabel.Location = new Point(0, 211);
            hintLabel.Name = "hintLabel";
            hintLabel.Size = new Size(384, 30);
            hintLabel.TabIndex = 1;
            hintLabel.Text = "Mặc định: admin/123 hoặc nhanvien/123";
            hintLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LoginForm
            // 
            AcceptButton = loginButton;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(384, 241);
            Controls.Add(panel);
            Controls.Add(hintLabel);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(35, 45, 60);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập - Quản lý rạp chiếu phim";
            panel.ResumeLayout(false);
            panel.PerformLayout();
            ResumeLayout(false);
        }
    }
}
