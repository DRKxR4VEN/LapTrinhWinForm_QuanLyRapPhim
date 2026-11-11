#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class MovieForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel top;
        private FlowLayoutPanel fieldsFlow;
        private FlowLayoutPanel posterFlow;
        private FlowLayoutPanel actionFlow;
        private Panel fieldId;
        private Panel fieldTitle;
        private Panel fieldGenre;
        private Panel fieldDuration;
        private Panel posterField;
        private Panel content;
        private Panel previewPanel;
        private Panel listPanel;
        private Label lblId;
        private Label lblTitle;
        private Label lblGenre;
        private Label lblDuration;
        private Label lblPoster;
        private Label previewTitle;
        private Label listTitle;
        private TextBox id;
        private TextBox title;
        private TextBox genre;
        private TextBox duration;
        private TextBox poster;
        private PictureBox preview;
        private ListView list;
        private ImageList posterImages;
        private DataGridView grid;
        private Button pickPosterButton;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;
        private Button importButton;
        private Button filterButton;
        private Button refreshButton;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            top = new TableLayoutPanel();
            fieldsFlow = new FlowLayoutPanel();
            fieldId = new Panel();
            lblId = new Label();
            id = new TextBox();
            fieldTitle = new Panel();
            lblTitle = new Label();
            title = new TextBox();
            fieldGenre = new Panel();
            lblGenre = new Label();
            genre = new TextBox();
            fieldDuration = new Panel();
            lblDuration = new Label();
            duration = new TextBox();
            posterFlow = new FlowLayoutPanel();
            posterField = new Panel();
            lblPoster = new Label();
            poster = new TextBox();
            pickPosterButton = new Button();
            actionFlow = new FlowLayoutPanel();
            addButton = new Button();
            editButton = new Button();
            deleteButton = new Button();
            importButton = new Button();
            filterButton = new Button();
            refreshButton = new Button();
            content = new Panel();
            grid = new DataGridView();
            listPanel = new Panel();
            list = new ListView();
            posterImages = new ImageList(components);
            listTitle = new Label();
            previewPanel = new Panel();
            preview = new PictureBox();
            previewTitle = new Label();
            var gridCellStyle = new DataGridViewCellStyle();
            var gridHeaderStyle = new DataGridViewCellStyle();
            top.SuspendLayout();
            fieldsFlow.SuspendLayout();
            fieldId.SuspendLayout();
            fieldTitle.SuspendLayout();
            fieldGenre.SuspendLayout();
            fieldDuration.SuspendLayout();
            posterFlow.SuspendLayout();
            posterField.SuspendLayout();
            actionFlow.SuspendLayout();
            content.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            listPanel.SuspendLayout();
            previewPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)preview).BeginInit();
            SuspendLayout();

            // lblId
            lblId.AutoSize = true;
            lblId.ForeColor = Color.FromArgb(90, 100, 115);
            lblId.Location = new Point(0, 0);
            lblId.Text = "Mã phim";

            // id
            id.BackColor = Color.White;
            id.ForeColor = Color.FromArgb(35, 45, 60);
            id.Location = new Point(0, 22);
            id.Size = new Size(112, 23);

            // fieldId
            fieldId.BackColor = Color.FromArgb(245, 247, 250);
            fieldId.Controls.Add(lblId);
            fieldId.Controls.Add(id);
            fieldId.Margin = new Padding(0, 0, 10, 4);
            fieldId.Size = new Size(120, 58);

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.FromArgb(90, 100, 115);
            lblTitle.Location = new Point(0, 0);
            lblTitle.Text = "Tên phim";

            // title
            title.BackColor = Color.White;
            title.ForeColor = Color.FromArgb(35, 45, 60);
            title.Location = new Point(0, 22);
            title.Size = new Size(212, 23);

            // fieldTitle
            fieldTitle.BackColor = Color.FromArgb(245, 247, 250);
            fieldTitle.Controls.Add(lblTitle);
            fieldTitle.Controls.Add(title);
            fieldTitle.Margin = new Padding(0, 0, 10, 4);
            fieldTitle.Size = new Size(220, 58);

            // lblGenre
            lblGenre.AutoSize = true;
            lblGenre.ForeColor = Color.FromArgb(90, 100, 115);
            lblGenre.Location = new Point(0, 0);
            lblGenre.Text = "Thể loại";

            // genre
            genre.BackColor = Color.White;
            genre.ForeColor = Color.FromArgb(35, 45, 60);
            genre.Location = new Point(0, 22);
            genre.Size = new Size(152, 23);

            // fieldGenre
            fieldGenre.BackColor = Color.FromArgb(245, 247, 250);
            fieldGenre.Controls.Add(lblGenre);
            fieldGenre.Controls.Add(genre);
            fieldGenre.Margin = new Padding(0, 0, 10, 4);
            fieldGenre.Size = new Size(160, 58);

            // lblDuration
            lblDuration.AutoSize = true;
            lblDuration.ForeColor = Color.FromArgb(90, 100, 115);
            lblDuration.Location = new Point(0, 0);
            lblDuration.Text = "Thời lượng (phút)";

            // duration
            duration.BackColor = Color.White;
            duration.ForeColor = Color.FromArgb(35, 45, 60);
            duration.Location = new Point(0, 22);
            duration.Size = new Size(127, 23);

            // fieldDuration
            fieldDuration.BackColor = Color.FromArgb(245, 247, 250);
            fieldDuration.Controls.Add(lblDuration);
            fieldDuration.Controls.Add(duration);
            fieldDuration.Margin = new Padding(0, 0, 10, 4);
            fieldDuration.Size = new Size(135, 58);

            // fieldsFlow
            fieldsFlow.AutoSize = true;
            fieldsFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fieldsFlow.BackColor = Color.FromArgb(245, 247, 250);
            fieldsFlow.Controls.Add(fieldId);
            fieldsFlow.Controls.Add(fieldTitle);
            fieldsFlow.Controls.Add(fieldGenre);
            fieldsFlow.Controls.Add(fieldDuration);
            fieldsFlow.Margin = new Padding(0);
            fieldsFlow.WrapContents = true;

            // lblPoster
            lblPoster.AutoSize = true;
            lblPoster.ForeColor = Color.FromArgb(90, 100, 115);
            lblPoster.Location = new Point(0, 0);
            lblPoster.Text = "Đường dẫn poster";

            // poster
            poster.BackColor = Color.White;
            poster.ForeColor = Color.FromArgb(35, 45, 60);
            poster.Location = new Point(0, 22);
            poster.Size = new Size(552, 23);

            // posterField
            posterField.BackColor = Color.FromArgb(245, 247, 250);
            posterField.Controls.Add(lblPoster);
            posterField.Controls.Add(poster);
            posterField.Margin = new Padding(0, 0, 10, 4);
            posterField.Size = new Size(560, 58);

            // pickPosterButton
            pickPosterButton.AutoSize = true;
            pickPosterButton.BackColor = Color.FromArgb(35, 110, 220);
            pickPosterButton.Cursor = Cursors.Hand;
            pickPosterButton.FlatStyle = FlatStyle.Flat;
            pickPosterButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            pickPosterButton.ForeColor = Color.White;
            pickPosterButton.Margin = new Padding(6, 22, 0, 0);
            pickPosterButton.Padding = new Padding(10, 6, 10, 6);
            pickPosterButton.Text = "Chọn poster";
            pickPosterButton.UseVisualStyleBackColor = false;
            pickPosterButton.Click += pickPosterButton_Click;

            // posterFlow
            posterFlow.AutoSize = true;
            posterFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            posterFlow.BackColor = Color.FromArgb(245, 247, 250);
            posterFlow.Controls.Add(posterField);
            posterFlow.Controls.Add(pickPosterButton);
            posterFlow.Margin = new Padding(0);
            posterFlow.WrapContents = false;

            // addButton
            addButton.AutoSize = true;
            addButton.BackColor = Color.FromArgb(35, 110, 220);
            addButton.Cursor = Cursors.Hand;
            addButton.FlatStyle = FlatStyle.Flat;
            addButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            addButton.ForeColor = Color.White;
            addButton.Margin = new Padding(4);
            addButton.Padding = new Padding(10, 6, 10, 6);
            addButton.Text = "Thêm";
            addButton.UseVisualStyleBackColor = false;
            addButton.Click += addButton_Click;

            // editButton
            editButton.AutoSize = true;
            editButton.BackColor = Color.FromArgb(35, 110, 220);
            editButton.Cursor = Cursors.Hand;
            editButton.FlatStyle = FlatStyle.Flat;
            editButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            editButton.ForeColor = Color.White;
            editButton.Margin = new Padding(4);
            editButton.Padding = new Padding(10, 6, 10, 6);
            editButton.Text = "Sửa";
            editButton.UseVisualStyleBackColor = false;
            editButton.Click += editButton_Click;

            // deleteButton
            deleteButton.AutoSize = true;
            deleteButton.BackColor = Color.FromArgb(35, 110, 220);
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            deleteButton.ForeColor = Color.White;
            deleteButton.Margin = new Padding(4);
            deleteButton.Padding = new Padding(10, 6, 10, 6);
            deleteButton.Text = "Xóa";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;

            // importButton
            importButton.AutoSize = true;
            importButton.BackColor = Color.FromArgb(35, 110, 220);
            importButton.Cursor = Cursors.Hand;
            importButton.FlatStyle = FlatStyle.Flat;
            importButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            importButton.ForeColor = Color.White;
            importButton.Margin = new Padding(4);
            importButton.Padding = new Padding(10, 6, 10, 6);
            importButton.Text = "Nhập CSV";
            importButton.UseVisualStyleBackColor = false;
            importButton.Click += importButton_Click;

            // filterButton
            filterButton.AutoSize = true;
            filterButton.BackColor = Color.FromArgb(35, 110, 220);
            filterButton.Cursor = Cursors.Hand;
            filterButton.FlatStyle = FlatStyle.Flat;
            filterButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            filterButton.ForeColor = Color.White;
            filterButton.Margin = new Padding(4);
            filterButton.Padding = new Padding(10, 6, 10, 6);
            filterButton.Text = "Lọc thể loại";
            filterButton.UseVisualStyleBackColor = false;
            filterButton.Click += filterButton_Click;

            // refreshButton
            refreshButton.AutoSize = true;
            refreshButton.BackColor = Color.FromArgb(35, 110, 220);
            refreshButton.Cursor = Cursors.Hand;
            refreshButton.FlatStyle = FlatStyle.Flat;
            refreshButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            refreshButton.ForeColor = Color.White;
            refreshButton.Margin = new Padding(4);
            refreshButton.Padding = new Padding(10, 6, 10, 6);
            refreshButton.Text = "Làm mới";
            refreshButton.UseVisualStyleBackColor = false;
            refreshButton.Click += refreshButton_Click;

            // actionFlow
            actionFlow.AutoSize = true;
            actionFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            actionFlow.BackColor = Color.FromArgb(245, 247, 250);
            actionFlow.Controls.Add(addButton);
            actionFlow.Controls.Add(editButton);
            actionFlow.Controls.Add(deleteButton);
            actionFlow.Controls.Add(importButton);
            actionFlow.Controls.Add(filterButton);
            actionFlow.Controls.Add(refreshButton);
            actionFlow.Margin = new Padding(0, 6, 0, 0);
            actionFlow.WrapContents = true;

            // top
            top.AutoSize = true;
            top.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            top.BackColor = Color.FromArgb(245, 247, 250);
            top.ColumnCount = 1;
            top.ColumnStyles.Add(new ColumnStyle());
            top.Controls.Add(fieldsFlow, 0, 0);
            top.Controls.Add(posterFlow, 0, 1);
            top.Controls.Add(actionFlow, 0, 2);
            top.Dock = DockStyle.Top;
            top.Padding = new Padding(10);
            top.RowCount = 3;
            top.RowStyles.Add(new RowStyle());
            top.RowStyles.Add(new RowStyle());
            top.RowStyles.Add(new RowStyle());

            // grid
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.FixedSingle;
            gridCellStyle.BackColor = Color.White;
            gridCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
            gridCellStyle.Padding = new Padding(4);
            gridCellStyle.SelectionBackColor = Color.FromArgb(205, 225, 250);
            gridCellStyle.SelectionForeColor = Color.FromArgb(35, 45, 60);
            grid.DefaultCellStyle = gridCellStyle;
            grid.ColumnHeadersHeight = 32;
            grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            gridHeaderStyle.BackColor = Color.FromArgb(230, 238, 249);
            gridHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            gridHeaderStyle.ForeColor = Color.FromArgb(35, 45, 60);
            gridHeaderStyle.Padding = new Padding(5);
            grid.ColumnHeadersDefaultCellStyle = gridHeaderStyle;
            grid.Dock = DockStyle.Fill;
            grid.EnableHeadersVisualStyles = false;
            grid.GridColor = Color.FromArgb(225, 229, 235);
            grid.MultiSelect = false;
            grid.ReadOnly = true;
            grid.RowHeadersVisible = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.SelectionChanged += grid_SelectionChanged;

            // listTitle
            listTitle.Dock = DockStyle.Top;
            listTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            listTitle.ForeColor = Color.FromArgb(35, 45, 60);
            listTitle.Height = 24;
            listTitle.Text = "DANH SÁCH PHIM";

            // posterImages
            posterImages.ColorDepth = ColorDepth.Depth32Bit;
            posterImages.ImageSize = new Size(64, 80);

            // list
            list.BackColor = Color.White;
            list.BorderStyle = BorderStyle.None;
            list.Dock = DockStyle.Fill;
            list.ForeColor = Color.FromArgb(35, 45, 60);
            list.LargeImageList = posterImages;
            list.View = View.LargeIcon;
            list.ItemActivate += list_ItemActivate;

            // listPanel
            listPanel.BackColor = Color.White;
            listPanel.Controls.Add(list);
            listPanel.Controls.Add(listTitle);
            listPanel.Dock = DockStyle.Right;
            listPanel.Padding = new Padding(6);
            listPanel.Width = 240;

            // preview
            preview.BackColor = Color.White;
            preview.BorderStyle = BorderStyle.FixedSingle;
            preview.Dock = DockStyle.Top;
            preview.Size = new Size(120, 150);
            preview.SizeMode = PictureBoxSizeMode.Zoom;

            // previewTitle
            previewTitle.Dock = DockStyle.Top;
            previewTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            previewTitle.ForeColor = Color.FromArgb(35, 45, 60);
            previewTitle.Height = 24;
            previewTitle.Text = "POSTER";

            // previewPanel
            previewPanel.BackColor = Color.White;
            previewPanel.Controls.Add(preview);
            previewPanel.Controls.Add(previewTitle);
            previewPanel.Dock = DockStyle.Right;
            previewPanel.Padding = new Padding(6);
            previewPanel.Width = 150;

            // content
            content.BackColor = Color.White;
            content.Controls.Add(grid);
            content.Controls.Add(listPanel);
            content.Controls.Add(previewPanel);
            content.Dock = DockStyle.Fill;
            content.Padding = new Padding(10);

            // MovieForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1104, 681);
            Controls.Add(content);
            Controls.Add(top);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý phim";
            top.ResumeLayout(false);
            top.PerformLayout();
            fieldsFlow.ResumeLayout(false);
            fieldId.ResumeLayout(false);
            fieldId.PerformLayout();
            fieldTitle.ResumeLayout(false);
            fieldTitle.PerformLayout();
            fieldGenre.ResumeLayout(false);
            fieldGenre.PerformLayout();
            fieldDuration.ResumeLayout(false);
            fieldDuration.PerformLayout();
            posterFlow.ResumeLayout(false);
            posterField.ResumeLayout(false);
            posterField.PerformLayout();
            actionFlow.ResumeLayout(false);
            content.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            listPanel.ResumeLayout(false);
            previewPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)preview).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
