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
        private void ApplyPermissions()
        {
            switch (_account.Role)
            {
                case 1:
                    // full quyền
                    break;

                case 2:
                    btnKitchen.Visible = false;
                    btnStaff.Visible = false;
                    btnCategories.Visible = false;
                    btnProducts.Visible = false;
                    btnTable.Visible = false;
                    btnReports.Visible = false;
                    break;

                case 3:
                    btnPOS.Visible = false;
                    btnStaff.Visible = false;
                    btnCategories.Visible = false;
                    btnProducts.Visible = false;
                    btnTable.Visible = false;
                    btnReports.Visible = false;
                    break;

                default:
                    MessageBox.Show("Không có quyền truy cập!");
                    this.Close();
                    break;
            }
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
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

        private void btnTable_Click(object sender, EventArgs e)
        {
            AddControls(new TableView());

        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            AddControls(new StaffView());
        }



        //private void btnSettings_Click(object sender, EventArgs e)
        //{

        //}

        //private void btnKitchen_Click(object sender, EventArgs e)
        //{

        //}


        private void btnProducts_Click(object sender, EventArgs e)
        {
            AddControls(new ProductView());

        }

        private void btnPOS_Click(object sender, EventArgs e)
        {
            Pos pos = new Pos();
            pos.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {

        }

        //private void guna2PictureBox1_Click(object sender, EventArgs e)
        //{

        //}

        public static void BlurBackGround(Form mainForm, Form Model)
        {
            Form Background = new Form();
            try
            {
                Background.StartPosition = FormStartPosition.Manual;
                Background.FormBorderStyle = FormBorderStyle.None;
                Background.Opacity = 0.5d;
                Background.BackColor = Color.Black;
                Background.Size = mainForm.Size;
                Background.Location = mainForm.Location;
                Background.ShowInTaskbar = false;
                Background.Show();
                Model.Owner = Background;
                Model.ShowDialog(Background); // Sử dụng ShowDialog để Background vẫn hiển thị
            }
            finally
            {
                Background.Dispose(); // Đảm bảo giải phóng tài nguyên của Background
            }
        }

        private void btnKitchen_Click(object sender, EventArgs e)
        {
            AddControls(new KitchenView());
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            AddControls(new ReportsView());
        }
    }
}
