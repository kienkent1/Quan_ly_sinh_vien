using BUS_QLBH;
using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class FrmKhach : Form
    {
        BUS_Khach busKhach = new BUS_QLBH.BUS_Khach();
        string stremail = FrmMain.mail;//nhận email tư FrmMain
                                       // Thêm biến này ở đầu class FrmKhach
        private string selectedMaKhach;

        public FrmKhach()
        {
            InitializeComponent();
        }
    private void FrmKhach_Load(object sender, EventArgs e)
        {
            LoadGridview_Khach();
            ResetValues();
        }
        private void LoadGridview_Khach()
        {
            dgvkhach.DataSource = busKhach.getKhach();
            dgvkhach.Columns[0].HeaderText = "Mã khách";
            dgvkhach.Columns[1].HeaderText = "Điện Thoại";
            dgvkhach.Columns[2].HeaderText = "Họ và Tên";
            dgvkhach.Columns[3].HeaderText = "Địa Chỉ";
            dgvkhach.Columns[4].HeaderText = "Giới Tính";
            dgvkhach.Columns[5].Visible = false;
        }

      
        private void ResetValues()
        {
            txtDiachi.Text = null;
            txtDienthoai.Text = null;
            txtTenkhach.Text = null;
            rbnam.Checked = false;
            rbnu.Checked = false;

            txtDiachi.Enabled = false;
            txtDienthoai.Enabled = false;
            txtTenkhach.Enabled = false;
            rbnu.Enabled = false;
            rbnam.Enabled = false;
            dgvkhach.Enabled = true;

            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtDienthoai.Focus();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtDiachi.Text = null;
            txtDienthoai.Text = null;
            txtTenkhach.Text = null;
            rbnam.Checked = true;
            rbnu.Checked = false;

            txtDiachi.Enabled = true;
            txtDienthoai.Enabled = true;
            txtTenkhach.Enabled = true;
            rbnu.Enabled = true;
            rbnam.Enabled = true;
            dgvkhach.Enabled = false;

            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txttimKiem.Text = "Nhập số điện thoại khách hàng";
            ResetValues();
            LoadGridview_Khach();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedMaKhach) ||
                    string.IsNullOrEmpty(txtDienthoai.Text) ||
                    string.IsNullOrEmpty(txtDiachi.Text) ||
                    string.IsNullOrEmpty(txtTenkhach.Text))
                {
                    MessageBox.Show("Vui lòng kiểm tra thông tin để thêm", "Thông báo");
                    return;
                }

                string soDienThoai = txtDienthoai.Text.Trim();
                string tenKhach = txtTenkhach.Text.Trim();
                string maKhach = selectedMaKhach.Trim();

                // Kiểm tra số điện thoại: từ 10 đến 13 chữ số và là số nguyên
                if (!Regex.IsMatch(soDienThoai, @"^\d{10,13}$"))
                {
                    MessageBox.Show("Số điện thoại phải là số nguyên và có từ 10 đến 13 chữ số.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDienthoai.Focus();
                    return;
                }

                // Kiểm tra tên khách không chứa số
                if (Regex.IsMatch(tenKhach, @"\d"))
                {
                    MessageBox.Show("Tên khách không được chứa số.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenkhach.Focus();
                    return;
                }

                // Kiểm tra số điện thoại có trong database chưa
                if (busKhach.KiemTraSoDienThoaiTonTai(maKhach,soDienThoai))
                {
                    MessageBox.Show("Số điện thoại này đã tồn tại. Vui lòng nhập số khác!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienthoai.Focus();
                    return;
                }

                float intDienThoai;
                bool isInt = float.TryParse(txtDienthoai.Text, out intDienThoai);
                string phai = rbnu.Checked ? "Nữ" : "Nam";

                if (!isInt || float.Parse(txtDienthoai.Text) < 0) // Kiểm tra số điện thoại hợp lệ
                {
                    MessageBox.Show("Bạn phải nhập số điện thoại >0, số nguyên",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDienthoai.Focus();
                    return;
                }

                DTO_Khach kh = new DTO_Khach(txtDienthoai.Text, txtTenkhach.Text, txtDiachi.Text, phai, stremail);
                if (busKhach.InsertKhach(kh))
                {
                    MessageBox.Show("Thêm thành công");
                    ResetValues();
                    LoadGridview_Khach(); // Refresh datagridview
                }
                else
                {
                    MessageBox.Show("Thêm không thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }

        }

        private void dgvkhach_Click(object sender, EventArgs e)
        {
            if (dgvkhach.Rows.Count > 1)
            {
                btnLuu.Enabled = false;
                txtDiachi.Enabled = true;
                txtDienthoai.Enabled = true;
                txtTenkhach.Enabled = true;
                rbnu.Enabled = true;
                rbnam.Enabled = true;
                txtDienthoai.Focus();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;

                selectedMaKhach = dgvkhach.CurrentRow.Cells[0].Value.ToString();
                txtDienthoai.Text = dgvkhach.CurrentRow.Cells[1].Value.ToString();
                txtTenkhach.Text = dgvkhach.CurrentRow.Cells[2].Value.ToString();
                txtDiachi.Text = dgvkhach.CurrentRow.Cells[3].Value.ToString();
                string phai = dgvkhach.CurrentRow.Cells[4].Value.ToString();
                rbnam.Checked = phai == "Nam";
                rbnu.Checked = phai == "Nữ";
            }
            else
            {
                MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //MessageBox.Show(selectedMaKhach);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(selectedMaKhach) ||
                   string.IsNullOrEmpty(txtDienthoai.Text) ||
                   string.IsNullOrEmpty(txtDiachi.Text) ||
                   string.IsNullOrEmpty(txtTenkhach.Text))
                {
                    MessageBox.Show("Vui lòng kiểm tra thông tin để thêm", "Thông báo");
                    return;
                }

                string soDienThoai = txtDienthoai.Text.Trim();
                string tenKhach = txtTenkhach.Text.Trim();
                string maKhach = selectedMaKhach.Trim();

                // Kiểm tra số điện thoại: từ 10 đến 13 chữ số và là số nguyên
                if (!Regex.IsMatch(soDienThoai, @"^\d{10,13}$"))
                {
                    MessageBox.Show("Số điện thoại phải là số nguyên và có từ 10 đến 13 chữ số.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDienthoai.Focus();
                    return;
                }

                // Kiểm tra tên khách không chứa số
                if (Regex.IsMatch(tenKhach, @"\d"))
                {
                    MessageBox.Show("Tên khách không được chứa số.",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtTenkhach.Focus();
                    return;
                }

                // Kiểm tra số điện thoại có trong database chưa
                if (busKhach.KiemTraSoDienThoaiTonTai(maKhach,soDienThoai))
                {
                    MessageBox.Show("Số điện thoại này đã tồn tại. Vui lòng nhập số khác!",
                                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDienthoai.Focus();
                    return;
                }


                // Kiểm tra thành công, tiếp tục xử lý
                string phai = rbnu.Checked ? "Nữ" : "Nam";

            DTO_Khach kh = new DTO_Khach(selectedMaKhach, soDienThoai, tenKhach, txtDiachi.Text, phai,stremail);

            if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (busKhach.UpdateKhach(kh))
                {
                    MessageBox.Show("Cập nhật khách hàng thành công");
                    ResetValues();
                    LoadGridview_Khach();
                }
                else
                {
                    MessageBox.Show("Cập nhật khách hàng không thành công");
                }
            }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi:" + ex);
            }

            
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(selectedMaKhach))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa", "Thông báo");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu khách hàng?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (busKhach.DeleteKhach(selectedMaKhach))
                {
                    MessageBox.Show("Xóa dữ liệu khách hàng thành công");
                    ResetValues();
                    LoadGridview_Khach();
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu khách hàng không thành công");
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string soDT = txttimKiem.Text;
            DataTable ds = busKhach.SearchKhach(soDT);
            if (ds.Rows.Count > 0)
            {
                dgvkhach.DataSource = ds;
                dgvkhach.Columns[0].HeaderText = "Mã Khách";
                dgvkhach.Columns[1].HeaderText = "Điện Thoại";
                dgvkhach.Columns[2].HeaderText = "Họ và Tên";
                dgvkhach.Columns[3].HeaderText = "Địa Chỉ";
                dgvkhach.Columns[4].HeaderText = "Giới Tính";
                dgvkhach.Columns[5].Visible = false;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng nào phù hợp tiêu chí tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttimKiem.Focus();
            }
            txttimKiem.Text = "Nhập số điện thoại khach hàng";
            txttimKiem.BackColor = Color.LightGray;
            ResetValues();
        }

        private void txttimKiem_Click(object sender, EventArgs e)
        {
            txttimKiem.Text = null;
            txttimKiem.BackColor = Color.White;
        }

        private void btnDanhsach_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_Khach();
        }
    }
}
