﻿using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLBH
{
   public class DAL_Khach:DBConnect
    {
        public DataTable getKhach()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DanhSachKhach";
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool KiemTraSoDienThoaiTonTai(string maKhach, string soDienThoai)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "KiemTraSoDienThoaiTonTai";

                cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                cmd.Parameters.AddWithValue("@DienThoai", soDienThoai);

                int result = Convert.ToInt32(cmd.ExecuteScalar());
                return result == 1; // Nếu trả về 1, số điện thoại đã tồn tại
            }
            catch (Exception e)
            {
                //MessageBox.Show("Lỗi kiểm tra số điện thoại: " + e.Message);
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }






        public bool insertKhach(DTO_Khach khach)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertDataIntoTblKhach";

                cmd.Parameters.AddWithValue("Makhach", khach.Makhach); // Thêm mã khách hàng
                cmd.Parameters.AddWithValue("Dienthoai", khach.SoDienThoai);
                cmd.Parameters.AddWithValue("TenKhach", khach.TenKhach);
                cmd.Parameters.AddWithValue("DiaChi", khach.DiaChi);
                cmd.Parameters.AddWithValue("phai", khach.Phai);
                cmd.Parameters.AddWithValue("Email", khach.EmailKhach);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                // Xử lý lỗi (log hoặc throw exception)
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool UpdateKhach(DTO_Khach khach)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDataIntoTblKhach";

                cmd.Parameters.AddWithValue("@MaKhach", khach.Makhach);
                cmd.Parameters.AddWithValue("@Dienthoai", khach.SoDienThoai);
                cmd.Parameters.AddWithValue("@TenKhach", khach.TenKhach);
                cmd.Parameters.AddWithValue("@DiaChi", khach.DiaChi);
                cmd.Parameters.AddWithValue("@phai", khach.Phai);

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show($"Lỗi khi cập nhật khách hàng: {e.Message}");
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool DeleteKhach(string maKhach)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDataFromtblKhach";
                cmd.Parameters.AddWithValue("@MaKhach", maKhach);
                cmd.Connection = _conn;

                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show($"Lỗi khi xóa khách hàng: {e.Message}");
            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public DataTable SearchKhach(string soDT)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchKhach";
                cmd.Parameters.AddWithValue("Dienthoai", soDT);
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}
