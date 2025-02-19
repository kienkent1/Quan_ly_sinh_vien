using BUS_QLBH;
using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class frmHang : Form
    {
        BUS_Hang busHang = new BUS_QLBH.BUS_Hang();
        string stremail = FrmMain.mail;//nhận email tư FrmMain
        string checkUrlImage;//kiểm tra hinh khi câp nhật
        string fileName;  // ten file
        string fileSavePath;//url store image  
        string fileAddress;// url load images
        public frmHang()
        {
            InitializeComponent();
        }

        

        private void frmHang_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_Hang();
        }
        private void ResetValues()
        {
            txtMahang.Text = null;
            txtTenhang.Text = null;
            txtSoluong.Text = null;
            txtDongianhap.Text = null;
            txtDongiaban.Text = null;
            txtHinh.Text = null;
            pbHinh.Image = null;
            txtGhichu.Text = null;

            txtTenhang.Enabled = false;
            txtSoluong.Enabled = false;
            txtDongianhap.Enabled = false;
            txtDongiaban.Enabled = false;
            txtHinh.Enabled = false;
            txtGhichu.Enabled = false;
            btnMo.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void LoadGridview_Hang()
        {
            dgvhang.DataSource = busHang.getHang();
            dgvhang.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvhang.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvhang.Columns[2].HeaderText = "Số Lượng";
            dgvhang.Columns[3].HeaderText = "Đơn Giá Nhập";
            dgvhang.Columns[4].HeaderText = "Đơn Giá Bán";
            dgvhang.Columns[5].HeaderText = "Hình Ảnh";
            dgvhang.Columns[7].Visible = false;

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMahang.Text))
            {
                MessageBox.Show("Bạn phải chọn sản phẩm để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int maHang = int.Parse(txtMahang.Text);
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                // Lấy đường dẫn ảnh trước khi xóa
                string imagePath = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + txtHinh.Text;

               
                //Xóa sản phẩm trong database
                //MessageBox.Show(imagePath);
                if (busHang.DeleteHang(maHang))
                {
                    // Kiểm tra và xóa hình trong thư mục nếu tồn tại
                    if (File.Exists(imagePath))
                    {
                        //MessageBox.Show("f");
                        try
                        {
                            File.Delete(imagePath);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Không thể xóa hình ảnh: " + ex.Message);
                        }
                    }

                    MessageBox.Show("Xóa sản phẩm thành công");
                    ResetValues();
                    LoadGridview_Hang(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Xóa sản phẩm không thành công");
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int intSoLuong;
            bool isInt = int.TryParse(txtSoluong.Text.Trim().ToString(), out intSoLuong);
            float floatDonGiaNhap;
            bool isFloatNhap = float.TryParse(txtDongianhap.Text.Trim().ToString(), out floatDonGiaNhap);
            float floatDonGiaBan;
            bool isFloatBan = float.TryParse(txtDongiaban.Text.Trim().ToString(), out floatDonGiaBan);
            float dongianhap = float.Parse(txtDongianhap.Text);
            float dongiaban = float.Parse(txtDongiaban.Text);

            if (txtTenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenhang.Focus();
                return;
            }
            else if (!isInt || int.Parse(txtSoluong.Text) < 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm >0, số nguyên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            else if (!isFloatNhap || float.Parse(txtDongianhap.Text) < 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (!isFloatBan || float.Parse(txtDongiaban.Text) < 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            else if (!isFloatNhap || dongianhap > dongiaban)
            {
                MessageBox.Show("Đơn giá nhập phải nhỏ hơn đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (txtHinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải upload hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            else
            {
                // Kiểm tra tên file ảnh và đổi tên nếu bị trùng
                string directory = Path.Combine(Application.StartupPath.Substring(0, Application.StartupPath.Length - 10), "Images");
                string newFileName = fileName;
                int counter = 1;

                // Nếu file đã tồn tại, thêm số thứ tự vào tên file
                while (File.Exists(Path.Combine(directory, newFileName)))
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
                    string fileExtension = Path.GetExtension(fileName);
                    newFileName = $"{fileNameWithoutExtension}({counter++}){fileExtension}";
                }

                // Tạo đường dẫn mới cho file ảnh
                string newFilePath = Path.Combine(directory, newFileName);

                // Thực hiện copy ảnh vào thư mục với tên mới
                File.Copy(fileAddress, newFilePath, true); // copy ảnh với tên mới

                // Cập nhật đối tượng DTO_Hang với đường dẫn mới của ảnh
                DTO_Hang h = new DTO_Hang(txtTenhang.Text, int.Parse(txtSoluong.Text), float.Parse(txtDongianhap.Text),
                    float.Parse(txtDongiaban.Text), "\\Images\\" + newFileName, txtGhichu.Text, stremail);

                if (busHang.InsertHang(h))
                {
                    MessageBox.Show("Thêm sản phẩm thành công");
                    ResetValues();
                    LoadGridview_Hang(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm không thành công");
                }
            }
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlgOpen = new OpenFileDialog();
                dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
                dlgOpen.FilterIndex = 2;
                dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
                if (dlgOpen.ShowDialog() == DialogResult.OK)
                {
                    fileAddress = dlgOpen.FileName;
                    pbHinh.Image = Image.FromFile(fileAddress);
                    fileName = Path.GetFileName(dlgOpen.FileName);
                    string saveDirectory = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                    fileSavePath = saveDirectory + "\\Images\\" + fileName;// combine with file name*/
                    txtHinh.Text = "\\Images\\" + fileName;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi: " + ex);
            }
        }
        private string GetUniqueFileName(string filePath)
        {
            int count = 1;
            string fileNameOnly = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);
            string path = Path.GetDirectoryName(filePath);
            string newFullPath = filePath;

            while (File.Exists(newFullPath))
            {
                string tempFileName = $"{fileNameOnly}({count++})";
                newFullPath = Path.Combine(path, tempFileName + extension);
            }
            return newFullPath;
        }





        private void btnSua_Click(object sender, EventArgs e)
        {
            int intSoLuong;
            bool isInt = int.TryParse(txtSoluong.Text.Trim().ToString(), out intSoLuong);
            float floatDonGiaNhap;
            bool isFloatNhap = float.TryParse(txtDongianhap.Text.Trim().ToString(), out floatDonGiaNhap);
            float floatDonGiaBan;
            bool isFloatBan = float.TryParse(txtDongiaban.Text.Trim().ToString(), out floatDonGiaBan);
            float dongianhap = float.Parse(txtDongianhap.Text);
            float dongiaban = float.Parse(txtDongiaban.Text);
            if (txtTenhang.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenhang.Focus();
                return;
            }
            else if (!isInt || int.Parse(txtSoluong.Text) < 0)// kiem tra so nguyen > 0
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm >0, số nguyên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            else if (!isFloatNhap || float.Parse(txtDongianhap.Text) < 0)// kiem tra so > 0
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (!isFloatBan || float.Parse(txtDongiaban.Text) < 0)// kiem tra so > 0
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            else if (!isFloatNhap || dongianhap > dongiaban)// kiem tra so > 0
            {
                MessageBox.Show("Đơn giá nhập phải nhỏ hơn đơn giá bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (txtHinh.Text.Trim().Length == 0)// kiem tra phai nhap hinh
            {
                MessageBox.Show("Bạn phải upload hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            else
            {
                DTO_Hang h = new DTO_Hang(int.Parse(txtMahang.Text), txtTenhang.Text, int.Parse(txtSoluong.Text),
                    float.Parse(txtDongianhap.Text),
                    float.Parse(txtDongiaban.Text), txtHinh.Text, txtGhichu.Text);
                if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa", "Confirm", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (busHang.UpdateHang(h))
                    {
                        if (txtHinh.Text != checkUrlImage){//nêu có thay doi hình
                            
                            File.Copy(fileAddress, fileSavePath, true);// copy file hinh vao ung dung
                            string imagePath = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + checkUrlImage;
                            if (File.Exists(imagePath))
                            {
                                //MessageBox.Show("f");
                                try
                                {
                                    File.Delete(imagePath);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Không thể xóa hình ảnh: " + ex.Message);
                                }
                            }
                        }
                        MessageBox.Show("Sửa thành công");
                        ResetValues();
                        LoadGridview_Hang(); // refresh datagridview
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

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tenHang = txttimKiem.Text;
            DataTable ds = busHang.SearchHang(tenHang);
            if (ds.Rows.Count > 0)
            {
                dgvhang.DataSource = ds;
                dgvhang.Columns[0].HeaderText = "Mã Sản Phẩm";
                dgvhang.Columns[1].HeaderText = "Tên Sản Phẩm";
                dgvhang.Columns[2].HeaderText = "Số Lượng";
                dgvhang.Columns[3].HeaderText = "Đơn Giá Nhập";
                dgvhang.Columns[4].HeaderText = "Đơn Giá Bán";
            }
            else
            {
                MessageBox.Show("Không tìm thấy san pham", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txttimKiem.Text = "Nhập tên sản phẩm";
            txttimKiem.BackColor = Color.LightGray;
            ResetValues();
        }

        private void txttimKiem_Click(object sender, EventArgs e)
        {
            txttimKiem.Text = null;
            txttimKiem.BackColor = Color.White;
        }

        

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btds.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnMo.Enabled = true;
            txtTenhang.Enabled = true;
            txtSoluong.Enabled = true;
            txtDongianhap.Enabled = true;
            txtDongiaban.Enabled = true;
            txtGhichu.Enabled = true;
            txtMahang.Text = null;
            txtTenhang.Text = null;
            txtSoluong.Text = null;
            txtDongianhap.Text = null;
            txtDongiaban.Text = null;
            txtHinh.Text = null;
            pbHinh.Image = null;
            txtGhichu.Text = null;
            txtTenhang.Focus();
        }

        private void dgvhang_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo đường dẫn đầy đủ đến thư mục chứa ảnh
                string saveDirectory = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
                if (dgvhang.Rows.Count > 1)
                {
                    btnMo.Enabled = true;
                    btnLuu.Enabled = false;
                    txtTenhang.Enabled = true;
                    txtSoluong.Enabled = true;
                    txtDongianhap.Enabled = true;
                    txtDongiaban.Enabled = true;
                    txtGhichu.Enabled = true;
                    txtTenhang.Focus();
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    txtMahang.Text = dgvhang.CurrentRow.Cells["MaHang"].Value.ToString();
                    txtTenhang.Text = dgvhang.CurrentRow.Cells["TenHang"].Value.ToString();
                    txtSoluong.Text = dgvhang.CurrentRow.Cells["SoLuong"].Value.ToString();
                    txtDongianhap.Text = dgvhang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
                    txtDongiaban.Text = dgvhang.CurrentRow.Cells["DonGiaBan"].Value.ToString();
                    txtHinh.Text = dgvhang.CurrentRow.Cells["HinhAnh"].Value.ToString();
                    checkUrlImage = txtHinh.Text;//giữ đường dẫn file hình
                    using (Image temp = Image.FromFile(Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10)) + txtHinh.Text))
                    {
                        pbHinh.Image = new Bitmap(temp);
                    }
                    txtGhichu.Text = dgvhang.CurrentRow.Cells["GhiChu"].Value.ToString();
                }
                else
                {
                    MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        

        private void btds_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_Hang();
        }
    }
}
