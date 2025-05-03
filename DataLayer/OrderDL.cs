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
            string sql = "SELECT * FROM tblMain WHERE Status <> 'Pending'";
            string mainID, date, time, tableName, waiterName, status, orderType, total, received, change;
            
            List<Order> orders = new List<Order>();


            try
            {
                Connect(); // Hàm kết nối CSDL của bạn
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);

                while (reader.Read())
                {
                    mainID = reader[0].ToString();
                    date = reader[1].ToString();
                    time = reader[2].ToString();
                    tableName = reader[3].ToString();
                    waiterName = reader[4].ToString();
                    status = reader[5].ToString();
                    orderType = reader[6].ToString();
                    total = reader[7].ToString();
                    received = reader[8].ToString();
                    change = reader[9].ToString();

                    Order order = new Order(int.Parse(mainID), DateTime.Parse(date), time, tableName, waiterName, status, orderType, Double.Parse(total),Double.Parse( received),Double.Parse( change));
                    order.Details = GetOrderDetails(order.MainID);
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


        public List<OrderDetail> GetOrderDetails(int mainId)
        {
            string sql = @"SELECT d.DetailID, d.ProID, p.ProName, d.qty, d.price, d.amount 
                     FROM tblDetails d 
                     JOIN tblProduct p ON d.ProID = p.ProID 
                     WHERE d.MainID = @MainID";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@MainID", mainId)
    };

            List<OrderDetail> details = new List<OrderDetail>();

            try
            {
                Connect();

                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, parameters);

                while (reader.Read())
                {
                    OrderDetail detail = new OrderDetail
                    {
                        DetailID = Convert.ToInt32(reader["DetailID"]),
                        ProID = Convert.ToInt32(reader["ProID"]),
                        ProName = reader["ProName"].ToString(),
                        Qty = Convert.ToInt32(reader["qty"]),
                        Price = Convert.ToDouble(reader["price"]),
                        Amount = Convert.ToDouble(reader["amount"])
                    };

                    details.Add(detail);
                }
                reader.Close();
                return details;
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

        public void MarkOrderAsComplete(int mainId)
        {
            string sql = "UPDATE tblMain SET Status = 'Complete' WHERE MainID = @MainID";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@MainID", mainId)
    };

            MyExcuteNonQuery(sql, CommandType.Text, parameters);
        }



    }




}
