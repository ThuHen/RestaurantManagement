﻿using System;
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
        public List<OrderDetail>  GetOrderDetails(int mainId)
        {
            return orderDL.GetOrderDetails(mainId);
        }
        public string GetOrderType(int mainId)
        {
            return orderDL.GetOrderType(mainId);
        }
        public Order GetOrder(int mainId)
        {
            return orderDL.GetOrder(mainId);
        }
        public bool UpdatePayment(int mainId, decimal total, decimal received, decimal change)
        {
            return orderDL.UpdatePayment(mainId, total, received, change);
        }
        public List<FullBillDetail> GetFullBillDetails(int mainId)
        {
            return orderDL.GetFullBillDetails(mainId);
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
