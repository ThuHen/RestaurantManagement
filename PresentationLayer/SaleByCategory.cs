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
    public partial class SaleByCategory : Form
    {
        public SaleByCategory()
        {
            InitializeComponent();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            Print frm = new Print();
            reportSaleByCategory cr = new reportSaleByCategory();

            DateTime startDate = Convert.ToDateTime(dateTimePicker1.Value).Date;
            DateTime endDate = Convert.ToDateTime(dateTimePicker2.Value).Date;
            DataTable data = new StatisticBL().getSalesByCategory(startDate, endDate);

            cr.SetDatabaseLogon("sa", "lethithuhenai");
            cr.SetDataSource(data);
            frm.crystalReportViewer2.ReportSource = cr;
            frm.crystalReportViewer2.Refresh();
            frm.Show();
        }
    }
}
