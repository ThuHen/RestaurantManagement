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
        OrderBL orderBL;

        public Pos()
        {
            InitializeComponent();
            catBL = new CategoryBL();
            productBL = new ProductBL();
            orderBL = new OrderBL();
        }

        public int MainId = 0;
        public string OrderType = "";
        public int DriverID = 0;
        public string customName = "";
        public string customPhone= "";

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
        private void LoadProducts()
        {
            List<Product> products = productBL.GetProducts();
            foreach (Product p in products)
            {
                Image productImage;

                if (p.Image != null && p.Image is byte[] imageArray)
                {
                    productImage = Image.FromStream(new MemoryStream(imageArray));
                }
                else
                {
                    // Dùng ảnh mặc định nếu không có ảnh
                    productImage = Properties.Resources.food;
                }

                AddItems("0", p.Id.ToString(), p.Name, p.CategoryName.ToString(),
                         p.Price.ToString(), productImage);
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

                guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.id, wdg.name, 1, wdg.price, wdg.price });
                GetTotal();
            };
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
            ClearForm();
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
            if (DriverID == 0 && OrderType == "Delivery")
            {
                guna2MessageDialog1.Show("Please select driver");
                return;
            }
            if ((customName == "" && OrderType == "Delivery") || (customName == "" && OrderType == "Take Away"))
            {
                guna2MessageDialog1.Show("Please select customer");
                return;
            }
            if ((customPhone == "" && OrderType == "Delivery") || (customPhone == "" && OrderType == "Take Away"))
            {
                guna2MessageDialog1.Show("Please select customer phone");
                return;
            }

            // Placeholder for hold logic
            Order order = new Order
            {
                Date = DateTime.Now.Date,
                Time = DateTime.Now.ToShortTimeString(),
                TableName = lblTable.Text,
                WaiterName = lblWaiter.Text,
                Status = "Hold",
                OrderType = OrderType,
                Total = 0,
                Received = 0,
                Change = 0,
                Details = GetOrderDetailsFromGrid(),
                DriverID = DriverID,
                CusName = customName,
                CusPhone= customPhone
            };



            if (order.OrderType=="")
            {
                guna2MessageDialog1.Show("Please select order type");
                return;
            }


            if (order.Details.Count == 0 || order.OrderType == "" || (order.OrderType == "Din In" && (order.TableName == "" || order.WaiterName == "")))
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

        

        private void btnDelivery_Click(object sender, EventArgs e)
        {
            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Delivery";

            AddCustomer frm = new AddCustomer();
            frm.mainID = MainId;
            frm.orderType = OrderType;
            Main.BlurBackGround(Main.Instance(null), frm); // Lấy instance của Main và truyền vào

            if (frm.cusName != "")
            {
                DriverID = frm.driverID;
                customName = frm.cusName;
                customPhone = frm.cusPhone;
                lbDriverName.Text = "Customer Name: " + customName + " Phone: " + customPhone + " Driver: " + frm.cbDriver.Text;
                lbDriverName.Visible = true;
            }
        }

        private void btnTakeAway_Click(object sender, EventArgs e)
        {

            lblTable.Text = "";
            lblWaiter.Text = "";
            lblTable.Visible = false;
            lblWaiter.Visible = false;
            OrderType = "Take Away";

            AddCustomer frm = new AddCustomer();
            frm.mainID = MainId;
            frm.orderType = OrderType;
            Main.BlurBackGround(Main.Instance(null), frm); // Lấy instance của Main và truyền vào

            if (frm.cusName!="")
            {
                DriverID = frm.driverID;
                customName = frm.cusName;
                customPhone = frm.cusPhone;
                lbDriverName.Text = "Customer Name: " + customName + " Phone: " + customPhone;
                lbDriverName.Visible = true;
         
            }
            
        }

        private void btnDinIn_Click(object sender, EventArgs e)
        {
            OrderType = "Din In";
            lbDriverName.Visible = false;
            TableSelect ts = new TableSelect();
            Main.BlurBackGround(Main.Instance(null), ts); // Lấy instance của Main và truyền vào

            if (ts.TableName != "")
            {
                lblTable.Text = ts.TableName;
                lblTable.Visible = true;
            }
            else
            {
                lblTable.Text = "";
                lblTable.Visible = false;
            }

            WaiterSelect ws = new WaiterSelect();
            Main.BlurBackGround(Main.Instance(null), ws); // Lấy instance của Main và truyền vào

            if (ws.WaiterName != "")
            {
                lblWaiter.Text = ws.WaiterName;
                lblWaiter.Visible = true;
            }
            
            else
            {
                lblWaiter.Text = "";
                lblWaiter.Visible = false;

            }
        }

        private void btnKOT_Click(object sender, EventArgs e)
        {
            if (DriverID == 0 && OrderType == "Delivery")
            {
                guna2MessageDialog1.Show("Please select driver");
                return;
            }
            if ((customName == "" && OrderType == "Delivery") || (customName == "" && OrderType == "Take Away"))
            {
                guna2MessageDialog1.Show("Please select customer");
                return;
            }
            if ((customPhone == "" && OrderType == "Delivery") || (customPhone == "" && OrderType == "Take Away"))
            {
                guna2MessageDialog1.Show("Please select customer phone");
                return;
            }

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
                Details = GetOrderDetailsFromGrid(),
                DriverID = DriverID,
                CusName = customName,
                CusPhone = customPhone
            };

            if (order.Details.Count == 0 || order.OrderType == "" || (order.OrderType == "Din In" && (order.TableName == "" || order.WaiterName == "")))
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
            //guna2DataGridView1.Rows.Clear();
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
        public int id = 0;
        private void btnBillList_Click(object sender, EventArgs e)
        {
            BillList frm = new BillList();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (frm.mainID > 0)
                {
                    id = frm.mainID;
                    LoadEntries();
                }
            }

        }
        private void LoadEntries()
        {
            guna2DataGridView1.Rows.Clear();
            List<OrderDetail> ordetails = orderBL.GetOrderDetails(id);
            Order order = orderBL.GetOrder(id);
            OrderType = orderBL.GetOrderType(id);
            foreach (OrderDetail detail in ordetails)

            {
                guna2DataGridView1.Rows.Add(new object[] { 0, 0, detail.ProID, detail.ProName, detail.Qty, detail.Price, detail.Amount });
                //guna2DataGridView1.Rows.Add(new object[] { 0, 0, wdg.id, wdg.name, 1, wdg.price, wdg.price });
            }
            GetTotal();
            if (OrderType == "Delivery")
            {
                btnDelivery.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible = false;
            }
            else if (OrderType == "Take Away")
            {
                btnTakeAway.Checked = true;
                lblWaiter.Visible = false;
                lblTable.Visible = false;
            }
            else if (OrderType   == "Din In")
            {
                btnDinIn.Checked = true;
                lblWaiter.Text = order.WaiterName;
                lblTable.Text = order.TableName;
                lblWaiter.Visible = true;
                lblTable.Visible = true;
            }

        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Checkout frm = new Checkout();
            frm.mainId = id;
            frm.amt = double.Parse(lblTotal.Text);
            Main.BlurBackGround(Main.Instance(null), frm); // Lấy instance của Main và truyền vào
            ClearForm();

        }
    }
}
