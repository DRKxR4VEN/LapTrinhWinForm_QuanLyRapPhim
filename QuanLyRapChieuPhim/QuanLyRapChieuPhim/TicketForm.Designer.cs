#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class TicketForm
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel topPanel;
        private FlowLayoutPanel screeningGroup;
        private Label lblScreening;
        private ComboBox screening;
        private FlowLayoutPanel typePanel;
        private Label lblType;
        private RadioButton adult;
        private RadioButton student;
        private FlowLayoutPanel snackGroup;
        private Label lblSnack;
        private ComboBox snack;
        private Button sellButton;
        private Panel rightPanel;
        private Label soldTitle;
        private ListView soldList;
        private Label soldInfo;
        private FlowLayoutPanel actionsPanel;
        private Button refundButton;
        private Button printButton;
        private FlowLayoutPanel bottomPanel;
        private Label status;
        private Label cart;
        private Label legend;
        private Panel seatHost;
        private FlowLayoutPanel seats;
        private System.Windows.Forms.Timer refreshTimer;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            topPanel = new FlowLayoutPanel();
            screeningGroup = new FlowLayoutPanel();
            typePanel = new FlowLayoutPanel();
            snackGroup = new FlowLayoutPanel();
            adult = new RadioButton();
            student = new RadioButton();
            lblScreening = new Label();
            screening = new ComboBox();
            lblType = new Label();
            lblSnack = new Label();
            snack = new ComboBox();
            sellButton = new Button();
            rightPanel = new Panel();
            soldList = new ListView();
            soldInfo = new Label();
            actionsPanel = new FlowLayoutPanel();
            refundButton = new Button();
            printButton = new Button();
            soldTitle = new Label();
            bottomPanel = new FlowLayoutPanel();
            status = new Label();
            cart = new Label();
            legend = new Label();
            seatHost = new Panel();
            seats = new FlowLayoutPanel();
            refreshTimer = new System.Windows.Forms.Timer(components);
            topPanel.SuspendLayout();
            screeningGroup.SuspendLayout();
            typePanel.SuspendLayout();
            snackGroup.SuspendLayout();
            rightPanel.SuspendLayout();
            actionsPanel.SuspendLayout();
            bottomPanel.SuspendLayout();
            seatHost.SuspendLayout();
            SuspendLayout();

            // lblScreening
            lblScreening.AutoSize = true;
            lblScreening.ForeColor = Color.FromArgb(35, 45, 60);
            lblScreening.Margin = new Padding(5, 8, 3, 3);
            lblScreening.Text = "Suất chiếu";
            lblScreening.TextAlign = ContentAlignment.MiddleLeft;

            // screening
            screening.DropDownStyle = ComboBoxStyle.DropDownList;
            screening.Margin = new Padding(3, 4, 3, 4);
            screening.Size = new Size(280, 23);
            screening.SelectedIndexChanged += screening_SelectedIndexChanged;

            // screeningGroup
            screeningGroup.AutoSize = true;
            screeningGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            screeningGroup.BackColor = Color.FromArgb(245, 247, 250);
            screeningGroup.Controls.Add(lblScreening);
            screeningGroup.Controls.Add(screening);
            screeningGroup.Margin = new Padding(0, 0, 10, 0);
            screeningGroup.WrapContents = false;

            // lblType
            lblType.AutoSize = true;
            lblType.ForeColor = Color.FromArgb(35, 45, 60);
            lblType.Margin = new Padding(0, 10, 4, 0);
            lblType.Text = "Loại vé";

            // adult
            adult.AutoSize = true;
            adult.BackColor = Color.FromArgb(245, 247, 250);
            adult.Checked = true;
            adult.ForeColor = Color.FromArgb(35, 45, 60);
            adult.Margin = new Padding(2, 9, 14, 0);
            adult.Text = "Người lớn";
            adult.CheckedChanged += adult_CheckedChanged;

            // student
            student.AutoSize = true;
            student.BackColor = Color.FromArgb(245, 247, 250);
            student.ForeColor = Color.FromArgb(35, 45, 60);
            student.Margin = new Padding(2, 9, 0, 0);
            student.Text = "Học sinh - Sinh viên";
            student.CheckedChanged += student_CheckedChanged;

            // typePanel
            typePanel.AutoSize = true;
            typePanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            typePanel.BackColor = Color.FromArgb(245, 247, 250);
            typePanel.Controls.Add(lblType);
            typePanel.Controls.Add(adult);
            typePanel.Controls.Add(student);
            typePanel.Margin = new Padding(8, 0, 8, 0);
            typePanel.WrapContents = false;

            // lblSnack
            lblSnack.AutoSize = true;
            lblSnack.ForeColor = Color.FromArgb(35, 45, 60);
            lblSnack.Margin = new Padding(5, 8, 3, 3);
            lblSnack.Text = "Bắp nước";
            lblSnack.TextAlign = ContentAlignment.MiddleLeft;

            // snack
            snack.DropDownStyle = ComboBoxStyle.DropDownList;
            snack.Margin = new Padding(3, 4, 3, 4);
            snack.Size = new Size(185, 23);
            snack.SelectedIndexChanged += snack_SelectedIndexChanged;

            // snackGroup
            snackGroup.AutoSize = true;
            snackGroup.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            snackGroup.BackColor = Color.FromArgb(245, 247, 250);
            snackGroup.Controls.Add(lblSnack);
            snackGroup.Controls.Add(snack);
            snackGroup.Margin = new Padding(0, 0, 10, 0);
            snackGroup.WrapContents = false;

            // sellButton
            sellButton.AutoSize = true;
            sellButton.BackColor = Color.FromArgb(35, 110, 220);
            sellButton.Cursor = Cursors.Hand;
            sellButton.FlatStyle = FlatStyle.Flat;
            sellButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            sellButton.ForeColor = Color.White;
            sellButton.Margin = new Padding(4);
            sellButton.Padding = new Padding(10, 6, 10, 6);
            sellButton.Text = "Bán vé đã chọn";
            sellButton.UseVisualStyleBackColor = false;
            sellButton.Click += sellButton_Click;

            // topPanel
            topPanel.AutoSize = true;
            topPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            topPanel.BackColor = Color.FromArgb(245, 247, 250);
            topPanel.Controls.Add(screeningGroup);
            topPanel.Controls.Add(typePanel);
            topPanel.Controls.Add(snackGroup);
            topPanel.Controls.Add(sellButton);
            topPanel.Dock = DockStyle.Top;
            topPanel.Padding = new Padding(10);

            // soldTitle
            soldTitle.Dock = DockStyle.Top;
            soldTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            soldTitle.ForeColor = Color.FromArgb(35, 45, 60);
            soldTitle.Height = 28;
            soldTitle.Text = "VÉ ĐÃ BÁN";

            // soldList
            soldList.BackColor = Color.White;
            soldList.BorderStyle = BorderStyle.FixedSingle;
            soldList.Columns.Add("Ghế", 44);
            soldList.Columns.Add("Loại vé", 92);
            soldList.Columns.Add("Tổng", 86);
            soldList.Columns.Add("Bán lúc", 98);
            soldList.Dock = DockStyle.Fill;
            soldList.ForeColor = Color.FromArgb(35, 45, 60);
            soldList.FullRowSelect = true;
            soldList.HideSelection = false;
            soldList.MultiSelect = false;
            soldList.ShowItemToolTips = true;
            soldList.View = View.Details;
            soldList.SelectedIndexChanged += soldList_SelectedIndexChanged;
            soldList.DoubleClick += soldList_DoubleClick;

            // soldInfo
            soldInfo.Dock = DockStyle.Bottom;
            soldInfo.ForeColor = Color.FromArgb(90, 100, 115);
            soldInfo.Height = 56;
            soldInfo.Padding = new Padding(2, 4, 2, 4);

            // refundButton
            refundButton.AutoSize = true;
            refundButton.BackColor = Color.Firebrick;
            refundButton.Cursor = Cursors.Hand;
            refundButton.Enabled = false;
            refundButton.FlatStyle = FlatStyle.Flat;
            refundButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refundButton.ForeColor = Color.White;
            refundButton.Margin = new Padding(4);
            refundButton.Padding = new Padding(10, 6, 10, 6);
            refundButton.Text = "Trả vé đã chọn";
            refundButton.UseVisualStyleBackColor = false;
            refundButton.Click += refundButton_Click;

            // printButton
            printButton.AutoSize = true;
            printButton.BackColor = Color.FromArgb(90, 100, 115);
            printButton.Cursor = Cursors.Hand;
            printButton.FlatStyle = FlatStyle.Flat;
            printButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            printButton.ForeColor = Color.White;
            printButton.Margin = new Padding(4);
            printButton.Padding = new Padding(10, 6, 10, 6);
            printButton.Text = "In lại vé";
            printButton.UseVisualStyleBackColor = false;
            printButton.Click += printButton_Click;

            // actionsPanel
            actionsPanel.BackColor = Color.FromArgb(245, 247, 250);
            actionsPanel.Controls.Add(refundButton);
            actionsPanel.Controls.Add(printButton);
            actionsPanel.Dock = DockStyle.Bottom;
            actionsPanel.Height = 84;
            actionsPanel.WrapContents = true;

            // rightPanel
            rightPanel.BackColor = Color.FromArgb(245, 247, 250);
            rightPanel.Controls.Add(soldList);
            rightPanel.Controls.Add(soldInfo);
            rightPanel.Controls.Add(actionsPanel);
            rightPanel.Controls.Add(soldTitle);
            rightPanel.Dock = DockStyle.Right;
            rightPanel.Padding = new Padding(8);
            rightPanel.Width = 340;

            // status
            status.AutoSize = true;
            status.ForeColor = Color.FromArgb(35, 45, 60);
            status.Margin = new Padding(10, 10, 18, 0);

            // cart
            cart.AutoSize = true;
            cart.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            cart.ForeColor = Color.FromArgb(35, 110, 220);
            cart.Margin = new Padding(4, 7, 10, 0);

            // legend
            legend.AutoSize = true;
            legend.ForeColor = Color.FromArgb(35, 45, 60);
            legend.Margin = new Padding(8, 10, 0, 0);
            legend.TextAlign = ContentAlignment.MiddleLeft;

            // bottomPanel
            bottomPanel.AutoSize = true;
            bottomPanel.BackColor = Color.FromArgb(245, 247, 250);
            bottomPanel.Controls.Add(status);
            bottomPanel.Controls.Add(cart);
            bottomPanel.Controls.Add(legend);
            bottomPanel.Dock = DockStyle.Bottom;

            // seats
            seats.AutoSize = true;
            seats.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            seats.BackColor = Color.White;
            seats.FlowDirection = FlowDirection.TopDown;
            seats.Location = new Point(0, 0);
            seats.WrapContents = false;

            // seatHost
            seatHost.AutoScroll = true;
            seatHost.BackColor = Color.White;
            seatHost.Controls.Add(seats);
            seatHost.Dock = DockStyle.Fill;
            seatHost.Padding = new Padding(10);

            // refreshTimer
            refreshTimer.Interval = 20000;
            refreshTimer.Tick += refreshTimer_Tick;

            // TicketForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1134, 641);
            Controls.Add(seatHost);
            Controls.Add(rightPanel);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(1050, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán vé";
            FormClosed += TicketForm_FormClosed;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            screeningGroup.ResumeLayout(false);
            screeningGroup.PerformLayout();
            typePanel.ResumeLayout(false);
            typePanel.PerformLayout();
            snackGroup.ResumeLayout(false);
            snackGroup.PerformLayout();
            rightPanel.ResumeLayout(false);
            actionsPanel.ResumeLayout(false);
            actionsPanel.PerformLayout();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            seatHost.ResumeLayout(false);
            seatHost.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
