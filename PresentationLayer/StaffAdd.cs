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
using System.Xml.Linq;
using BussinessLayer;
using TransferObject;

namespace PresentationLayer
{
    public partial class StaffAdd : SampleAdd
    {
        private StaffBL staffBL;
        public StaffAdd()
        {
            InitializeComponent();
            staffBL = new StaffBL();
        }
   
        public int id = 0;
        public int catID = 0;
        public Staff staff = null;
        private void StaffAdd_Load(object sender, EventArgs e)
        {
            StaffCatBL catBL = new StaffCatBL();
            cbRole.DataSource = catBL.GetStaffCats();
            cbRole.DisplayMember = "typeName";
            cbRole.ValueMember = "typeID";
            cbRole.SelectedIndex = -1;

            if (catID > 0)
            {
                cbRole.SelectedValue = catID;
            }
            if (id > 0)
            {

                ForUpdateLoadData();
            }

        }
        public override void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtNameadd.Text;
            string phone = txtPhone.Text;
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(phone) || cbRole.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            string roleId = cbRole.SelectedValue.ToString();

            if (id > 0)//edit
            {
                Staff staffedit = new Staff(id, name, phone, int.Parse(roleId));
                staffBL.Edit(staffedit);
                this.DialogResult = DialogResult.OK;

            }
            else//save
            {
                Staff staff = new Staff(name, phone, int.Parse(roleId));
                try
                {
                    int numberOfRows = staffBL.Add(staff);
                    if (numberOfRows > 0)
                    {
                        id = 0;
                        catID = 0;
                        txtNameadd.Text = "";
                        txtPhone.Text = "";
                        cbRole.SelectedIndex = -1;
                        txtNameadd.Focus();
                        this.DialogResult = DialogResult.OK;
                        this.Close();

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
            staff = staffBL.GetStaff(id);
            if (staff != null)
            {
                txtNameadd.Text = staff.sName;
                txtPhone.Text = staff.sPhone;
            }
            else
            {
                MessageBox.Show("Staff not found.");
            }
        }

        private void btnClose_Click_1(object sender, EventArgs e)
        {

        }

        private void txtNameadd_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
