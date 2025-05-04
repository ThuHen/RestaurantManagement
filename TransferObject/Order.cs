    using System;
    using System.Collections.Generic;
    using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace TransferObject
    {
        public class Order
        {
            public int MainID { get; set; }
            public DateTime Date { get; set; }
            public string Time { get; set; }
            public string TableName { get; set; }
            public string WaiterName { get; set; }
            public string Status { get; set; }
            public string OrderType { get; set; }
            public double Total { get; set; }
            public double Received { get; set; }
            public double Change { get; set; }
            public int driverID { get; set; }
            public string CusName { get; set; }

            public string CusPhone { get; set; }


        public List<OrderDetail> Details { get; set; }

        public Order() { }

        // Ví dụ nếu bạn có constructor này trong class Order:
        public Order(int mainID,string tableName, string waiterName, DateTime date, string orderType)
        {
            MainID = mainID;
            TableName = tableName;
            WaiterName = waiterName;
            Date = date;
            OrderType = orderType;
        }


        public Order(int mainID, string tableName, string waiterName, string orderType, string status, double total)
        {
            MainID = mainID;
            TableName = tableName;
            WaiterName = waiterName;
            OrderType = orderType;
            Status = status;
            Total = total;
        }
        public Order( int mainID, DateTime date, string time, string tableName, string waiterName, string status,string orderType, double total,double received, double change )
        {
            MainID = mainID;
            Date = date;
            Time = time;
            TableName = tableName;
            WaiterName = waiterName;
            Status = status;
            OrderType = orderType;
            Total = total;
            Received = received;
            Change = change;
        }

        public Order(int mainID, DateTime date, string time, string tableName, string waiterName, string orderType)
        {
            MainID = mainID;
            Date = date;
            Time = time;
            TableName = tableName;
            WaiterName = waiterName;
            OrderType = orderType;
            
        }

        public Order(int mainID, DateTime date, string time, string tableName, string waiterName, string status, string orderType, double total, double received, double change, int driverId, string cusName, string cusPhone)
        {
            MainID = mainID;
            Date = date;
            Time = time;
            TableName = tableName;
            WaiterName = waiterName;
            Status = status;
            OrderType = orderType;
            Total = total;
            Received = received;
            Change = change;
            driverID = driverId;
            CusName = cusName;
            CusPhone = cusPhone;
        }


    }
}
