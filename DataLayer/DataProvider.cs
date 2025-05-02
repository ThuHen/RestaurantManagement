using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;

namespace DataLayer
{
    public class DataProvider
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        public DataProvider() {
            String connString = "Data Source=LAPTOP-8TFSIJ5P\\SQL;Initial Catalog=RestaurantManager;Integrated Security=True;";
            conn = new SqlConnection(connString);
        }
        public SqlConnection GetConnection()
        {
            return conn;
        }
        public void Connect()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        /// cmd.ExcuteScalar
        public object MyExecuteScalar(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            try
            {
                Connect();
                cmd = new SqlCommand(sql, conn);
                cmd.CommandType = type;

                if (parameters != null)
                {
                    foreach (SqlParameter param in parameters)
                    {
                        cmd.Parameters.Add(param);
                    }
                }

                return cmd.ExecuteScalar();
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


        // 
        public SqlDataReader MyExecuteReader(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (parameters != null)
            {
                foreach (SqlParameter p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            try
            {
                return cmd.ExecuteReader();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public int MyExcuteNonQuery(string sql, CommandType type, List<SqlParameter> parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = type;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }
            }
            try
            {
                Connect();
                return cmd.ExecuteNonQuery();
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
