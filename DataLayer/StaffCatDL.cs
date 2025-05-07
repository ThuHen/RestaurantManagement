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
    public class StaffCatDL:DataProvider
    {
      
        public List<StaffType> GetStaffCats()
        {
            string sql = "SELECT * FROM stafftype";
            string id, name;
            List<StaffType> categories = new List<StaffType>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    StaffType StaffType = new StaffType(int.Parse(id),name);
                    categories.Add(StaffType);
                }
                reader.Close();
                
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                Disconnect();
            }
            return categories;
        }
        public int Add(StaffType StaffType)
        {
            string sql = "INSERT INTO stafftype(typeName) VALUES('" + StaffType.typeName +  "')";
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
            string sql = "DELETE FROM stafftype WHERE typeId = @id";
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
            string sql = "UPDATE stafftype SET typeName = @name WHERE typeId = @id";
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
