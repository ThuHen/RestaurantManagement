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
    public class UserDL : DataProvider
    {

        public List<Account> GetAccounts()
        {
            string sql = "SELECT * FROM users";
            string id, name, pass;
            int roleId;
            List<Account> users = new List<Account>();
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);
                while (reader.Read())
                {
                    id = reader[0].ToString();
                    name = reader[1].ToString();
                    pass = reader[2].ToString();
                    roleId = int.Parse(reader[3].ToString());
                    Account user = new Account(id, name, pass, roleId);
                    users.Add(user);
                }
                reader.Close();
                return users;
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
        public int Add(Account user)
        {
            string sql = "INSERT INTO users(username, upass, roleID) VALUES('" + user.Username + "', '" + user.Password + "', '" + user.RoleID + "')";
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
            string sql = "DELETE FROM users WHERE userID = @id";
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

        public void Update(Account account)
        {
            string sql = "UPDATE users SET username = @name, upass = @pass, roleID = @role WHERE userID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("@name", account.Username));
            parameters.Add(new SqlParameter("@id", Int32.Parse(account.Id.ToString())));
            parameters.Add(new SqlParameter("@pass", account.Password));
            parameters.Add(new SqlParameter("@role", Int32.Parse(account.RoleID.ToString())));

            try
            {
                MyExcuteNonQuery(sql, CommandType.Text, parameters);
            }
            catch (SqlException ex)
            {
                throw ex; // Giữ nguyên stack trace gốc
            }
        }
        public Account GetAccount(int id)
        {
            string sql = "SELECT * FROM users WHERE userID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@id", id)
    };

            Account user = null;

            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, parameters); // ✅ truyền parameters
                if (reader.Read())
                {
                    string Id = reader["userID"].ToString();
                    string name = reader["username"].ToString();
                    string pass = reader["upass"].ToString();
                    int roleId = Convert.ToInt32(reader["roleID"]);
                    user = new Account(Id, name, pass, roleId);

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
            return user;
        }


    }
}
