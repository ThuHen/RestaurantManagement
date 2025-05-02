using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using TransferObject;

namespace BussinessLayer
{
    public class OrderBL
    {
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
