#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel options;
        private Label sizeLabel;
        private NumericUpDown fontSize;
        private Button backButton;
        private Button foreButton;
        private Button saveButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            options = new FlowLayoutPanel();
            sizeLabel = new Label();
            fontSize = new NumericUpDown();
            backButton = new Button();
            foreButton = new Button();
            saveButton = new Button();
            ((System.ComponentModel.ISupportInitialize)fontSize).BeginInit();
            SuspendLayout();

            // sizeLabel
            sizeLabel.AutoSize = true;
            sizeLabel.ForeColor = Color.FromArgb(35, 45, 60);
            sizeLabel.Margin = new Padding(5, 8, 3, 3);
            sizeLabel.Text = "Cỡ chữ";
            sizeLabel.TextAlign = ContentAlignment.MiddleLeft;

            // fontSize
            fontSize.DecimalPlaces = 0;
            fontSize.Location = new Point(0, 0);
            fontSize.Margin = new Padding(3, 4, 8, 3);
            fontSize.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            fontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            fontSize.Name = "fontSize";
            fontSize.Size = new Size(80, 23);
            fontSize.Value = new decimal(new int[] { 9, 0, 0, 0 });

            // backButton
            backButton.AutoSize = true;
            backButton.BackColor = Color.FromArgb(35, 110, 220);
            backButton.Cursor = Cursors.Hand;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.Margin = new Padding(4);
            backButton.Padding = new Padding(10, 6, 10, 6);
            backButton.Text = "Chọn màu nền";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;

            // foreButton
            foreButton.AutoSize = true;
            foreButton.BackColor = Color.FromArgb(35, 110, 220);
            foreButton.Cursor = Cursors.Hand;
            foreButton.FlatStyle = FlatStyle.Flat;
            foreButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            foreButton.ForeColor = Color.White;
            foreButton.Margin = new Padding(4);
            foreButton.Padding = new Padding(10, 6, 10, 6);
            foreButton.Text = "Chọn màu chữ";
            foreButton.UseVisualStyleBackColor = false;
            foreButton.Click += foreButton_Click;

            // saveButton
            saveButton.AutoSize = true;
            saveButton.BackColor = Color.FromArgb(35, 110, 220);
            saveButton.Cursor = Cursors.Hand;
            saveButton.FlatStyle = FlatStyle.Flat;
            saveButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            saveButton.ForeColor = Color.White;
            saveButton.Margin = new Padding(4);
            saveButton.Padding = new Padding(10, 6, 10, 6);
            saveButton.Text = "Lưu Registry";
            saveButton.UseVisualStyleBackColor = false;
            saveButton.Click += saveButton_Click;

            // options
            options.AutoSize = true;
            options.BackColor = Color.FromArgb(245, 247, 250);
            options.Dock = DockStyle.Top;
            options.Padding = new Padding(10);
            options.WrapContents = true;
            options.Controls.Add(sizeLabel);
            options.Controls.Add(fontSize);
            options.Controls.Add(backButton);
            options.Controls.Add(foreButton);
            options.Controls.Add(saveButton);

            // SettingsForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(404, 181);
            Controls.Add(options);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cấu hình giao diện";
            ((System.ComponentModel.ISupportInitialize)fontSize).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
