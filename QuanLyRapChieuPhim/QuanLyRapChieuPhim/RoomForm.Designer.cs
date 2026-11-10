#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class RoomForm
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel top;
        private FlowLayoutPanel fieldsFlow;
        private FlowLayoutPanel actionFlow;
        private Panel fieldId;
        private Panel fieldName;
        private Panel fieldRows;
        private Panel fieldSeats;
        private Panel fieldVip;
        private Label lblId;
        private Label lblName;
        private Label lblRows;
        private Label lblSeats;
        private Label lblVip;
        private TextBox id;
        private TextBox name;
        private TextBox rows;
        private TextBox seats;
        private TextBox vip;
        private DataGridView grid;
        private Button addButton;
        private Button editButton;
        private Button deleteButton;

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
            fieldName = new Panel();
            lblName = new Label();
            name = new TextBox();
            fieldRows = new Panel();
            lblRows = new Label();
            rows = new TextBox();
            fieldSeats = new Panel();
            lblSeats = new Label();
            seats = new TextBox();
            fieldVip = new Panel();
            lblVip = new Label();
            vip = new TextBox();
            actionFlow = new FlowLayoutPanel();
            addButton = new Button();
            editButton = new Button();
            deleteButton = new Button();
            grid = new DataGridView();
            var gridCellStyle = new DataGridViewCellStyle();
            var gridHeaderStyle = new DataGridViewCellStyle();
            top.SuspendLayout();
            fieldsFlow.SuspendLayout();
            fieldId.SuspendLayout();
            fieldName.SuspendLayout();
            fieldRows.SuspendLayout();
            fieldSeats.SuspendLayout();
            fieldVip.SuspendLayout();
            actionFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)grid).BeginInit();
            SuspendLayout();

            // lblId
            lblId.AutoSize = true;
            lblId.ForeColor = Color.FromArgb(90, 100, 115);
            lblId.Location = new Point(0, 0);
            lblId.Text = "Mã phòng";

            // id
            id.BackColor = Color.White;
            id.ForeColor = Color.FromArgb(35, 45, 60);
            id.Location = new Point(0, 22);
            id.Size = new Size(102, 23);

            // fieldId
            fieldId.BackColor = Color.FromArgb(245, 247, 250);
            fieldId.Controls.Add(lblId);
            fieldId.Controls.Add(id);
            fieldId.Margin = new Padding(0, 0, 10, 4);
            fieldId.Size = new Size(110, 58);

            // lblName
            lblName.AutoSize = true;
            lblName.ForeColor = Color.FromArgb(90, 100, 115);
            lblName.Location = new Point(0, 0);
            lblName.Text = "Tên phòng";

            // name
            name.BackColor = Color.White;
            name.ForeColor = Color.FromArgb(35, 45, 60);
            name.Location = new Point(0, 22);
            name.Size = new Size(192, 23);

            // fieldName
            fieldName.BackColor = Color.FromArgb(245, 247, 250);
            fieldName.Controls.Add(lblName);
            fieldName.Controls.Add(name);
            fieldName.Margin = new Padding(0, 0, 10, 4);
            fieldName.Size = new Size(200, 58);

            // lblRows
            lblRows.AutoSize = true;
            lblRows.ForeColor = Color.FromArgb(90, 100, 115);
            lblRows.Location = new Point(0, 0);
            lblRows.Text = "Số hàng";

            // rows
            rows.BackColor = Color.White;
            rows.ForeColor = Color.FromArgb(35, 45, 60);
            rows.Location = new Point(0, 22);
            rows.Size = new Size(92, 23);

            // fieldRows
            fieldRows.BackColor = Color.FromArgb(245, 247, 250);
            fieldRows.Controls.Add(lblRows);
            fieldRows.Controls.Add(rows);
            fieldRows.Margin = new Padding(0, 0, 10, 4);
            fieldRows.Size = new Size(100, 58);

            // lblSeats
            lblSeats.AutoSize = true;
            lblSeats.ForeColor = Color.FromArgb(90, 100, 115);
            lblSeats.Location = new Point(0, 0);
            lblSeats.Text = "Ghế / hàng";

            // seats
            seats.BackColor = Color.White;
            seats.ForeColor = Color.FromArgb(35, 45, 60);
            seats.Location = new Point(0, 22);
            seats.Size = new Size(102, 23);

            // fieldSeats
            fieldSeats.BackColor = Color.FromArgb(245, 247, 250);
            fieldSeats.Controls.Add(lblSeats);
            fieldSeats.Controls.Add(seats);
            fieldSeats.Margin = new Padding(0, 0, 10, 4);
            fieldSeats.Size = new Size(110, 58);

            // lblVip
            lblVip.AutoSize = true;
            lblVip.ForeColor = Color.FromArgb(90, 100, 115);
            lblVip.Location = new Point(0, 0);
            lblVip.Text = "Hàng VIP";

            // vip
            vip.BackColor = Color.White;
            vip.ForeColor = Color.FromArgb(35, 45, 60);
            vip.Location = new Point(0, 22);
            vip.Size = new Size(92, 23);

            // fieldVip
            fieldVip.BackColor = Color.FromArgb(245, 247, 250);
            fieldVip.Controls.Add(lblVip);
            fieldVip.Controls.Add(vip);
            fieldVip.Margin = new Padding(0, 0, 10, 4);
            fieldVip.Size = new Size(100, 58);

            // fieldsFlow
            fieldsFlow.AutoSize = true;
            fieldsFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            fieldsFlow.BackColor = Color.FromArgb(245, 247, 250);
            fieldsFlow.Controls.Add(fieldId);
            fieldsFlow.Controls.Add(fieldName);
            fieldsFlow.Controls.Add(fieldRows);
            fieldsFlow.Controls.Add(fieldSeats);
            fieldsFlow.Controls.Add(fieldVip);
            fieldsFlow.Margin = new Padding(0);
            fieldsFlow.WrapContents = true;

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

            // actionFlow
            actionFlow.AutoSize = true;
            actionFlow.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            actionFlow.BackColor = Color.FromArgb(245, 247, 250);
            actionFlow.Controls.Add(addButton);
            actionFlow.Controls.Add(editButton);
            actionFlow.Controls.Add(deleteButton);
            actionFlow.Margin = new Padding(0, 6, 0, 0);
            actionFlow.WrapContents = true;

            // top
            top.AutoSize = true;
            top.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            top.BackColor = Color.FromArgb(245, 247, 250);
            top.ColumnCount = 1;
            top.ColumnStyles.Add(new ColumnStyle());
            top.Controls.Add(fieldsFlow, 0, 0);
            top.Controls.Add(actionFlow, 0, 1);
            top.Dock = DockStyle.Top;
            top.Padding = new Padding(10);
            top.RowCount = 2;
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

            // RoomForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(884, 521);
            Controls.Add(grid);
            Controls.Add(top);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(760, 450);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý phòng chiếu";
            top.ResumeLayout(false);
            top.PerformLayout();
            fieldsFlow.ResumeLayout(false);
            fieldId.ResumeLayout(false);
            fieldId.PerformLayout();
            fieldName.ResumeLayout(false);
            fieldName.PerformLayout();
            fieldRows.ResumeLayout(false);
            fieldRows.PerformLayout();
            fieldSeats.ResumeLayout(false);
            fieldSeats.PerformLayout();
            fieldVip.ResumeLayout(false);
            fieldVip.PerformLayout();
            actionFlow.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
