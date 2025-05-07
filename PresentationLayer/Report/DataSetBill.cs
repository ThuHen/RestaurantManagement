using System;

namespace PresentationLayer.Report
{
    partial class DataSetBill
    {
        partial class StaffReportDataTable
        {
        }

        partial class billReportDataTable
        {
            // Các thuộc tính của DataTable
            public DateTime Date { get; set; }
            public string Time { get; set; }
            public string OrderType { get; set; }
            public string TableName { get; set; }
            public string WaiterName { get; set; }
            public string CusName { get; set; }
            public string Name { get; set; }
            public int Qty { get; set; }
            public double Price { get; set; }
            public double Amount { get; set; }
            public double Received { get; set; }
            public double Change { get; set; }

            // Phương thức thêm một dòng vào DataTable
            public void AddBillReportRow(DateTime date, string time, string orderType, string cusName, string name, string tableName,
                                         double received, double change, int qty, double amount, double price, string waiterName)
            {
                this.Date = date;
                this.Time = time;
                this.OrderType = orderType;
                this.CusName = cusName;
                this.Name = name;
                this.TableName = tableName;
                this.Received = received;
                this.Change = change;
                this.Qty = qty;
                this.Amount = amount;
                this.Price = price;
                this.WaiterName = waiterName;
            }
        }
    }
}
