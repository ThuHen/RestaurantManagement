using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class StatisticDL : DataProvider
    {
        public DataTable getSalesByCategory(DateTime startDate, DateTime endDate)
        {
            String query = @"
        SELECT 
            
            c.catName,           
            p.Name,
            d.Qty,
            d.Price,
            d.Amount,
            m.Date
        FROM tblMain m
        INNER JOIN tblDetails d ON m.MainID = d.MainID
        INNER JOIN products p ON p.Id = d.ProID
        INNER JOIN Category c ON c.catID = p.CategoryID
        WHERE m.Date BETWEEN @startDate AND @endDate
    ";

            SqlConnection conn = GetConnection();
            conn.Open();
            SqlCommand sqlCommand = new SqlCommand(query, conn);
            sqlCommand.Parameters.AddWithValue("@startDate", startDate);
            sqlCommand.Parameters.AddWithValue("@endDate", endDate);
            DataTable rs = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            sqlDataAdapter.Fill(rs);
            conn.Close();
            return rs;
        }
    }
}
