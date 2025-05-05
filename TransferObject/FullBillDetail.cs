using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
   public class FullBillDetail
    {
        public int MainID { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }

        public string OrderType { get; set; }
        public string TableName { get; set; }
        public string WaiterName { get; set; }
        public string CusName { get; set; }
        public int DetailID { get; set; }
        public int ProID { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

        public double Received { get; set; }
        public double Change { get; set; }

    }
}
