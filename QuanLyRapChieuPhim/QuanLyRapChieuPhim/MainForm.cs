namespace QuanLyRapChieuPhim;

public class MainForm : Form
{
    readonly AppUser user;
    readonly (Color Back,Color Fore,float FontSize) appearance=AppData.LoadAppearance();
    public MainForm(AppUser currentUser)
    {
        user=currentUser;
        Text=$"QUẢN LÝ RẠP CHIẾU PHIM - {user.Username} ({user.Role})";
        Width=1200; Height=760; IsMdiContainer=true; StartPosition=FormStartPosition.CenterScreen;
        BackColor=Ui.MainBack; ForeColor=Ui.MainText; Font=new Font("Segoe UI",10);
        var menu=new MenuStrip { BackColor=Color.White, ForeColor=Ui.MainText, Font=new Font("Segoe UI",10), Padding=new Padding(8,5,0,5) };
        var isAdmin=user.Role=="QuanTri";
        var sys=new ToolStripMenuItem("Hệ thống");
        if(isAdmin)
        {
            sys.DropDownItems.Add(new ToolStripMenuItem("Sao lưu dữ liệu",null,(_,_)=>Backup()));
            sys.DropDownItems.Add(new ToolStripMenuItem("Khôi phục dữ liệu",null,(_,_)=>Restore()));
            sys.DropDownItems.Add(new ToolStripSeparator());
        }
        sys.DropDownItems.Add(new ToolStripMenuItem("Đổi giao diện",null,(_,_)=>Open(new SettingsForm())));
        sys.DropDownItems.Add(new ToolStripMenuItem("Thoát",null,(_,_)=>Close()));
        var manage=new ToolStripMenuItem("Quản lý");
        if(isAdmin)
        {
            manage.DropDownItems.Add(new ToolStripMenuItem("Phim",null,(_,_)=>Open(new MovieForm())));
            manage.DropDownItems.Add(new ToolStripMenuItem("Phòng chiếu",null,(_,_)=>Open(new RoomForm())));
            manage.DropDownItems.Add(new ToolStripMenuItem("Suất chiếu",null,(_,_)=>Open(new ScreeningForm())));
        }
        manage.DropDownItems.Add(new ToolStripMenuItem("Bán vé",null,(_,_)=>Open(new TicketForm())));
        if(isAdmin)
            manage.DropDownItems.Add(new ToolStripMenuItem("Doanh thu",null,(_,_)=>Open(new RevenueForm())));
        menu.Items.Add(sys); menu.Items.Add(manage); MainMenuStrip=menu; Controls.Add(menu);
        var mdi=Controls.OfType<MdiClient>().FirstOrDefault();
        if(mdi!=null) mdi.BackColor=Ui.MainBack;
        var welcome=new Panel { Dock=DockStyle.Fill, BackColor=Ui.MainBack, Padding=new Padding(30) };
        var title=new Label { Text="QUẢN LÝ RẠP CHIẾU PHIM", Dock=DockStyle.Top, Height=60, Font=new Font("Segoe UI",22,FontStyle.Bold), ForeColor=Ui.MainText, TextAlign=ContentAlignment.MiddleCenter };
        var sub=new Label { Text="Chọn chức năng trong menu “Quản lý” để bắt đầu", Dock=DockStyle.Top, Height=40, Font=new Font("Segoe UI",12), ForeColor=Color.FromArgb(90,100,115), TextAlign=ContentAlignment.TopCenter };
        welcome.Controls.Add(sub); welcome.Controls.Add(title);
        Controls.Add(welcome); welcome.BringToFront(); menu.BringToFront();
        // Keep the welcome panel behind child windows and away from the menu.
        MdiChildActivate += (_,_) => { if(ActiveMdiChild!=null) welcome.Visible=false; else welcome.Visible=true; };
        menu.BringToFront();
    }
    void Open(Form form)
    {
        if(user.Role!="QuanTri" && form is not TicketForm && form is not SettingsForm)
        {
            MessageBox.Show("Bạn không có quyền truy cập chức năng này.","Phân quyền",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            form.Dispose();
            return;
        }
        foreach(Form f in MdiChildren) if(f.GetType()==form.GetType()){f.Activate();form.Dispose();return;}
        form.MdiParent=this; form.Show();
    }
    void Backup()
    {
        using var d=new FolderBrowserDialog{Description="Chọn thư mục lưu bản sao dữ liệu"};
        if(d.ShowDialog()!=DialogResult.OK)return;
        try{var dest=Path.Combine(d.SelectedPath,"Backup_"+DateTime.Now.ToString("yyyyMMdd_HHmmss"));Directory.CreateDirectory(dest);foreach(var f in Directory.GetFiles(FileHelper.DataDir))File.Copy(f,Path.Combine(dest,Path.GetFileName(f)),true);MessageBox.Show("Đã sao lưu: "+dest);}
        catch(Exception ex){Ui.ShowError(ex);}
    }
    void Restore()
    {
        using var d=new FolderBrowserDialog{Description="Chọn thư mục backup cần khôi phục"};
        if(d.ShowDialog()!=DialogResult.OK)return;
        if(MessageBox.Show("Khôi phục sẽ ghi đè dữ liệu hiện tại. Tiếp tục?","Xác nhận",MessageBoxButtons.YesNo)!=DialogResult.Yes)return;
        try{foreach(var f in Directory.GetFiles(d.SelectedPath,"*.txt"))File.Copy(f,Path.Combine(FileHelper.DataDir,Path.GetFileName(f)),true);AppData.Initialize();MessageBox.Show("Khôi phục xong. Nên đóng và mở lại các Form.");}
        catch(Exception ex){Ui.ShowError(ex);}
    }
}
