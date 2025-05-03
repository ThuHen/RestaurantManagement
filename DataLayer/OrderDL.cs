using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;
using System.Xml.Linq;

namespace DataLayer
{
    public class OrderDL:DataProvider
    {
        public int InsertOrder(Order order)
        {
            string query = @"INSERT INTO tblMain 
                            VALUES (@aDate, @aTime, @TableName, @WaiterName,
                                    @status, @orderType, @total, @received, @change);
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@aDate", order.Date),
                new SqlParameter("@aTime", order.Time),
                new SqlParameter("@TableName", order.TableName),
                new SqlParameter("@WaiterName", order.WaiterName),
                new SqlParameter("@status", order.Status),
                new SqlParameter("@orderType", order.OrderType),
                new SqlParameter("@total", order.Total),
                new SqlParameter("@received", order.Received),
                new SqlParameter("@change", order.Change)
            };

            object result = MyExecuteScalar(query, CommandType.Text, parameters);
            int mainId = Convert.ToInt32(result);

            foreach (var detail in order.Details)
            {
                InsertOrderDetail(detail, mainId);
            }

            return mainId;
        }

        public void UpdateOrder(Order order, int mainId)
        {
            string query = @"UPDATE tblMain SET 
                                status = @status, 
                                total = @total,
                                received = @received, 
                                change = @change 
                             WHERE MainID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@status", order.Status),
                new SqlParameter("@total", order.Total),
                new SqlParameter("@received", order.Received),
                new SqlParameter("@change", order.Change),
                new SqlParameter("@ID", mainId)
            };

            MyExcuteNonQuery(query, CommandType.Text, parameters);

            foreach (var detail in order.Details)
            {
                if (detail.DetailID == 0)
                    InsertOrderDetail(detail, mainId);
                else
                    UpdateOrderDetail(detail);
            }
        }

        private void InsertOrderDetail(OrderDetail detail, int mainId)
        {
            string query = @"INSERT INTO tblDetails 
                             VALUES (@MainID, @ProID, @qty, @price, @amount)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MainID", mainId),
                new SqlParameter("@ProID", detail.ProID),
                new SqlParameter("@qty", detail.Qty),
                new SqlParameter("@price", detail.Price),
                new SqlParameter("@amount", detail.Amount)
            };

            MyExcuteNonQuery(query, CommandType.Text, parameters);
        }

        private void UpdateOrderDetail(OrderDetail detail)
        {
            string query = @"UPDATE tblDetails SET 
                                ProID = @ProID, 
                                qty = @qty, 
                                price = @price, 
                                amount = @amount 
                             WHERE DetailID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProID", detail.ProID),
                new SqlParameter("@qty", detail.Qty),
                new SqlParameter("@price", detail.Price),
                new SqlParameter("@amount", detail.Amount),
                new SqlParameter("@ID", detail.DetailID)
            };

            MyExcuteNonQuery(query, CommandType.Text, parameters);
        }

       
           public List<Order> GetOrders()
        {
            string sql = "SELECT MainID, TableName, WaiterName, OrderType, Status, Total FROM tblMain WHERE Status <> 'Pending'";
            List<Order> orders = new List<Order>();

            try
            {
                Connect(); // Hàm kết nối CSDL của bạn
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);

                while (reader.Read())
                {
                    int mainID = Convert.ToInt32(reader["MainID"]);
                    string tableName = reader["TableName"]?.ToString();
                    string waiterName = reader["WaiterName"]?.ToString();
                    string orderType = reader["OrderType"]?.ToString();
                    string status = reader["Status"]?.ToString();
                    double total = reader["Total"] != DBNull.Value ? Convert.ToDouble(reader["Total"]) : 0.0;

                    Order order = new Order(mainID, tableName, waiterName, orderType, status, total);
                    orders.Add(order);
                }
                reader.Close();
                return orders;
            }
            catch (SqlException ex)
            {
                throw ex; // Có thể log hoặc xử lý chi tiết hơn
            }
            finally
            {
                Disconnect(); // Đóng kết nối
            }
        }

    }




}
