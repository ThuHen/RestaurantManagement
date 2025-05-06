using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BussinessLayer
{
    public class StatisticBL
    {
        public DataTable getSalesByCategory(DateTime startDate, DateTime endDate)
        {
            return new StatisticDL().getSalesByCategory(startDate, endDate);
        }
    }
}
