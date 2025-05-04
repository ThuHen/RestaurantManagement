using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PresentationLayer;
using TransferObject;
using BussinessLayer;

namespace PresentationLayer
{
    public partial class Login : Form
    {
        private LoginBL loginBL;
        public Login()
        {
            InitializeComponent();
            loginBL = new LoginBL();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private Account ValidateLogin(Account account)
        {
            try
            {
                return loginBL.Login(account);               
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            Account account = new Account(username, password);
            Account currentUser = ValidateLogin(account);
            if (currentUser != null)
            {
                this.Hide();
                Main frm = new Main(currentUser);
                frm.Show();
            }
            else
            {
                guna2MessageDialog1.Show("Invalid username and password");
                return;
            }
        }
    }
}
