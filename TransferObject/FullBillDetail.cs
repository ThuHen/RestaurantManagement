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
        public string CustomerName { get; set; }
        public int DetailID { get; set; }
        public int ProID { get; set; }
        public string ProName { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }

    }
}
