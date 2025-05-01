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
using System.IO;

namespace PresentationLayer
{
    public partial class Pos : Form
    {
        CategoryBL catBL;
        ProductBL productBL;
        public Pos()
        {
            InitializeComponent();
            catBL = new CategoryBL();
            productBL = new ProductBL();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Pos_Load(object sender, EventArgs e)
        {
            guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle;

            AddCategory();
            ProductPanel.Controls.Clear();
            LoadProducts();
        }

        private void AddCategory()
        {
            List<Category> categories = catBL.GetCategories();
            foreach (Category c in categories)
            {
                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.FillColor = Color.FromArgb(50, 55, 89);
                b.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
                b.Text = c.Name;

                b.Click += new EventHandler(b_Click);
                CategoryPanel.Controls.Add(b);
            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button b = (Guna.UI2.WinForms.Guna2Button)sender;
            foreach (var item in ProductPanel.Controls)
            {

                var pro = (UcProduct)item;
                pro.Visible = pro.category.ToLower().Contains(b.Text.Trim().ToLower());
            }
        }

        private void AddItems(int id, string name, string cat, string p, Image i)
        {
            var w = new UcProduct()
            {
                id = id, // Corrected: Use the property of the anonymous object
                name = name,
                price = p,
                category = cat,
                image = i
            };
            ProductPanel.Controls.Add((w));
            w.onSelect += (ss, ee) =>
            {
                var wdg = (UcProduct)ss;

                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (Convert.ToInt32(item.Cells["dgvId"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GetTotal(); // Recalculate total when quantity is updated
                        return;
                    }
                }
                //this line add new product
                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.name, 1, wdg.price, wdg.price });
                GetTotal();
            };
        }

        private void LoadProducts()
        {
            List<Product> products = productBL.GetProducts();
            foreach (Product p in products)
            {
                Byte[] imageArray = (byte[])p.Image;
                byte[] imageByteArray = imageArray;
                AddItems(p.Id, p.Name, p.CategoryName.ToString(),
                         p.Price.ToString(), Image.FromStream(new MemoryStream(imageArray)));
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach (var item in ProductPanel.Controls)
            {
                var pro = (UcProduct)item;
                pro.Visible = pro.name.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        private void GetTotal()
        {
            double total = 0;
            lblTotal.Text = "";
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                total += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lblTotal.Text = total.ToString("N2");
        }

        private void guna2DataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure that the edited cell is either the quantity or the amount column
            if (e.ColumnIndex == guna2DataGridView1.Columns["dgvQty"].Index ||
                e.ColumnIndex == guna2DataGridView1.Columns["dgvAmount"].Index)
            {
                // Recalculate the total
                GetTotal();
            }
        }
    }
}