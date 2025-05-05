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
        public ReportsView()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            Print frm = new Print();
            reportMenu cr = new reportMenu();

            List<Product> products = productBL.GetProducts();

            //cr.SetDatabaseLogon("sa", "lethithuhenai");
            cr.SetDataSource(products);
            frm.crystalReportViewer2.ReportSource = cr;
            frm.crystalReportViewer2.Refresh();
            frm.Show();
        }
    }
}
