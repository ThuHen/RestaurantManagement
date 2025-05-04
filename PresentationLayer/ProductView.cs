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
            AddColumns();
            guna2DataGridView2.Columns["Name"].Width = 200;
            guna2DataGridView2.Columns["Price"].Width = 200;
            guna2DataGridView2.Columns["CategoryId"].Visible = false;
            guna2DataGridView2.Columns["Image"].Visible = false;
            guna2DataGridView2.Columns["Id"].Visible = false;
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
                try
                {
                    productBL.Del(id);
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    MessageBox.Show("Delete successfully");
                    GetData();
                }
                catch (SqlException sqlEx)
                {
                    // Kiểm tra mã lỗi để xác định xem có phải là lỗi ràng buộc khóa ngoại (foreign key constraint)
                    if (sqlEx.Number == 547) // Mã lỗi SQL 547 là lỗi ràng buộc khóa ngoại
                    {
                        // Hiển thị thông báo lỗi nếu gặp phải lỗi ràng buộc khóa ngoại
                        MessageBox.Show("Danh mục này đang ràng buộc với các sản phẩm. Không thể xóa được.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Nếu là lỗi khác, hiển thị thông báo lỗi chung
                        MessageBox.Show("Đã có lỗi xảy ra khi xóa danh mục: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
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
