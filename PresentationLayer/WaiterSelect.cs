using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using BussinessLayer;

namespace PresentationLayer
{
    public partial class WaiterSelect : Form
    {
        StaffBL staffBL;
        public WaiterSelect()
        {
            InitializeComponent();
            staffBL = new StaffBL();
        }

        public string WaiterName;
        private void WaiterSelect_Load(object sender, EventArgs e)
        {
            List<Staff> staffs = staffBL.GetStaffByRole("Waiter");
            foreach (Staff staff in staffs)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.Text = staff.Name.ToString();
                b.Width = 150;
                b.Height = 50;
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.HoverState.FillColor = Color.FromArgb(50, 55, 89);
                b.Visible = true;

                b.Click += new EventHandler(b_Click);
                flowLayoutPanel1.Controls.Add(b);
            }
        }
        private void b_Click(object sender, EventArgs e)
        {
            WaiterName = (sender as Guna.UI2.WinForms.Guna2Button).Text.ToString();
            this.Close();
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

      

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
