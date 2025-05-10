using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace PresentationLayer
{
    public partial class UserAdd : SampleAdd
    {
        private UserBL userBL;

        public UserAdd()
        {
            InitializeComponent();
            userBL = new UserBL();
        }

        public int id = 0;
        public int catID = 0;
        public Account user = null;
        private void UserAdd_Load(object sender, EventArgs e)
        {
            if (catID > 0)
            {
                cbCat.SelectedIndex = catID-1;
            }
            if (id > 0)
            {
                ForUpdateLoadData();
            }


        }


        public override void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPass.Text;
            int categoryId = 0;



            string category = cbCat.SelectedItem.ToString();
            switch (category)
            {
                case "admin":
                    categoryId = 1;
                    break;
                case "casher":
                    categoryId = 2;
                    break;
                case "kitchen":
                    categoryId = 3;
                    break;
                case "manager":
                    categoryId = 4;
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || categoryId == 0)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            Account useredit = new Account(id.ToString(), username, password, categoryId);
            if (id > 0)//edit
            {
                
                userBL.Update(useredit);
                this.DialogResult = DialogResult.OK;
            }
            else//save
            {
                Account user = new Account(username, password, categoryId);
                try
                {
                    int numberOfRows = userBL.Add(user);
                    if (numberOfRows > 0)
                    {
                        id = 0;
                        //catID = 0;
                        txtUserName.Text = "";
                        txtPass.Text = "";
                        
                        
                        cbCat.SelectedIndex = -1;
                        txtUserName.Focus();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ForUpdateLoadData()
        {
            user = userBL.GetAccount(id);
            if (user != null)
            {
                txtUserName.Text = user.Username;
                txtPass.Text = user.Password;
                //cbCat.SelectedValue = user.CategoryID;

                
            }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {

        }


    }
}
