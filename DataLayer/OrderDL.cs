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
    public class OrderDL : DataProvider
    {
        public int InsertOrder(Order order)
        {
            string query = @"INSERT INTO tblMain 
                            VALUES (@Date, @Time, @TableName, @WaiterName,
                                    @Status, @OrderType, @Total, @Received, @Change, @DriverID, @CusName, @CusPhone);
                            SELECT SCOPE_IDENTITY()";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Date", order.Date),
                new SqlParameter("@Time", order.Time),
                new SqlParameter("@TableName", order.TableName),
                new SqlParameter("@WaiterName", order.WaiterName),
                new SqlParameter("@Status", order.Status),
                new SqlParameter("@OrderType", order.OrderType),
                new SqlParameter("@Total", order.Total),
                new SqlParameter("@Received", order.Received),
                new SqlParameter("@Change", order.Change),
                new SqlParameter("@DriverID", order.DriverID),
                new SqlParameter("@CusName", order.CusName),
                new SqlParameter("@CusPhone", order.CusPhone)
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
                                Status = @Status, 
                                Total = @Total,
                                Received = @Received, 
                                Change = @Change 
                                DriverID = @DriverID,                 
                                CusName = @CusName,
                                CusPhone = @CusPhone
                             WHERE MainID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Status", order.Status),
                new SqlParameter("@Total", order.Total),
                new SqlParameter("@Received", order.Received),
                new SqlParameter("@Change", order.Change),
                new SqlParameter("@DriverID", order.DriverID),
                new SqlParameter("@CusName", order.CusName),
                new SqlParameter("@CusPhone", order.CusPhone),
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
                             VALUES (@MainID, @ProID, @Qty, @Price, @Amount)";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MainID", mainId),
                new SqlParameter("@ProID", detail.ProID),
                new SqlParameter("@Qty", detail.Qty),
                new SqlParameter("@Price", detail.Price),
                new SqlParameter("@Amount", detail.Amount)
            };

            MyExcuteNonQuery(query, CommandType.Text, parameters);
        }

        private void UpdateOrderDetail(OrderDetail detail)
        {
            string query = @"UPDATE tblDetails SET 
                                ProID = @ProID, 
                                Qty = @Qty, 
                                Price = @Price, 
                                Amount = @Amount 
                             WHERE DetailID = @ID";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@ProID", detail.ProID),
                new SqlParameter("@Qty", detail.Qty),
                new SqlParameter("@Price", detail.Price),
                new SqlParameter("@Amount", detail.Amount),
                new SqlParameter("@ID", detail.DetailID)
            };

            MyExcuteNonQuery(query, CommandType.Text, parameters);
        }


        public List<Order> GetOrders()
        {
            string sql = "SELECT * FROM tblMain WHERE Status <> 'Pending'";
            string mainID, date, time, tableName, waiterName, Status, OrderType, Total, Received, Change, DriverID, cusName, cusPhone;

            List<Order> orders = new List<Order>();


            try
            {
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
                        Status = reader[5].ToString();
                        OrderType = reader[6].ToString();
                        Total = reader[7].ToString();
                        Received = reader[8].ToString();
                        Change = reader[9].ToString();
                        DriverID = reader[10].ToString();
                        cusName = reader[11].ToString();
                        cusPhone = reader[12].ToString();

                        Order order = new Order(int.Parse(mainID), DateTime.Parse(date), time, tableName, waiterName, Status, OrderType, Double.Parse(Total), Double.Parse(Received), Double.Parse(Change), int.Parse(DriverID), cusName, cusPhone);
                        //order.Details = GetOrderDetails(order.MainID);
                        orders.Add(order);
                    }
                    reader.Close();
                    return orders;
                }
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
        public List<Order> GetKitchenOrders()
        {
            string sql = "SELECT MainID, Date, Time, TableName, WaiterName, OrderType FROM tblMain WHERE Status <>'Complete'";
            List<Order> orders = new List<Order>();

            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text);

                while (reader.Read())
                {
                    int mainId = Convert.ToInt32(reader["MainID"]);
                    DateTime date = Convert.ToDateTime(reader["Date"]);
                    string time = reader["Time"].ToString();
                    string tableName = reader["TableName"].ToString();
                    string waiterName = reader["WaiterName"].ToString();
                    string OrderType = reader["OrderType"].ToString();

                    // Tạo đối tượng Order đầy đủ với MainID và các trường cần thiết
                    Order order = new Order(mainId, date, time, tableName, waiterName, OrderType);
                    //order.Details = GetOrderDetails(mainId); // nếu bạn vẫn muốn lấy chi tiết món
                    orders.Add(order);

                }

                reader.Close();
                return orders;
            }
            finally
            {
                Disconnect();
            }
        }

        public List<OrderDetail> GetOrderDetails(int mainId)
        {
            string sql = @"SELECT d.DetailID, d.ProID, p.Name, d.Qty, d.Price, d.Amount 
                     FROM tblDetails d 
                     JOIN products p ON d.ProID = p.Id 
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
                        ProName = reader["Name"].ToString(),
                        Qty = Convert.ToInt32(reader["Qty"]),
                        Price = Convert.ToDouble(reader["Price"]),
                        Amount = Convert.ToDouble(reader["Amount"])
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

        public bool UpdatePayment(int mainID, decimal Total, decimal Received, decimal Change)
        {
            string sql = @"UPDATE tblMain SET Total = @Total, Received = @rec, Change = @Change, Status='Paid' WHERE MainID = @id";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@Total", Total),
                new SqlParameter("@rec", Received),
                new SqlParameter("@Change", Change),
                new SqlParameter("@id", mainID)
            };
            try
            {
                Connect();
                int row = MyExcuteNonQuery(sql, CommandType.Text, parameters);
                return row > 0;
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
        public string GetOrderType(int mainId)
        {
            string sql = "SELECT OrderType FROM tblMain WHERE MainID = @MainID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MainID", mainId)
            };
            try
            {
                Connect();
                object result = MyExecuteScalar(sql, CommandType.Text, parameters);
                return result?.ToString();
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

        public Order GetOrder(int mainID)
        {
            string sql = "SELECT * FROM tblMain WHERE MainID = @MainID";
            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@MainID", mainID)
            };
            Order order = null;
            try
            {
                Connect();
                SqlDataReader reader = MyExecuteReader(sql, CommandType.Text, parameters);
                if (reader.Read())
                {
                    order = new Order
                    {
                        MainID = Convert.ToInt32(reader["MainID"]),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Time = reader["Time"].ToString(),
                        TableName = reader["TableName"].ToString(),
                        WaiterName = reader["WaiterName"].ToString(),
                        Status = reader["Status"].ToString(),
                        OrderType = reader["OrderType"].ToString(),
                        Total = Convert.ToDouble(reader["Total"]),
                        Received = Convert.ToDouble(reader["Received"]),
                        Change = Convert.ToDouble(reader["Change"])
                    };

                }
                return order;
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

        public List<FullBillDetail> GetFullBillDetails(int mainId)
        {
            string qry = @"
        SELECT * FROM tblMain m
        JOIN tblDetails d ON d.MainID = m.MainID
        JOIN products p ON p.Id = d.ProID
        WHERE m.MainID = @MainID";

            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@MainID", mainId)
    };
            List<FullBillDetail> details = new List<FullBillDetail>();
            try
            {
                Connect();

                SqlDataReader reader = MyExecuteReader(qry, CommandType.Text, parameters);

                while (reader.Read())
                {
                    FullBillDetail detail = new FullBillDetail
                    {

                        MainID = Convert.ToInt32(reader["MainID"]),
                        Date = Convert.ToDateTime(reader["Date"]),
                        Time = reader["Time"].ToString(),
                        TableName = reader["TableName"].ToString(),
                        WaiterName = reader["WaiterName"].ToString(),
                        CusName = reader["CusName"].ToString(),
                        OrderType = reader["OrderType"].ToString(),
                        Name = reader["Name"].ToString(),
                        Qty = Convert.ToInt32(reader["Qty"]),
                        Price = Convert.ToDouble(reader["Price"]),
                        Amount = Convert.ToDouble(reader["Amount"]),
                        Received = Convert.ToDouble(reader["Received"]),
                        Change = Convert.ToDouble(reader["Change"])

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

    }
}
