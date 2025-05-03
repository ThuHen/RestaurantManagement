using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;


namespace DataLayer
{
    public class TableDL:DataProvider
    {
      
        public List<Table> GetTables()
        {   
            string sql = "SELECT * FROM tables";
            string id, name;
            List<Table> tables = new List<Table>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    Table table = new Table(id, name);
                    tables.Add(table);
                }
                reader.Close();
                return tables;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
        }
        public int Add(Table table)
        {
            string sql = "INSERT INTO tables(tname) VALUES('" + table.Name +  "')";
            try
            {
                return MyExcuteNonQuery(sql, CommandType.Text);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public void Del(string id)
        {
            string sql = "DELETE FROM tables WHERE tid = @id";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@id", id));
            try
            {
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex; // (hoặc: throw để giữ stack trace gốc)
            }
        }

        public void Edit(int id, string name)
        {
            string sql = "UPDATE tables SET tname = @name WHERE tid = @id";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@name", name));
            parameters.Add(new SqlParameter("@id", id));

            try
            {
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex; // Giữ nguyên stack trace gốc
            }
        }
        


    }
}
