using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLBH
{
   public class DAL_Hang : DBConnect
    {
        public DataTable getHang()
        {
           
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHang", _conn);
            DataTable dtHang = new DataTable();
            da.Fill(dtHang);
            return dtHang;
        }
        public bool insertHang(DTO_Hang hang)
        {         
            try
            {              
                _conn.Open();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "InsertDataIntoTblHang";
                cmd.Parameters.AddWithValue("TenHang", hang.TenHang);
                cmd.Parameters.AddWithValue("SoLuong", hang.SoLuong);
                cmd.Parameters.AddWithValue("DonGiaNhap", hang.DonGiaNhap);
                cmd.Parameters.AddWithValue("DonGiaBan", hang.DonGiaBan);
                cmd.Parameters.AddWithValue("HinhAnh", hang.HinhAnh);
                cmd.Parameters.AddWithValue("GhiChu", hang.GhiChu);
                cmd.Parameters.AddWithValue("Email", hang.EmailNV);
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

        public bool UpdateHang(DTO_Hang hang)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDataIntoTblHang";
                cmd.Parameters.AddWithValue("MaHang", hang.MaHang);
                cmd.Parameters.AddWithValue("TenHang", hang.TenHang);
                cmd.Parameters.AddWithValue("SoLuong", hang.SoLuong);
                cmd.Parameters.AddWithValue("DonGiaNhap", hang.DonGiaNhap);
                cmd.Parameters.AddWithValue("DonGiaBan", hang.DonGiaBan);
                cmd.Parameters.AddWithValue("HinhAnh", hang.HinhAnh);
                cmd.Parameters.AddWithValue("GhiChu", hang.GhiChu);
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

        public bool DeleteHang(int maHang)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDataFromtblHang";
                cmd.Parameters.AddWithValue("MaHang", maHang);
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

        public DataTable SearchHang(string tenHang)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHang where TenHang like '%" + tenHang + "%'", _conn);
            DataTable dtHang = new DataTable();
            da.Fill(dtHang);
            return dtHang;
        }

        public DataTable ThongKeHang()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThongKeSP";
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

        public DataTable ThongKeTonKho()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThongKeTonKho";
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
    }
}
