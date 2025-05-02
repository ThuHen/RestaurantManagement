using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;

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
    }
}
