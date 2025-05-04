using BussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class AddCustomer: Form
    {
        private StaffBL staffBL;

        public AddCustomer()
        {
            InitializeComponent();
            staffBL = new StaffBL();
        }

        public string orderType = "";
        public int driverID = 0;
        public string cusName = "";
        public int mainID= 0;
        public string cusPhone = "";

        private void AddCustomer_Load(object sender, EventArgs e)
        {
            if (orderType== "Take Away")
            {
                lbDriver.Visible = false;
                cbDriver.Visible = false;

            }
           
            StaffBL staffBL = new StaffBL();
            cbDriver.DataSource = staffBL.GetStaffCustomer();
            cbDriver.DisplayMember = "name";
            cbDriver.ValueMember = "id";
            cbDriver.SelectedIndex = -1;

            if (mainID > 0)
            {
                cbDriver.SelectedValue = driverID;
            }
            


        }

        //private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    driverID = Convert.ToInt32(cbDriver.SelectedValue);
        //}

        private void tbnOk_Click(object sender, EventArgs e)
        {
            cusName = txtName.Text;
            cusPhone = txtPhone.Text;
            if (cbDriver.SelectedValue != null)
            {
                driverID = Convert.ToInt32(cbDriver.SelectedValue);
            }
            //this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            cusName = "";
            cusPhone = "";
            driverID = 0;
            this.Close();
        }
    }
}
