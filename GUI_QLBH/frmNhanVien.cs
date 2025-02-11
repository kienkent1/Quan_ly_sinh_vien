﻿using BUS_QLBH;
using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class frmNhanVien : Form
    {
        BUS_NhanVien busNhanVien = new BUS_QLBH.BUS_NhanVien();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_NhanVien();
        }
        private void LoadGridview_NhanVien()
        {
            dgvNhanvien.DataSource = busNhanVien.getNhanVien();
            dgvNhanvien.Columns[0].HeaderText = "Email";
            dgvNhanvien.Columns[1].HeaderText = "Tên Nhân Viên";
            dgvNhanvien.Columns[2].HeaderText = "Địa chỉ";
            dgvNhanvien.Columns[3].HeaderText = "Vai Trò";
            dgvNhanvien.Columns[4].HeaderText = "Tình Trạng";
        }
        private void ResetValues()
        {
            txttimKiem.Text = "Nhập tên nhân viên";
            txtEmail.Text = null;
            txtTennv.Text = null;
            txtDiachi.Text = null;
            txtEmail.Enabled = false;
            txtTennv.Enabled = false;
            txtDiachi.Enabled = false;
            rbNhanvien.Enabled = false;
            rbQuantri.Enabled = false;
            rbHoatDong.Enabled = false;
            rbNgung.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_NhanVien();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtEmail.Text = null;
            txtTennv.Text = null;
            txtDiachi.Text = null;
            txtTennv.Enabled = true;
            txtEmail.Enabled = true;
            txtDiachi.Enabled = true;
            rbNhanvien.Enabled = true;
            rbQuantri.Enabled = true;
            rbNgung.Enabled = true;
            rbHoatDong.Enabled = true;
            btnLuu.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            rbNhanvien.Checked = false;
            rbNgung.Checked = false;
            rbQuantri.Checked = false;
            rbHoatDong.Checked = false;
            txtEmail.Focus();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool IsValid(string emailaddress)// check rule email
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public void SendMail(string email)
        {
            try
            {

                //Sending the email.
                //Now we must create a new Smtp client to send our email.

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);   //smtp.gmail.com // For Gmail
                                                                            //smtp.live.com // Windows live / Hotmail
                                                                            //smtp.mail.yahoo.com // Yahoo
                                                                            //smtp.aim.com // AIM
                                                                            //my.inbox.com // Inbox

                //Authentication.
                //This is where the valid email account comes into play. You must have a valid email account(with password) to give our program a place to send the mail from.

                NetworkCredential cred = new NetworkCredential("kienpxtb00438@fpt.edu.vn", "qbop bclo aoaf kyps");

                //To send an email we must first create a new mailMessage(an email) to send.
                MailMessage Msg = new MailMessage();

                // Sender e-mail address.
                Msg.From = new MailAddress("kienpxtb00438@fpt.edu.vn");//Nothing But Above Credentials or your credentials (*******@gmail.com)

                // Recipient e-mail address.
                Msg.To.Add(email);

                // Assign the subject of our message.
                Msg.Subject = "Chao mừng thanh vien mới";

                // Create the content(body) of our message.
                Msg.Body = "Chào anh/chị. Mật khẩu truy cập phần mềm là abc123, anh/chi vui lòng đăng nhập vào phần mềm và đổi mật khẩu ";

                // Send our account login details to the client.
                client.Credentials = cred;

                //Enabling SSL(Secure Sockets Layer, encyription) is reqiured by most email providers to send mail
                client.EnableSsl = true;

                // Send our email.
                client.Send(Msg);
                //Confirmation After Click the Button
                MessageBox.Show("Your Mail is sended");
            }
            catch (Exception ex)
            {
                // If Mail Doesnt Send Error Mesage Will Be Displayed
                MessageBox.Show(ex.Message);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string email;
            int role = 0;//vai tro nhan vien
            if (rbQuantri.Checked)
                role = 1;//quản trị
            int tinhtrang = 0;//ngừng hoạt động
            if (rbHoatDong.Checked)
                tinhtrang = 1;// hoạt đọng
            if (txtEmail.Text.Trim().Length == 0)// kiem tra phai nhap email
            {
                MessageBox.Show("Bạn phải nhập email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            else if (!IsValid(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Bạn phải nhập đúng định dang email", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEmail.Focus();
                return;
            }
            if (txtTennv.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTennv.Focus();
                return;
            }
            else if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }
            if (rbQuantri.Checked == false && rbNhanvien.Checked == false)// kiem tra phai check tình trạng
            {
                MessageBox.Show("Bạn phải chon vai trò nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTennv.Focus();
                return;
            }
            if (rbHoatDong.Checked == false && rbNgung.Checked == false)// kiem tra phai check tình trạng
            {
                MessageBox.Show("Bạn phải chon tình trạng nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTennv.Focus();
                return;
            }
            else
            {
                // Tạo DTo
                DTO_NhanVien nv = new DTO_NhanVien(txtEmail.Text, txtTennv.Text, txtDiachi.Text, role, tinhtrang); // Vì ID tự tăng nên để ID số gì cũng dc
                if (busNhanVien.insertNhanVien(nv))
                {
                    MessageBox.Show("Thêm thành công");
                    ResetValues();
                    LoadGridview_NhanVien(); // refresh datagridview
                    email = txtEmail.Text;
                    SendMail(nv.EmailNV);
                }
                else
                {
                    MessageBox.Show("Thêm ko thành công");
                }
            }
        }

        private void dgvNhanvien_Click(object sender, EventArgs e)
        {
            if (dgvNhanvien.Rows.Count > 1)
            {
                btnLuu.Enabled = false;
                txtTennv.Enabled = true;
                txtDiachi.Enabled = true;
                rbQuantri.Enabled = true;
                rbNhanvien.Enabled = true;
                rbHoatDong.Enabled = true;
                rbNgung.Enabled = true;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                //show data from selected row to controls
                txtEmail.Text = dgvNhanvien.CurrentRow.Cells["email"].Value.ToString();
                txtTennv.Text = dgvNhanvien.CurrentRow.Cells["TenNv"].Value.ToString();
                txtDiachi.Text = dgvNhanvien.CurrentRow.Cells["diaChi"].Value.ToString();
                if (int.Parse(dgvNhanvien.CurrentRow.Cells["vaiTro"].Value.ToString()) == 1)
                    rbQuantri.Checked = true;
                else
                    rbNhanvien.Checked = true;
                if (int.Parse(dgvNhanvien.CurrentRow.Cells["tinhTrang"].Value.ToString()) == 1)
                    rbHoatDong.Checked = true;
                else
                    rbNgung.Checked = true;
            }
            else
            {
                MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtTennv.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTennv.Focus();
                return;
            }
            else if (txtDiachi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDiachi.Focus();
                return;
            }
            else
            {
                int role = 0;//vai tro nhan vien
                if (rbQuantri.Checked)
                    role = 1;//quản trị
                int tinhtrang = 0;//ngừng hoạt động
                if (rbHoatDong.Checked)
                    tinhtrang = 1;// hoạt đọng
                // Tạo DTo
                DTO_NhanVien nv = new DTO_NhanVien(txtEmail.Text, txtTennv.Text, txtDiachi.Text, role, tinhtrang); // Vì ID tự tăng nên để ID số gì cũng dc
                if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    //do something if YES
                    if (busNhanVien.UpdateNhanVien(nv))
                    {
                        MessageBox.Show("Sửa thành công");
                        ResetValues();
                        LoadGridview_NhanVien(); // refresh datagridview
                    }
                    else
                    {
                        MessageBox.Show("Sửa ko thành công");
                    }
                }
                else
                {
                    ResetValues();
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //do something if YES
                if (busNhanVien.DeleteNnhanVien(email))
                {
                    MessageBox.Show("Xóa dữ liệu thành công");
                    ResetValues();
                    LoadGridview_NhanVien(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
            else
            {
                //do something if NO
                ResetValues();
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tenNhanvien = txttimKiem.Text;//search by name
            DataTable ds = busNhanVien.SearchNhanVien(tenNhanvien);
            if (ds.Rows.Count > 0) // tim thay ket qua
            {
                dgvNhanvien.DataSource = ds;
                dgvNhanvien.Columns[0].HeaderText = "Email";
                dgvNhanvien.Columns[1].HeaderText = "Tên Nhân Viên";
                dgvNhanvien.Columns[2].HeaderText = "Địa chỉ";
                dgvNhanvien.Columns[3].HeaderText = "Vai Trò";
                dgvNhanvien.Columns[4].HeaderText = "Tình Trạng";
            }
            else
            {
                MessageBox.Show("Không tìm thấy nhân viên", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txttimKiem.Text = "Nhập tên nhân viên";
            txttimKiem.BackColor = Color.LightGray;
            ResetValues();
        }

        private void txttimKiem_TextChanged(object sender, EventArgs e)
        {
            txttimKiem.Text = null;
            txttimKiem.BackColor = Color.White;
        }

        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_NhanVien();
        }
    }
}
