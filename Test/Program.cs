using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            SanBayDL sanBayDL = new SanBayDL();
            DataTable dt = sanBayDL.GetSanBayList();

            foreach (DataColumn col in dt.Columns)
            {
                Console.WriteLine(col.ColumnName);
            }
        }
    }
}
