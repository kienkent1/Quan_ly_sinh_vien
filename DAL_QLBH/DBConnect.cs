using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QLBH
{
    public class DBConnect
    {
        protected SqlConnection _conn = new SqlConnection(@"Data Source=XUONGKIEN\MSSQLSERVER01;Initial Catalog=SOF2051_QLBanHang1;Integrated Security=True");
    }
}
