namespace QuanLyRapChieuPhim;

public static class Ui
{
    public static readonly Color MainBack = Color.FromArgb(245, 247, 250);
    public static readonly Color MainText = Color.FromArgb(35, 45, 60);
    public static readonly Color Accent = Color.FromArgb(35, 110, 220);

    public static DataGridView Grid()
    {
        return new DataGridView {
            Dock=DockStyle.Fill, ReadOnly=true, AllowUserToAddRows=false,
            AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill,
            SelectionMode=DataGridViewSelectionMode.FullRowSelect, MultiSelect=false,
            BackgroundColor=Color.White, BorderStyle=BorderStyle.FixedSingle,
            GridColor=Color.FromArgb(225,229,235), EnableHeadersVisualStyles=false,
            ColumnHeadersHeight=32, ColumnHeadersHeightSizeMode=DataGridViewColumnHeadersHeightSizeMode.DisableResizing,
            ColumnHeadersDefaultCellStyle={ BackColor=Color.FromArgb(230,238,249), ForeColor=MainText, Font=new Font("Segoe UI",9,FontStyle.Bold), Padding=new Padding(5) },
            DefaultCellStyle={ BackColor=Color.White, ForeColor=MainText, SelectionBackColor=Color.FromArgb(205,225,250), SelectionForeColor=MainText, Padding=new Padding(4) },
            RowHeadersVisible=false
        };
    }
    public static TextBox Box(int width=160)=>new(){Width=width,Font=new Font("Segoe UI",9),ForeColor=MainText,BackColor=Color.White};
    public static Button Btn(string text)=>new(){Text=text,AutoSize=true,Padding=new Padding(10,6,10,6),BackColor=Accent,ForeColor=Color.White,FlatStyle=FlatStyle.Flat,Margin=new Padding(4),Font=new Font("Segoe UI",9,FontStyle.Bold),Cursor=Cursors.Hand};
    public static Label Lab(string text)=>new(){Text=text,AutoSize=true,TextAlign=ContentAlignment.MiddleLeft,ForeColor=MainText,Margin=new Padding(5,8,3,3)};
    public static FlowLayoutPanel Flow()=>new(){Dock=DockStyle.Top,AutoSize=true,Padding=new Padding(10),WrapContents=true,BackColor=MainBack};
    public static void ApplyTheme(Control root)
    {
        root.BackColor=MainBack;
        root.ForeColor=MainText;
        root.Font=new Font("Segoe UI",9);
        foreach(Control c in root.Controls)
        {
            if(c is DataGridView) continue;
            if(c is Button b) { b.BackColor=Accent; b.ForeColor=Color.White; b.FlatStyle=FlatStyle.Flat; }
            else if(c is TextBox t) { t.BackColor=Color.White; t.ForeColor=MainText; }
            else if(c is Label l) l.ForeColor=MainText;
            else if(c is FlowLayoutPanel || c is Panel || c is TableLayoutPanel) c.BackColor=MainBack;
            ApplyTheme(c);
        }
    }
    public static void ShowError(Exception ex) { FileHelper.Log(ex); MessageBox.Show(ex.Message,"Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error); }
}
