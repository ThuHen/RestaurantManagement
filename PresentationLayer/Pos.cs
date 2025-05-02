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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Forms.VisualStyles;
using System.Collections;

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

        public int MainId = 0;
        public string OrderType;

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

        private void AddItems(string id, String proID, string name, string cat, string p, Image i)
        {
            var w = new UcProduct()
            {
                id = Convert.ToInt32(proID),
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
                    if (Convert.ToInt32(item.Cells["dgvproID"].Value) == wdg.id)
                    {
                        item.Cells["dgvQty"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvQty"].Value.ToString()) *
                                                        double.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GetTotal();
                        return;
                    }
                }

                guna2DataGridView1.Rows.Add(new object[] {0, 0, wdg.id, wdg.name, 1, wdg.price, wdg.price });
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
                AddItems("0",p.Id.ToString(), p.Name, p.CategoryName.ToString(),
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
            if (e.ColumnIndex == guna2DataGridView1.Columns["dgvQty"].Index ||
                e.ColumnIndex == guna2DataGridView1.Columns["dgvAmount"].Index)
            {
                GetTotal();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            guna2DataGridView1.Rows.Clear();
            MainId = 0;
            lblTotal.Text = "0.00";
        }

        private void ClearForm()
        {
            MainId = 0;
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            lblTotal.Text = "0.00";
            guna2DataGridView1.Rows.Clear();
        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            // Placeholder for hold logic
        }

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Away";
        }

        private void btnDinIn_Click(object sender, EventArgs e)
        {
            OrderType = "Din In";
            TableSelect ts = new TableSelect();
            Main.BlurBackGround(Main.Instance(null), ts); // Lấy instance của Main và truyền vào

            if (ts.TableName != "")
            {
                lblTable.Text = ts.TableName;
                lblTable.Visible=true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible=false;
            }

            WaiterSelect ws = new WaiterSelect();
            Main.BlurBackGround(Main.Instance(null), ws); // Lấy instance của Main và truyền vào

            if (ws.WaiterName != "")
            {
                lblWaiter.Text = ws.WaiterName;
                lblWaiter.Visible=true;
            }
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;

            }
        }

        private void btnKOT_Click(object sender, EventArgs e)
        {
            Order order = new Order
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.ToShortTimeString(),
                TableName = lblTable.Text,
                WaiterName = lblWaiter.Text,
                Status = "Pending",
                OrderType = OrderType,
                Total = 0,
                Received = 0,
                Change = 0,
                Details = GetOrderDetailsFromGrid()
            };

            if(order.Details.Count == 0 || order.OrderType == "" || (order.OrderType == "Din In" && (order.TableName == "" || order.WaiterName == "")))
            {
                MessageBox.Show("Can not kot!! Please try again", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int newMainId = OrderBL.SaveOrder(order, MainId);
                if (newMainId > 0)
                {
                    guna2MessageDialog1.Show("Saved Successfully");
                    ClearForm();
                }
            }
        }

        private List<OrderDetail> GetOrderDetailsFromGrid()
        {
            var details = new List<OrderDetail>();
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                details.Add(new OrderDetail
                {
                    DetailID = Convert.ToInt32(row.Cells["dgvId"].Value),
                    ProID = Convert.ToInt32(row.Cells["dgvproID"].Value),
                    Qty = Convert.ToInt32(row.Cells["dgvQty"].Value),
                    Price = Convert.ToDouble(row.Cells["dgvPrice"].Value),
                    Amount = Convert.ToDouble(row.Cells["dgvAmount"].Value)
                });
            }
            return details;
        }

        private void guna2Button_Click(object sender, EventArgs e)
        {
            ProductPanel.Controls.Clear();
            LoadProducts();
        }
    }
}
