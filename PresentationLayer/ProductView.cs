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
using System.IO;


namespace PresentationLayer
{
    public partial class ProductView: SampleView
    {
        private ProductBL productBL;
        public ProductView()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        private void ProductView_Load(object sender, EventArgs e)
        {
            GetData();
            // Cột Edit
            DataGridViewImageColumn editCol = new DataGridViewImageColumn();
            editCol.Name = "editcol";
            editCol.HeaderText = "";
            //editCol.Image = Properties.Resources.edit_icon; // <-- icon sửa, cần thêm hình vào Resources
            editCol.Width = 20;
            guna2DataGridView2.Columns.Add(editCol);
            // Cột Delete
            DataGridViewImageColumn deleteCol = new DataGridViewImageColumn();
            deleteCol.Name = "deletecol";
            deleteCol.HeaderText = "";
            //deleteCol.Image = Properties.Resources.delete_icon; // <-- icon xóa, cần thêm hình vào Resources
            deleteCol.Width = 20;
            guna2DataGridView2.Columns.Add(deleteCol);
        }

        public void GetData()
        {
            try
            {
                if (txtSearch.Text != "")
                {
                    guna2DataGridView2.DataSource = productBL.GetProducts().Where(x => x.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
                }
                else
                {
                    guna2DataGridView2.DataSource = productBL.GetProducts();
                }
                guna2DataGridView2.Columns["Name"].HeaderText = "Product Name";
                guna2DataGridView2.Columns["Id"].Visible = false;
                guna2DataGridView2.Columns["CategoryId"].Visible = false;
                guna2DataGridView2.Columns["Image"].Visible = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            ProductAdd form = new ProductAdd();
            DialogResult result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                GetData();
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
            string id = guna2DataGridView2.Rows[row].Cells["id"].Value.ToString();

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
                productBL.Del(id);
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Delete successfully");
                GetData();
            }
        }
        private void Edit(string id)
        {
            // Lấy giá trị tên hiện tại từ dòng đang chọn
            int idCategory = Convert.ToInt32(guna2DataGridView2.CurrentRow.Cells["CategoryId"].Value);
            int idProduct = Convert.ToInt32(guna2DataGridView2.CurrentRow.Cells["Id"].Value);

            // Mở form sửa
            ProductAdd frm = new ProductAdd();
            frm.id = idProduct;
            frm.catID = idCategory;
            DialogResult result = frm.ShowDialog();
            if (result == DialogResult.OK)
            {
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                MessageBox.Show("Update successfully");
                GetData();
            }
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}
