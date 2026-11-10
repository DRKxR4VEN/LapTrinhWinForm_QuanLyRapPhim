#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormCauHinh
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel tuyChon;
        private Label nhanCoChu;
        private NumericUpDown oCoChu;
        private Button nutMauNen;
        private Button nutMauChu;
        private Button nutLuu;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tuyChon = new FlowLayoutPanel();
            nhanCoChu = new Label();
            oCoChu = new NumericUpDown();
            nutMauNen = new Button();
            nutMauChu = new Button();
            nutLuu = new Button();
            ((System.ComponentModel.ISupportInitialize)oCoChu).BeginInit();
            SuspendLayout();

            // nhanCoChu
            nhanCoChu.AutoSize = true;
            nhanCoChu.ForeColor = Color.FromArgb(35, 45, 60);
            nhanCoChu.Margin = new Padding(5, 8, 3, 3);
            nhanCoChu.Text = "Cỡ chữ";
            nhanCoChu.TextAlign = ContentAlignment.MiddleLeft;

            // oCoChu
            oCoChu.DecimalPlaces = 0;
            oCoChu.Location = new Point(0, 0);
            oCoChu.Margin = new Padding(3, 4, 8, 3);
            oCoChu.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            oCoChu.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            oCoChu.Name = "oCoChu";
            oCoChu.Size = new Size(80, 23);
            oCoChu.Value = new decimal(new int[] { 9, 0, 0, 0 });

            // nutMauNen
            nutMauNen.AutoSize = true;
            nutMauNen.BackColor = Color.FromArgb(35, 110, 220);
            nutMauNen.Cursor = Cursors.Hand;
            nutMauNen.FlatStyle = FlatStyle.Flat;
            nutMauNen.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutMauNen.ForeColor = Color.White;
            nutMauNen.Margin = new Padding(4);
            nutMauNen.Padding = new Padding(10, 6, 10, 6);
            nutMauNen.Text = "Chọn màu nền";
            nutMauNen.UseVisualStyleBackColor = false;
            nutMauNen.Click += nutMauNen_Click;

            // nutMauChu
            nutMauChu.AutoSize = true;
            nutMauChu.BackColor = Color.FromArgb(35, 110, 220);
            nutMauChu.Cursor = Cursors.Hand;
            nutMauChu.FlatStyle = FlatStyle.Flat;
            nutMauChu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutMauChu.ForeColor = Color.White;
            nutMauChu.Margin = new Padding(4);
            nutMauChu.Padding = new Padding(10, 6, 10, 6);
            nutMauChu.Text = "Chọn màu chữ";
            nutMauChu.UseVisualStyleBackColor = false;
            nutMauChu.Click += nutMauChu_Click;

            // nutLuu
            nutLuu.AutoSize = true;
            nutLuu.BackColor = Color.FromArgb(35, 110, 220);
            nutLuu.Cursor = Cursors.Hand;
            nutLuu.FlatStyle = FlatStyle.Flat;
            nutLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutLuu.ForeColor = Color.White;
            nutLuu.Margin = new Padding(4);
            nutLuu.Padding = new Padding(10, 6, 10, 6);
            nutLuu.Text = "Lưu Registry";
            nutLuu.UseVisualStyleBackColor = false;
            nutLuu.Click += nutLuu_Click;

            // tuyChon
            tuyChon.AutoSize = true;
            tuyChon.BackColor = Color.FromArgb(245, 247, 250);
            tuyChon.Dock = DockStyle.Top;
            tuyChon.Padding = new Padding(10);
            tuyChon.WrapContents = true;
            tuyChon.Controls.Add(nhanCoChu);
            tuyChon.Controls.Add(oCoChu);
            tuyChon.Controls.Add(nutMauNen);
            tuyChon.Controls.Add(nutMauChu);
            tuyChon.Controls.Add(nutLuu);

            // FormCauHinh
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(404, 181);
            Controls.Add(tuyChon);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            StartPosition = FormStartPosition.CenterParent;
            Text = "Cấu hình giao diện";
            ((System.ComponentModel.ISupportInitialize)oCoChu).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
