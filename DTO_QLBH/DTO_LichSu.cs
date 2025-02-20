using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLBH
{
    public class DTO_LichSu
    {
        public int MaNV { get; set; }
        public string ThaoTac { get; set; }
        public DateTime ThoiGian { get; set; }
        public string ChiTiet { get; set; }
        public int id { get; set; }

        public DTO_LichSu(int maNV, string thaoTac, DateTime thoiGian, string chiTiet)
        {
            MaNV = maNV;
            ThaoTac = thaoTac;
            ThoiGian = thoiGian;
            ChiTiet = chiTiet;
        }
    }
}
