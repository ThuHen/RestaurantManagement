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

        public List<Order> GetOrders()
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
