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
    public class DAL_NhanVien : DBConnect
    {
        public DataTable getNhanVien()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DanhSachNV]";
                cmd.Connection = _conn;
                DataTable dtHang = new DataTable();
                dtHang.Load(cmd.ExecuteReader());
                return dtHang;
            }
            finally
            {
                _conn.Close();
            }

        }
        public bool KiemTraEmailTonTai( string Email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "KiemTraEmailTonTai";

               
                cmd.Parameters.AddWithValue("@Email", Email);

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
        public bool insertNhanVien(DTO_NhanVien nv)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[InsertDataIntoTblNhanVien]";
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("tennv", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("diachi", nv.DiaChi);
                cmd.Parameters.AddWithValue("vaiTro", nv.VaiTro);
                cmd.Parameters.AddWithValue("tinhTrang", nv.TinhTrang);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }
        public bool UpdateNhanVien(DTO_NhanVien nv)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDataIntoTblNhanVien";
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("Tennv", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("diaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("vaiTro", nv.VaiTro);
                cmd.Parameters.AddWithValue("tinhTrang", nv.TinhTrang);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }

        public bool DeleteNnhanVien(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteDataFromtblNhanVien]";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Connection = _conn;
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }
        public DataTable SearchNhanVien(string tenNhanvien)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[SearchNhanVien]";
                cmd.Parameters.AddWithValue("tenNV", tenNhanvien);
                cmd.Connection = _conn;
                DataTable dtNhanVien = new DataTable();
                dtNhanVien.Load(cmd.ExecuteReader());
                return dtNhanVien;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool NhanVienDangNhap(DTO_NhanVien nv)
        {
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DangNhap";
                //cmd.Parameters.AddWithValue("email", email);
                //cmd.Parameters.AddWithValue("matKhau", matKhau);
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("matKhau", nv.MatKhau);
                // Query và kiểm tra
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }

        public DataTable VaiTroNhanVien(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[LayVaiTroNV]";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Connection = _conn;
                DataTable dtNhanVien = new DataTable();
                dtNhanVien.Load(cmd.ExecuteReader());
                return dtNhanVien;
            }
            finally
            {
                _conn.Close();
            }
        }
        public DataTable TinhTrangNhanVien(string email)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[LayTinhTrangNV]";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Connection = _conn;
                DataTable dtNhanVien = new DataTable();
                dtNhanVien.Load(cmd.ExecuteReader());
                return dtNhanVien;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
        public bool UpdateMatKhau(string email, string matKhauCu, string matKhauMoi)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "chgpwd";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("@opwd", matKhauCu);
                cmd.Parameters.AddWithValue("@npwd", matKhauMoi);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }
        public bool NhanVienQuenMatKhau(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "QuenMatKhau";
                cmd.Parameters.AddWithValue("email", email);
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }
        public bool TaoMatKhau(string email, string matKhauMoi)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TaoMatKhauMoi";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("matkhau", matKhauMoi);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                _conn.Close();
            }
            return false;
        }
        public string GetCurrentPasswordHash(string email)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCurrentPasswordHash"; // Tên stored procedure
                cmd.Parameters.AddWithValue("email", email); // Tham số @email

                object result = cmd.ExecuteScalar();
                return result?.ToString();
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ (ghi log, hiển thị thông báo, v.v.)
                Console.WriteLine("Lỗi khi lấy hash mật khẩu: " + ex.Message);
                return null;
            }
            finally
            {
                _conn.Close();
            }
        }

    }
}
