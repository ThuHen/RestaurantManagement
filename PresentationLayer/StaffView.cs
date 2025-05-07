using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BussinessLayer;
using TransferObject;

namespace PresentationLayer
{
    public partial class StaffView : SampleView
    {
        private StaffBL staffBL;
        private StaffCatBL staffCatBL;
        public StaffView()
        {
            InitializeComponent();
            staffBL = new StaffBL();
            staffCatBL = new StaffCatBL();
        }
        int flag = 0;

        private void StaffView_Load(object sender, EventArgs e)
        {
            GetData();

        }
        public void AddColumns()
        {
            // Cột Edit
            DataGridViewImageColumn editCol = new DataGridViewImageColumn();
            editCol.Name = "editcol";
            editCol.HeaderText = "";
            editCol.Image = Properties.Resources.icons8_edit_100; // <-- icon sửa, cần thêm hình vào Resources
            editCol.Width = 40;
            editCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            guna2DataGridView2.Columns.Add(editCol);
            // Cột Delete
            DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
            deleteCol.Name = "deletecol";
            deleteCol.HeaderText = "";
            deleteCol.Image = Properties.Resources.icons8_delete_100; // <-- icon xóa, cần thêm hình vào Resources
            deleteCol.Width = 40;
            deleteCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            guna2DataGridView2.Columns.Add(deleteCol);
        }
        public void GetData()
        {
            guna2DataGridView2.Columns.Clear();

            if (flag == 0)
            {
                try
                {
                    if (txtSearch.Text != "")
                    {
                        guna2DataGridView2.DataSource = staffBL.GetStaffs().Where(x => x.sName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {
                        guna2DataGridView2.DataSource = staffBL.GetStaffs();
                    }

                    guna2DataGridView2.Columns["sID"].Visible = false;
                    guna2DataGridView2.Columns["sRoleID"].Visible = false;
                    guna2DataGridView2.Columns["sName"].HeaderText = "Name";
                    guna2DataGridView2.Columns["sPhone"].HeaderText = "Phone";
                    guna2DataGridView2.Columns["typeName"].HeaderText = "Role";
                    guna2DataGridView2.Columns["sName"].Width = 600;
                    AddColumns();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    if (txtSearch.Text != "")
                    {
                        guna2DataGridView2.DataSource = staffCatBL.GetStaffCats().Where(x => x.typeName.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                    }
                    else
                    {
                        guna2DataGridView2.DataSource = staffCatBL.GetStaffCats();
                    }

                    guna2DataGridView2.Columns["typeID"].Visible = false;
                    guna2DataGridView2.Columns["typeName"].Width = 1000;
                    guna2DataGridView2.Columns["typeName"].HeaderText = "Staff Category Name";
                    AddColumns();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                StaffAdd form = new StaffAdd();
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    GetData();
                }
            }
            else
            {
                StaffCatAdd form = new StaffCatAdd();
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    GetData();
                }
            }
        }

        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void guna2DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            //MessageBox.Show("col: " + col + " row: " + row);
            // Đảm bảo chỉ xử lý khi click vào dòng hợp lệ
            if (row < 0) return;
            string id = guna2DataGridView2.Rows[row].Cells["typeID"].Value.ToString();

            int editColumnIndex = guna2DataGridView2.Columns["editcol"].Index;
            int deleteColumnIndex = guna2DataGridView2.Columns["deletecol"].Index;
            if (col == editColumnIndex)
            {
                // Xử lý sửa
                Edit(id);
                //MessageBox.Show("Edit");

            }
            else if (col == deleteColumnIndex)
            {
                // Xử lý xóa
                Delete(id);
                //MessageBox.Show("Delete");
            }
        }

        private void Delete(string id)
        {
            //need to confirm before delete
            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
            {
                if (flag == 0)
                {
                    staffBL.Del(id);
                }
                else
                {
                    staffCatBL.Del(id);
                }
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Delete successfully");
                GetData();
            }
        }
        private void Edit(string id)
        {
            DialogResult result;
            if (flag == 0)
            {
                int idCatRole = Convert.ToInt32(guna2DataGridView2.CurrentRow.Cells["sRoleID"].Value);
                int idStaff = Convert.ToInt32(id);
                StaffAdd frm = new StaffAdd();
                frm.id = idStaff;
                frm.catID = idCatRole;
                result = frm.ShowDialog();
            }
            else
            {
                string currentName = guna2DataGridView2.CurrentRow.Cells["typeName"].Value.ToString();
                int idCategory = Convert.ToInt32(guna2DataGridView2.CurrentRow.Cells["typeID"].Value);
                StaffCatAdd frm = new StaffCatAdd();
                frm.id = idCategory;
                frm.txtNameadd.Text = currentName;
                result = frm.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Update successfully");
                GetData();
            }
        }

        private void btnStaffCat_Click(object sender, EventArgs e)
        {
            flag = 1;
            GetData();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            flag = 0;
            GetData();
        }
    }
}
