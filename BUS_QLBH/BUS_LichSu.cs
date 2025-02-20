using DTO_QLBH;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QLBH;

namespace BUS_QLBH
{
    public class BUS_LichSu
    {
        DAL_LichSu dalLichSu = new DAL_LichSu();

        // Lấy danh sách lịch sử
        public DataTable GetLichSu()
        {
            return dalLichSu.GetLichSu();
        }

        // Thêm lịch sử
        public bool InsertLichSu(DTO_LichSu lichSu)
        {
            return dalLichSu.InsertLichSu(lichSu);
        }
        
    }
}
