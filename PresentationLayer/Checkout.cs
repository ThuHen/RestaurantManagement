using BussinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer
{
    partial class Checkout: SampleAdd
    {
        private OrderBL orderBL;
        public Checkout()
        {
            InitializeComponent();
            orderBL = new OrderBL();
        }

        private void Checkout_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

         private void txtChange_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double receipt = 0;
            double change = 0;

            double.TryParse(txtBillAmount.Text, out amt);
            double.TryParse(txtPayment.Text, out amt);

            change = amt - receipt;
            txtChange.Text = change.ToString();
        }
        public int id = 0;
        public override void btnSave_Click(object sender, EventArgs e)
        {
            decimal total = (decimal)double.Parse(txtBillAmount.Text);
            decimal received = (decimal)double.Parse(txtPayment.Text);
            decimal change = (decimal)double.Parse(txtChange.Text);

            if (id == 0) // Thêm mới
            {
                if (orderBL.UpdatePayment(id, total, received, change))
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Thanh toán đã được cập nhật thành công!");
                    this.Close();
                }
            }
              
        }



    


        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
