using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace DataLayer
{
    public class LoginDL : DataProvider
    {
        public Account Login(Account account)
        {
            string sql = "SELECT * FROM users WHERE username = '" + account.Username + "' AND upass = '" + account.Password + "'";
            try
            {

                List<SqlParameter> sp = new List<SqlParameter> {
                    new SqlParameter("@username", account.Username),
                    new SqlParameter("@upass", account.Password)
                };
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, sp);
                if (reader.Read())
                {
                    account.Username = reader["username"].ToString();
                    account.Password = reader["upass"].ToString();
                    account.RoleID = Convert.ToInt32(reader["roleID"]);
                    return account;
                }
                else
                {
                    return null;
                }
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
