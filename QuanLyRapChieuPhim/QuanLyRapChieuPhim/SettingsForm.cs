namespace QuanLyRapChieuPhim;

public class SettingsForm : Form
{
    readonly NumericUpDown size=new(){Minimum=8,Maximum=20,DecimalPlaces=0,Value=9};Color back=Color.FromArgb(35,39,47),fore=Color.White;
    public SettingsForm(){BackColor=Ui.MainBack;ForeColor=Ui.MainText;Font=new Font("Segoe UI",9);Text="Cấu hình giao diện";Width=420;Height=220;var p=Ui.Flow();p.Controls.Add(Ui.Lab("Cỡ chữ"));p.Controls.Add(size);var b1=Ui.Btn("Chọn màu nền");b1.Click+=(_,_)=>{using var d=new ColorDialog();if(d.ShowDialog()==DialogResult.OK)back=d.Color;};var b2=Ui.Btn("Chọn màu chữ");b2.Click+=(_,_)=>{using var d=new ColorDialog();if(d.ShowDialog()==DialogResult.OK)fore=d.Color;};var save=Ui.Btn("Lưu Registry");save.Click+=(_,_)=>{AppData.SaveAppearance(back,fore,(float)size.Value);MessageBox.Show("Đã lưu. Đóng và mở lại ứng dụng để áp dụng.");Close();};p.Controls.Add(b1);p.Controls.Add(b2);p.Controls.Add(save);Controls.Add(p);}
}
