﻿using BussinessLayer;
using Guna.UI2.WinForms;
using PresentationLayer.Report;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using TransferObject;

namespace PresentationLayer
{
    public partial class BillList : SampleAdd
    {
        private OrderBL orderBL;
        public BillList()
        {
            InitializeComponent();
            orderBL = new OrderBL();

        }
        public int mainID = 0;

        private void BillList_Load(object sender, EventArgs e)
        {
            GetData();
            // Cột Edit
            DataGridViewImageColumn editCol = new DataGridViewImageColumn();
            editCol.Name = "editcol";
            editCol.HeaderText = "";
            editCol.Image = Properties.Resources.icons8_edit_100;
            editCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvOrders.Columns.Add(editCol);
            editCol.Width = 20;

            // in bill

            DataGridViewImageColumn printCol = new DataGridViewImageColumn();
            printCol.Name = "printcol";
            printCol.HeaderText = "";
            printCol.Image = Properties.Resources.icons8_print_100;
            printCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            dgvOrders.Columns.Add(printCol);
            printCol.Width = 20;
        }

        private void GetData()
        {
            try
            {
                List<Order> orders = orderBL.GetOrdersForBill(); // Gọi từ Business Layer
                dgvOrders.DataSource = orders;

                // Tuỳ chỉnh hiển thị cột
                dgvOrders.Columns["MainID"].HeaderText = "Order ID";
                dgvOrders.Columns["TableName"].HeaderText = "Table";
                dgvOrders.Columns["WaiterName"].HeaderText = "Waiter";
                dgvOrders.Columns["orderType"].HeaderText = "Order  Type";
                dgvOrders.Columns["status"].HeaderText = "Status";
                dgvOrders.Columns["total"].HeaderText = "Total";

                if (dgvOrders.Columns["aDate"] != null)
                    dgvOrders.Columns["aDate"].Visible = false;

                if (dgvOrders.Columns["aTime"] != null)
                    dgvOrders.Columns["aTime"].Visible = true;

                if (dgvOrders.Columns["received"] != null)
                    dgvOrders.Columns["received"].Visible = false;

                if (dgvOrders.Columns["change"] != null)
                    dgvOrders.Columns["change"].Visible = false;
                if (dgvOrders.Columns["driverID"] != null)
                    dgvOrders.Columns["driverID"].Visible = true;
                if (dgvOrders.Columns["cusName"] != null)
                    dgvOrders.Columns["cusName"].Visible = true;
                if (dgvOrders.Columns["cusPhone"] != null)
                    dgvOrders.Columns["cusPhone"].Visible = true;

                if (dgvOrders.Columns["driverID"] != null)
                    dgvOrders.Columns["driverID"].Visible = true;

                if (dgvOrders.Columns["cusName"] != null)
                    dgvOrders.Columns["cusName"].Visible = true;

                if (dgvOrders.Columns["cusPhone"] != null)
                    dgvOrders.Columns["cusPhone"].Visible = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đơn hàng: " + ex.Message);
            }
        }
        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            int col = e.ColumnIndex;
            int row = e.RowIndex;
            //MessageBox.Show("col: " + col + " row: " + row);  
            // Đảm bảo chỉ xử lý khi click vào dòng hợp lệ
            if (row < 0) return;
            string id = dgvOrders.Rows[row].Cells["MainID"].Value.ToString();

            int editColumnIndex = dgvOrders.Columns["editcol"].Index;
            if (col == editColumnIndex)
            {

                Edit(id);
                //MessageBox.Show("Edit");

            }

            int printColumnIndex = dgvOrders.Columns["printcol"].Index;
            if (col == printColumnIndex)
            {
                Print frm = new Print();
                

                mainID = Convert.ToInt32(dgvOrders.CurrentRow.Cells["MainID"].Value);

                List<FullBillDetail> bill = orderBL.GetFullBillDetails(mainID);

                DataTable dt = new DataTable("BillReportTableName");

                dt.Columns.Add("Date", typeof(DateTime));
                dt.Columns.Add("Time", typeof(string));
               
                dt.Columns.Add("OrderType", typeof(string));
                dt.Columns.Add("CusName", typeof(string));
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("TableName", typeof(string));
                dt.Columns.Add("Received", typeof(double));
                dt.Columns.Add("Change", typeof(double));
                
              

               
                dt.Columns.Add("Qty", typeof(int));
                dt.Columns.Add("Amount", typeof(double));
                dt.Columns.Add("Price", typeof(double));
                dt.Columns.Add("WaiterName", typeof(string));

                foreach (var item in bill)
                {
                   dt.Rows.Add(item.Date, item.Time, item.OrderType,item.CusName, item.Name, item.TableName,
                        item.Received, item.Change, item.Qty, item.Amount, item.Price, item.WaiterName);
                }
                
                // Gắn DataSet vào ReportSource

                reportBill rpt = new reportBill();
                rpt.SetDataSource(dt);
                frm.crystalReportViewer2.ReportSource = rpt;
                frm.Show();
            }



        }

        private void Edit(string id)
        {
            // Lấy giá trị tên hiện tại từ dòng đang chọn
            mainID = Convert.ToInt32(dgvOrders.CurrentRow.Cells["MainID"].Value);
            //MessageBox.Show("Edit " + mainID);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
