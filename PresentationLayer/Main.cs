using PresentationLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer
{
    public partial class Main : Form
    {
        private Account _account;
        public Main(Account account)
        {
            InitializeComponent();
            _account = account;
        }
        // for accessing Main
        static Main _obj;
        public static Main Instance(Account account)
        {
            if (_obj == null)
            {
                _obj = new Main(account);
            }
            return _obj;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            lbUser.Text = $"Welcome, {_account.Username}!";
            _obj = this;


        }
        public void AddControls(Form frm)
        {
            centerPanel.Controls.Clear();
            frm.Dock = DockStyle.Fill;
            frm.TopLevel = false;
            centerPanel.Controls.Add(frm);
            frm.Show();
        }

        //private void btnHome_Click(object sender, EventArgs e)
        //{
        //    AddControls( new Home());
        //}

        private void btnCategories_Click(object sender, EventArgs e)
        {
            AddControls(new CategoryView());
        }

        //private void btnTable_Click(object sender, EventArgs e)
        //{
        //    AddControls(new TableView());

        //}

        //private void btnStaff_Click(object sender, EventArgs e)
        //{
        //    AddControls(new StaffView());

        //}



        //private void btnSettings_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnKitchen_Click(object sender, EventArgs e)
        //{

        //}


        //private void btnProducts_Click(object sender, EventArgs e)
        //{
        //    AddControls(new ProductView());

        //}

        //private void guna2PictureBox1_Click(object sender, EventArgs e)
        //{

        //}
    }
}
