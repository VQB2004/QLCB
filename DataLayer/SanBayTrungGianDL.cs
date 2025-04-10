using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
namespace DataLayer
{
    public class SanBayTrungGianDL
    {
       private DataProvider provider = new DataProvider();
        //Lấy danh sách sân bay trung gian
        public List<SanBayTrungGianTO> GetSanBayTrungGianList()
        {
            string sql = "SELECT * FROM SanBayTrungGian";
            DataTable dt = provider.MyExecuteReader(sql, CommandType.Text);

            List<SanBayTrungGianTO> list = new List<SanBayTrungGianTO>();

            foreach (DataRow row in dt.Rows)
            {
                SanBayTrungGianTO sbtg = new SanBayTrungGianTO
                {
                    MaSB = Convert.ToInt32(row["maSB"]),
                    MaTB = Convert.ToInt32(row["maTB"]),
                    ThoiGianDung = TimeSpan.Parse(row["thoiGianDung"].ToString()).ToString(@"hh\:mm") 
                };

                list.Add(sbtg);
            }

            return list;
        }

        // Thêm sân bay trung gian
        public bool AddSanBayTrungGian(int maSB, int maTB, TimeSpan thoiGianDung)
        {
            string sql = "INSERT INTO SanBayTrungGian (maSB, maTB, thoiGianDung) VALUES (@maSB, @maTB, @thoiGianDung)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maSB", maSB),
                new SqlParameter("@maTB", maTB),
                new SqlParameter("@thoiGianDung", thoiGianDung)
            };
            return provider.MyExecuteNonQuery(sql, CommandType.Text, parameters) > 0;
        }

    }
}
