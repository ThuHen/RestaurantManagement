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
using TransferObject;

namespace PresentationLayer
{
    public partial class KitchenView: Form
    {
        private OrderBL orderBL;
        public KitchenView()
        {
            InitializeComponent();
            orderBL = new OrderBL();
        }

        private void KitchenView_Load(object sender, EventArgs e)
        {
            
        }


        private void LoadOrders()
        {

            flowLayoutPanel1.Controls.Clear();
            
        }
    }
}
