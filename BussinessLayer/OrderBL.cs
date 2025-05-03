using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class OrderBL
    {
        private OrderDL orderDL;

        public OrderBL()
        {
            orderDL = new OrderDL();
        }

        public void MarkOrderAsComplete(int mainId)
        {
            orderDL.MarkOrderAsComplete(mainId);
        }


        public List<Order> GetOrdersForBill()
        {
            try
            {
                return orderDL.GetOrders();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Order> GetKitchenOrders()
        {
            return orderDL.GetKitchenOrders();
        }

        public bool UpdatePayment(int mainId, decimal total, decimal received, decimal change)
        {
            return orderDL.UpdatePayment(mainId, total, received, change);
        }


        public static int SaveOrder(Order order, int existingMainId)
        {
            OrderDL orderDL = new OrderDL();
            if (existingMainId == 0)
            {

                return orderDL.InsertOrder(order);
            }
            else
            {
                orderDL.UpdateOrder(order, existingMainId);
                return existingMainId;
            }
        }


    }
}
