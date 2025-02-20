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
    public class DAL_LichSu : DBConnect
    {
        // Lấy danh sách lịch sử
        public DataTable GetLichSu()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblLichSu ORDER BY ThoiGian DESC", _conn);
            DataTable dtLichSu = new DataTable();
            da.Fill(dtLichSu);
            return dtLichSu;
        }

        // Thêm lịch sử vào database
        public bool InsertLichSu(DTO_LichSu lichSu)
        {
            try
            {
                _conn.Open();
                string sql = "INSERT INTO tblLichSu (MaNV, ThaoTac, ThoiGian, ChiTiet) VALUES (@MaNV, @ThaoTac, @ThoiGian, @ChiTiet)";
                SqlCommand cmd = new SqlCommand(sql, _conn);
                cmd.Parameters.AddWithValue("@MaNV", lichSu.MaNV);
                cmd.Parameters.AddWithValue("@ThaoTac", lichSu.ThaoTac);
                cmd.Parameters.AddWithValue("@ThoiGian", lichSu.ThoiGian);
                cmd.Parameters.AddWithValue("@ChiTiet", lichSu.ChiTiet);

                int result = cmd.ExecuteNonQuery();
                return result > 0;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }
       
    }
}
