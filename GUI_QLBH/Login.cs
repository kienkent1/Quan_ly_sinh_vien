using BUS_QLBH;
using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class Login : Form
    {
        BUS_NhanVien busNhanVien = new BUS_QLBH.BUS_NhanVien();

        //Các giá trị pass cho FrmMain phân quyền
        public string email { set; get; }
        public string matkhau { get; set; }
        public string vaitro { set; get; }
        public string tinhtrang { get; set; }
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            FrmMain.session = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            DTO_NhanVien nv = new DTO_NhanVien();
            nv.EmailNV = txtemail.Text;
            nv.MatKhau = encryption(txtmatkhau.Text);// ma mat khau de so sanh voi mat khau da ma hoa trong csdl
            DataTable dtTinhTrang = busNhanVien.TinhTrangNhanVien(nv.EmailNV);
            if (dtTinhTrang.Rows.Count > 0)
            {
                int tinhtrang = Convert.ToInt32(dtTinhTrang.Rows[0][0]);
                if (tinhtrang != 0)
                {
                    if (busNhanVien.NhanVienDangNhap(nv))
                    {
                        FrmMain.mail = nv.EmailNV;
                        DataTable dt = busNhanVien.VaiTroNhanVien(nv.EmailNV);
                        vaitro = dt.Rows[0][0].ToString();
                        MessageBox.Show("Đăng nhập thành công");
                        FrmMain.session = 1;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập không thành công, kiểm tra lại email hoặc mật khẩu");
                        txtemail.Text = null;
                        txtmatkhau.Text = null;
                        txtemail.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản của bạn không hoạt động, vui lòng liên hệ với quản trị viên.");
                }
            }
            else
            {
                MessageBox.Show("Email không tồn tại trong hệ thống.");
            }
        }
        public string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        private void btnQuenmk_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "")
            {
                if (busNhanVien.NhanVienQuenMatKhau(txtemail.Text))//show form input email. If true will send pass random
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(RandomString(4, true));
                    builder.Append(RandomNumber(1000, 9999));
                    builder.Append(RandomString(2, false));
                    //MessageBox.Show(builder.ToString());
                    string matkhaumoi = encryption(builder.ToString());
                    busNhanVien.TaoMatKhau(txtemail.Text, matkhaumoi);// update new pass to database
                    SendMail(txtemail.Text, builder.ToString());// send new pass to email
                }
                else
                {
                    MessageBox.Show("Email khong ton tai, vui long nhap lai email!");
                }
            }
            else
            {
                MessageBox.Show("Ban can nhap email nhan thong tin phuc hoi mat khau!");
                txtemail.Focus();
            }
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        //Tao so ngau nhien
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // send mail
        public void SendMail(string email, string matkhau)
        {
            try
            {
                //Now we must create a new Smtp client to send our email.
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);   //smtp.gmail.com // For Gmail
                //Authentication.
                //This is where the valid email account comes into play. You must have a valid email
                //account(with password) to give our program a place to send the mail from.
                NetworkCredential cred = new NetworkCredential("kienpxtb00438@fpt.edu.vn", "qbop bclo aoaf kyps");
                //To send an email we must first create a new mailMessage(an email) to send.
                MailMessage Msg = new MailMessage();
                // Sender e-mail address.
                Msg.From = new MailAddress("kienpxtb00438@fpt.edu.vn");//Nothing But Above Credentials or your credentials (*******@gmail.com)
                // Recipient e-mail address.
                Msg.To.Add(email);
                // Assign the subject of our message.
                Msg.Subject = "Ban da su dung tinh nang quen Mat khau";
                // Create the content(body) of our message.
                Msg.Body = "Chào anh/chị. Mật khẩu moi truy cập phần mềm là " + matkhau;
                // Send our account login details to the client.
                client.Credentials = cred;
                //Enabling SSL(Secure Sockets Layer, encyription) is reqiured by most email providers to send mail
                client.EnableSsl = true;
                client.Send(Msg);// Send our email.
                //Confirmation After Click the Button
                MessageBox.Show("Mot Email phục hồi mat khau da duoc goi toi ban!");
            }
            catch (Exception ex)
            {
                // If Mail Doesnt Send Error Mesage Will Be Displayed
                MessageBox.Show(ex.Message);
            }
        }
    }
}
