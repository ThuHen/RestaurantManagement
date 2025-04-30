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
    public class StaffDL : DataProvider
    {
        public Staff GetStaff(int id)
        {
            string sql = "SELECT * FROM Staff WHERE sID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@id", id)
    };
            Staff staff = null;
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, parameters); // ✅ truyền parameters
                if (reader.Read())
                {
                    int sID = Convert.ToInt32(reader["sID"]);
                    string name = reader["sName"].ToString();
                    string phone = reader["sPhone"].ToString();
                    int roleId = Convert.ToInt32(reader["sRole"]);                  
                    staff = new Staff(sID, name, phone, roleId);

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
            return staff;
        }


        public List<Staff> GetStaffs()
        {
            string sql = "SELECT s.*, t.typeName FROM staff s JOIN stafftype t ON s.sRole = t.typeID;";
            string id, name, phone, roleId, roleName;

            List<Staff> staffs = new List<Staff>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    phone = reader[2].ToString();
                    roleId = reader[3].ToString();
                    roleName = reader[4].ToString();
                    // Create Staff object from the data read
                    Staff staff = new Staff(int.Parse(id), name, phone, int.Parse(roleId), roleName);
                    staffs.Add(staff);
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
            return staffs;


        }
        public int Add(Staff Staff)
        {

            string sql = "INSERT INTO Staff(sName, sPhone, sRole) VALUES(@Name, @Phone, @RoleId)";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", Staff.Name),
                new SqlParameter("@Phone", Staff.Phone),
                new SqlParameter("@RoleId", Staff.RoleId)
            };
            try
            {
                Connect();
                return MyExcuteNonQuery(sql, CommandType.Text, parameters);
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
        public void Del(string id)
        {
            string sql = "DELETE FROM Staff WHERE sID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@id", id)
            };
            try
            {
                Connect();
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
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
        public void Edit(Staff Staff)
        {
            string sql = "UPDATE Staff SET sName = @name, sPhone = @phone, sRole = @role WHERE sID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@name", Staff.Name),
                new SqlParameter("@phone", Staff.Phone),
                new SqlParameter("@role", Staff.RoleId),
                new SqlParameter("@id", Staff.ID)
            };
            try
            {
                Connect();
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
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
    }
}
