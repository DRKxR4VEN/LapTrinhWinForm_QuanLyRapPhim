#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class ScreeningForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel layout;
        private Panel leftPanel;
        private Label leftTitle;
        private Label leftHint;
        private Panel treeHost;
        private TreeView tree;
        private Panel rightPanel;
        private Panel card;
        private FlowLayoutPanel formPanel;
        private Label formTitle;
        private Panel movieField;
        private Label movieLabel;
        private ComboBox movie;
        private Panel roomField;
        private Label roomLabel;
        private ComboBox room;
        private Panel dateField;
        private Label dateLabel;
        private DateTimePicker startDate;
        private DateTimePicker startTime;
        private Label movieInfo;
        private Label endInfo;
        private FlowLayoutPanel buttonsFlow;
        private Button addButton;
        private Button cancelButton;
        private Button refreshButton;
        private Panel separator;
        private Label selectionTitle;
        private Label selectionInfo;
        private Label hint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            layout = new TableLayoutPanel();
            leftPanel = new Panel();
            treeHost = new Panel();
            tree = new TreeView();
            leftHint = new Label();
            leftTitle = new Label();
            rightPanel = new Panel();
            card = new Panel();
            formPanel = new FlowLayoutPanel();
            formTitle = new Label();
            movieField = new Panel();
            movieLabel = new Label();
            movie = new ComboBox();
            roomField = new Panel();
            roomLabel = new Label();
            room = new ComboBox();
            dateField = new Panel();
            dateLabel = new Label();
            startDate = new DateTimePicker();
            startTime = new DateTimePicker();
            movieInfo = new Label();
            endInfo = new Label();
            buttonsFlow = new FlowLayoutPanel();
            addButton = new Button();
            cancelButton = new Button();
            refreshButton = new Button();
            separator = new Panel();
            selectionTitle = new Label();
            selectionInfo = new Label();
            hint = new Label();
            layout.SuspendLayout();
            leftPanel.SuspendLayout();
            treeHost.SuspendLayout();
            rightPanel.SuspendLayout();
            card.SuspendLayout();
            formPanel.SuspendLayout();
            movieField.SuspendLayout();
            roomField.SuspendLayout();
            dateField.SuspendLayout();
            buttonsFlow.SuspendLayout();
            SuspendLayout();

            // leftTitle
            leftTitle.Dock = DockStyle.Top;
            leftTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            leftTitle.ForeColor = Color.FromArgb(35, 45, 60);
            leftTitle.Height = 32;
            leftTitle.Text = "LỊCH CHIẾU";

            // leftHint
            leftHint.Dock = DockStyle.Bottom;
            leftHint.ForeColor = Color.FromArgb(90, 100, 115);
            leftHint.Height = 44;
            leftHint.Text = "Cây lịch: Ngày → Phim → Suất. Chọn đúng dòng suất chiếu để xem chi tiết hoặc hủy.";
            leftHint.TextAlign = ContentAlignment.MiddleLeft;

            // tree
            tree.BackColor = Color.White;
            tree.BorderStyle = BorderStyle.FixedSingle;
            tree.Dock = DockStyle.Fill;
            tree.ForeColor = Color.FromArgb(35, 45, 60);
            tree.FullRowSelect = true;
            tree.HideSelection = false;
            tree.ItemHeight = 24;
            tree.ShowNodeToolTips = true;
            tree.AfterSelect += tree_AfterSelect;

            // treeHost
            treeHost.Controls.Add(tree);
            treeHost.Dock = DockStyle.Fill;
            treeHost.Padding = new Padding(0, 4, 0, 4);

            // leftPanel
            leftPanel.BackColor = Color.FromArgb(245, 247, 250);
            leftPanel.Controls.Add(treeHost);
            leftPanel.Controls.Add(leftHint);
            leftPanel.Controls.Add(leftTitle);
            leftPanel.Dock = DockStyle.Fill;
            leftPanel.Padding = new Padding(8);

            // formTitle
            formTitle.AutoSize = true;
            formTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            formTitle.ForeColor = Color.FromArgb(35, 45, 60);
            formTitle.Margin = new Padding(0, 0, 0, 8);
            formTitle.Text = "LÊN LỊCH SUẤT CHIẾU";

            // movieLabel
            movieLabel.AutoSize = true;
            movieLabel.ForeColor = Color.FromArgb(90, 100, 115);
            movieLabel.Location = new Point(0, 0);
            movieLabel.Text = "Phim";

            // movie
            movie.DropDownStyle = ComboBoxStyle.DropDownList;
            movie.Location = new Point(0, 22);
            movie.Size = new Size(360, 23);
            movie.SelectedIndexChanged += movie_SelectedIndexChanged;

            // movieField
            movieField.BackColor = Color.White;
            movieField.Controls.Add(movieLabel);
            movieField.Controls.Add(movie);
            movieField.Margin = new Padding(0, 0, 0, 2);
            movieField.Size = new Size(380, 58);

            // roomLabel
            roomLabel.AutoSize = true;
            roomLabel.ForeColor = Color.FromArgb(90, 100, 115);
            roomLabel.Location = new Point(0, 0);
            roomLabel.Text = "Phòng chiếu";

            // room
            room.DropDownStyle = ComboBoxStyle.DropDownList;
            room.Location = new Point(0, 22);
            room.Size = new Size(360, 23);
            room.SelectedIndexChanged += room_SelectedIndexChanged;

            // roomField
            roomField.BackColor = Color.White;
            roomField.Controls.Add(roomLabel);
            roomField.Controls.Add(room);
            roomField.Margin = new Padding(0, 0, 0, 2);
            roomField.Size = new Size(380, 58);

            // dateLabel
            dateLabel.AutoSize = true;
            dateLabel.ForeColor = Color.FromArgb(90, 100, 115);
            dateLabel.Location = new Point(0, 0);
            dateLabel.Text = "Ngày và giờ bắt đầu";

            // startDate
            startDate.CustomFormat = "dd/MM/yyyy";
            startDate.Format = DateTimePickerFormat.Custom;
            startDate.Location = new Point(0, 22);
            startDate.Size = new Size(225, 23);
            startDate.ValueChanged += startDate_ValueChanged;

            // startTime
            startTime.CustomFormat = "HH:mm";
            startTime.Format = DateTimePickerFormat.Custom;
            startTime.Location = new Point(235, 22);
            startTime.ShowUpDown = true;
            startTime.Size = new Size(125, 23);
            startTime.ValueChanged += startTime_ValueChanged;

            // dateField
            dateField.BackColor = Color.White;
            dateField.Controls.Add(dateLabel);
            dateField.Controls.Add(startDate);
            dateField.Controls.Add(startTime);
            dateField.Margin = new Padding(0, 0, 0, 2);
            dateField.Size = new Size(380, 58);

            // movieInfo
            movieInfo.AutoSize = true;
            movieInfo.ForeColor = Color.FromArgb(90, 100, 115);
            movieInfo.Margin = new Padding(0, 0, 0, 6);
            movieInfo.Text = "Chọn phim để xem thời lượng.";

            // endInfo
            endInfo.AutoSize = true;
            endInfo.ForeColor = Color.FromArgb(35, 110, 220);
            endInfo.Margin = new Padding(0, 0, 0, 8);
            endInfo.MaximumSize = new Size(380, 0);

            // addButton
            addButton.AutoSize = true;
            addButton.BackColor = Color.FromArgb(35, 110, 220);
            addButton.Cursor = Cursors.Hand;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Margin = new Padding(4);
            addButton.Padding = new Padding(10, 6, 10, 6);
            addButton.Text = "Thêm suất chiếu";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;

            // cancelButton
            cancelButton.AutoSize = true;
            cancelButton.BackColor = Color.Firebrick;
            cancelButton.Cursor = Cursors.Hand;
            cancelButton.Enabled = false;
            cancelButton.FlatStyle = FlatStyle.Flat;
            cancelButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Margin = new Padding(4);
            cancelButton.Padding = new Padding(10, 6, 10, 6);
            cancelButton.Text = "Hủy suất đã chọn";
            cancelButton.UseVisualStyleBackColor = false;
            cancelButton.Click += cancelButton_Click;

            // refreshButton
            refreshButton.AutoSize = true;
            refreshButton.BackColor = Color.FromArgb(90, 100, 115);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.Margin = new Padding(4);
            refreshButton.Padding = new Padding(10, 6, 10, 6);
            refreshButton.Text = "Làm mới";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click;

            // buttonsFlow
            buttonsFlow.BackColor = Color.White;
            buttonsFlow.Controls.Add(addButton);
            buttonsFlow.Controls.Add(cancelButton);
            buttonsFlow.Controls.Add(refreshButton);
            buttonsFlow.Height = 104;
            buttonsFlow.Margin = new Padding(0, 0, 0, 6);
            buttonsFlow.WrapContents = true;
            buttonsFlow.Width = 380;

            // separator
            separator.BackColor = Color.FromArgb(220, 225, 232);
            separator.Margin = new Padding(0, 6, 0, 8);
            separator.Size = new Size(60, 1);

            // selectionTitle
            selectionTitle.AutoSize = true;
            selectionTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            selectionTitle.ForeColor = Color.FromArgb(35, 45, 60);
            selectionTitle.Margin = new Padding(0, 0, 0, 2);
            selectionTitle.Text = "THÔNG TIN SUẤT ĐANG CHỌN";

            // selectionInfo
            selectionInfo.AutoSize = true;
            selectionInfo.BackColor = Color.FromArgb(245, 247, 250);
            selectionInfo.BorderStyle = BorderStyle.FixedSingle;
            selectionInfo.ForeColor = Color.FromArgb(35, 45, 60);
            selectionInfo.Margin = new Padding(0, 8, 0, 0);
            selectionInfo.MaximumSize = new Size(380, 0);
            selectionInfo.Padding = new Padding(10);
            selectionInfo.Text = "Chưa chọn suất chiếu nào.";

            // hint
            hint.AutoSize = true;
            hint.ForeColor = Color.FromArgb(90, 100, 115);
            hint.Margin = new Padding(0, 4, 0, 0);
            hint.MaximumSize = new Size(380, 0);
            hint.Text = "Mỗi suất chiếu được cộng thêm 15 phút dọn phòng khi kiểm tra trùng lịch.";

            // formPanel
            formPanel.AutoScroll = true;
            formPanel.BackColor = Color.White;
            formPanel.Controls.Add(formTitle);
            formPanel.Controls.Add(movieField);
            formPanel.Controls.Add(roomField);
            formPanel.Controls.Add(dateField);
            formPanel.Controls.Add(movieInfo);
            formPanel.Controls.Add(endInfo);
            formPanel.Controls.Add(buttonsFlow);
            formPanel.Controls.Add(separator);
            formPanel.Controls.Add(selectionTitle);
            formPanel.Controls.Add(selectionInfo);
            formPanel.Controls.Add(hint);
            formPanel.Dock = DockStyle.Fill;
            formPanel.FlowDirection = FlowDirection.TopDown;
            formPanel.WrapContents = false;

            // card
            card.AutoScroll = true;
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Controls.Add(formPanel);
            card.Dock = DockStyle.Fill;
            card.Padding = new Padding(16, 12, 16, 12);

            // rightPanel
            rightPanel.BackColor = Color.FromArgb(245, 247, 250);
            rightPanel.Controls.Add(card);
            rightPanel.Dock = DockStyle.Fill;
            rightPanel.Padding = new Padding(8);

            // layout
            layout.BackColor = Color.FromArgb(245, 247, 250);
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            layout.Controls.Add(leftPanel, 0, 0);
            layout.Controls.Add(rightPanel, 1, 0);
            layout.Dock = DockStyle.Fill;
            layout.Padding = new Padding(8);
            layout.RowCount = 1;
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // ScreeningForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1084, 641);
            Controls.Add(layout);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý suất chiếu";
            layout.ResumeLayout(false);
            leftPanel.ResumeLayout(false);
            treeHost.ResumeLayout(false);
            rightPanel.ResumeLayout(false);
            card.ResumeLayout(false);
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            movieField.ResumeLayout(false);
            movieField.PerformLayout();
            roomField.ResumeLayout(false);
            roomField.PerformLayout();
            dateField.ResumeLayout(false);
            dateField.PerformLayout();
            buttonsFlow.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
