using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Order
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string TableName { get; set; }
        public string WaiterName { get; set; }
        public string Status { get; set; }
        public string OrderType { get; set; }
        public double Total { get; set; }
        public double Received { get; set; }
        public double Change { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
