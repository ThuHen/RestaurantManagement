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
    public partial class CategoryView : SampleView
    {
        private CategoryBL categoryBL;
        public CategoryView()
        {
            InitializeComponent();
            categoryBL = new CategoryBL();
        }

        private void LoadData()
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    guna2DataGridView2.DataSource = categoryBL.GetCategories().Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                else
                {
                    guna2DataGridView2.DataSource = categoryBL.GetCategories();
                }
                //guna2DataGridView2.Columns["Id"].DataPropertyName = "Id";
                guna2DataGridView2.Columns["Id"].Visible = false;
                //guna2DataGridView2.Columns["Name"].DataPropertyName = "Name";
                

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CategoryView_Load(object sender, EventArgs e)
        {
            LoadData();
            // Cột Edit
            DataGridViewImageColumn editCol = new DataGridViewImageColumn();
            //editCol.Image = Properties.Resources.edit_icon; // <-- icon sửa, cần thêm hình vào Resources
            editCol.Width = 20;
            guna2DataGridView2.Columns.Add(editCol);
            // Cột Delete
            DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
            //deleteCol.Image = Properties.Resources.delete_icon; // <-- icon xóa, cần thêm hình vào Resources
            deleteCol.Width = 20;
            guna2DataGridView2.Columns.Add(deleteCol);
            guna2DataGridView2.Columns["Name"].Width = 800;

        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            
            CategoryAdd form = new CategoryAdd();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadData();
            }
        }

        // Edit là cột 0, Delete là cột 1
        int editColumnIndex = 0;
        int deleteColumnIndex = 1;
        private void dataGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int col = e.ColumnIndex;
            int row = e.RowIndex;
            //MessageBox.Show("col: " + col + " row: " + row);
            // Đảm bảo chỉ xử lý khi click vào dòng hợp lệ
            if (row < 0) return;
            string id = guna2DataGridView2.Rows[row].Cells["id"].Value.ToString();

            if (col == editColumnIndex)
            {
                // Xử lý sửa
                Edit(id);

            }
            else if (col == deleteColumnIndex)
            {
                // Xử lý xóa
                Delete(id);
            }
        }
        private void Delete(string id)
        {
            //need to confirm before delete
            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            if (guna2MessageDialog1.Show("Are you sure you want to delete?") == DialogResult.Yes)
            {
                categoryBL.Del(id);
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Delete successfully");
                LoadData();
            }
        }
        private void Edit(string id)
        {
            // Lấy giá trị tên hiện tại từ dòng đang chọn
            string currentName = guna2DataGridView2.CurrentRow.Cells["Name"].Value.ToString();
            int idCategory = Convert.ToInt32(guna2DataGridView2.CurrentRow.Cells["Id"].Value);

            // Mở form sửa
            CategoryAdd frm = new CategoryAdd();
            frm.id = idCategory;
            frm.txtNameadd.Text = currentName;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Update successfully");
                LoadData();
            }
        }
        public override void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}
