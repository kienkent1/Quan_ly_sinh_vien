using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace GUI_QLBH
{
    public partial class FrmMain : Form
    {
        public static int session = 0;//kiem tra tình trạng login
        public static int profile = 0;// 
        public static string mail;// truyên email từ frmMain cho các form khác thong qua bien static
        Thread th;//using System.Threading;
        Login dn = new Login();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void đăngNhậpCtrlDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExistForm("Login"))
            {
                dn.MdiParent = this;
                dn.Show();
                dn.FormClosed += new FormClosedEventHandler(Login_FormClosed);
            }
            else
            {
                ActiveChildForm("Login");
            }
        }
        void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed        
            this.Refresh();
            FrmMain_Load(sender, e);// load form main again
        }
        private bool CheckExistForm(string name)
        {
            bool check = false;
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            ResetValue();
            if (profile == 1)// nếu vừa câp nhật mật khẩu thì 
                             //phải login lại, mục 'thong tin nhan vien' ẩn
            {
                thongtinnvToolStripMenuItem.Text = null;
                profile = 0; //ẩn mục 'thong tin nhan vien'
            }
        }
        private void ResetValue()
        {
            if (session == 1) // Người dùng đã đăng nhập
            {
                thongtinnvToolStripMenuItem.Text = "Chào " + FrmMain.mail;
                NhanVienToolStripMenuItem.Visible = true;
                danhMụcToolStripMenuItem.Visible = true;
                LoOutToolStripMenuItem.Enabled = true;
                thongkeToolStripMenuItem.Visible = true;
                ThongKeSPToolStripMenuItem.Visible = true;
                ProfileNvToolStripMenuItem.Visible = true;
                đăngNhậpToolStripMenuItem.Enabled = false;

                if (int.Parse(dn.vaitro) == 0) // Nếu vai trò là nhân viên
                {
                    VaiTroNV(); // Ẩn chức năng không dành cho nhân viên
                }
            }
            else // Người dùng chưa đăng nhập
            {
                NhanVienToolStripMenuItem.Visible = false;
                danhMụcToolStripMenuItem.Visible = false;
                LoOutToolStripMenuItem.Enabled = false;
                ProfileNvToolStripMenuItem.Visible = false;
                ThongKeSPToolStripMenuItem.Visible = false;
                thongkeToolStripMenuItem.Visible = false;
                đăngNhậpToolStripMenuItem.Enabled = true;
            }
        }

        private void VaiTroNV()
        {
            NhanVienToolStripMenuItem.Visible = false;
            thongkeToolStripMenuItem.Visible = false;
        }

        private void ProfileNvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmThongTinNV profilenv = new FrmThongTinNV(FrmMain.mail);// khơi tạo FrmThongTinNV với email nv

            if (!CheckExistForm("frmThongTinNV"))
            {
                profilenv.MdiParent = this;
                profilenv.FormClosed += new FormClosedEventHandler(FrmThongTinNV_FormClosed);
                profilenv.Show();
            }
            else
                ActiveChildForm("frmThongTinNV");
        }
        void FrmThongTinNV_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed
            this.Refresh();

            FrmMain_Load(sender, e);// load form main again
        }

        private void LoOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleLogout();
        }
        private void HandleLogout()
        {
            // Đặt lại trạng thái khi đăng xuất
            thongtinnvToolStripMenuItem.Text = null;
            session = 0;
            ResetValue();

            // Hiển thị lại form đăng nhập
            ShowLoginForm();
        }
        private void ShowLoginForm()
        {
            foreach (Form frm in this.MdiChildren)
            {
                frm.Close(); // Đóng tất cả form con trước khi mở lại Login
            }

            dn = new Login(); // Tạo lại đối tượng form Login
            dn.MdiParent = this;
            dn.Show();
            dn.FormClosed += new FormClosedEventHandler(Login_FormClosed);
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HandleLogout();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExistForm("frmHang"))
            {
                th = new Thread(OpenNewForm);//chay lại form đang nhap
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
                ActiveChildForm("frmHang");
        }
        private void OpenNewForm()
        {
            Application.Run(new frmHang());
        }
    }
}
