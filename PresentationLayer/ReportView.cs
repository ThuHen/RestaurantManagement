using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using PresentationLayer.Report;
using TransferObject;

namespace PresentationLayer
{
    public partial class ReportsView : Form
    {
        private ProductBL productBL;
        private StaffBL staffBL;
        public ReportsView()
        {
            InitializeComponent();
            productBL = new ProductBL();
            staffBL = new StaffBL();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Print frm = new Print();
            reportMenu cr = new reportMenu();

            List<Product> products = productBL.GetProducts();

            //cr.SetDatabaseLogon("sa", "");
            cr.SetDataSource(products);
            frm.crystalReportViewer2.ReportSource = cr;
            frm.crystalReportViewer2.Refresh();
            frm.Show();
        }

        private void staffBtn_Click(object sender, EventArgs e)
        {
            Print frm = new Print();
            reportStaff cr = new reportStaff();

            List<Staff> staffs = staffBL.GetStaffs();

            DataTable dt = new DataTable("BillReportTableName");

            dt.Columns.Add("sName", typeof(string));
            dt.Columns.Add("sPhone", typeof(string));

            dt.Columns.Add("typeName", typeof(string));


            foreach (var item in staffs)
            {
                dt.Rows.Add(item.sName, item.sPhone, item.typeName);

            }

            //cr.SetDatabaseLogon("sa", "");
            cr.SetDataSource(staffs);
            frm.crystalReportViewer2.ReportSource = cr;
            frm.crystalReportViewer2.Refresh();
            frm.Show();
        }

        private void btnSaleCat_Click(object sender, EventArgs e)
        {
            SaleByCategory saleByCategory = new SaleByCategory();
            saleByCategory.ShowDialog();

        }
    }
}
