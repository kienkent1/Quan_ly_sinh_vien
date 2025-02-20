using BUS_QLBH;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI_QLBH
{
    public partial class FrmThongKe : Form
    {
        private int id { get; set; }
        BUS_Hang busHang = new BUS_QLBH.BUS_Hang();
        BUS_LichSu buslichsu = new BUS_QLBH.BUS_LichSu();
        public FrmThongKe()
        {
            InitializeComponent();
            tpsanpham.Focus();
        }



        private void tcThongKe_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpsanpham)
            {
                LoadGridview_ThongKeHang();
            }
            else if (e.TabPage == tptonkho)
            {
                LoadGridview_ThongKeTonKho();
            }

        }



        private void LoadGridview_ThongKeHang()
        {
            dgvsp.AutoResizeColumns();
            dgvsp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvsp.DataSource = busHang.ThongKeHang();
            dgvsp.Columns[0].HeaderText = "Mã nhân viên";
            dgvsp.Columns[1].HeaderText = "Tên nhân viên";
            dgvsp.Columns[2].HeaderText = "Số Lượng Sản Phẩm Nhập";
            dgvsp.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvsp.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void LoadGridview_ThongKeTonKho()
        {
            dgvtonkho.AutoResizeColumns();
            dgvtonkho.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvtonkho.DataSource = busHang.ThongKeTonKho();
            dgvtonkho.Columns[0].HeaderText = "Tên Sản Phẩm";
            dgvtonkho.Columns[1].HeaderText = "Số Lượng Tồn";
            dgvtonkho.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvtonkho.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void LoadGridview_LichSu()
        {

            dgvlichsu.AutoResizeColumns();
            dgvlichsu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvlichsu.DataSource = buslichsu.GetLichSu(); // Truy vấn từ BUS
            dgvlichsu.Columns[0].Visible = false;
            dgvlichsu.Columns[1].HeaderText = "Mã nhân viên";
            dgvlichsu.Columns[2].HeaderText = "Thao tác";
            dgvlichsu.Columns[3].HeaderText = "Thời gian";
            dgvlichsu.Columns[4].HeaderText = "Chi tiết";
            dgvlichsu.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }





        private void dgvlichsu_Click(object sender, EventArgs e)
        {

        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            string ID = id.ToString();
            if (string.IsNullOrWhiteSpace(ID))
            {
                MessageBox.Show("Bạn phải chọn phần cần để xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (buslichsu.deleteLS(id))
            //{
            //    // Kiểm tra và xóa hình trong thư mục nếu tồn tại
            //    MessageBox.Show("Xóa thành công");
            //    LoadGridview_LichSu();
            //}
            //else
            //{
            //    MessageBox.Show("Xóa không thành công");
            //}

            MessageBox.Show(ID);
            
        }
            


        private void button1_Click(object sender, EventArgs e)
        {
            LoadGridview_LichSu();


        }
    }
}
