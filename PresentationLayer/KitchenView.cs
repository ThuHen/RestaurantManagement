using BussinessLayer;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;

namespace PresentationLayer
{
    public partial class KitchenView : Form
    {
        private OrderBL orderBL;
        public KitchenView()
        {
            InitializeComponent();
            orderBL = new OrderBL();
        }

        private void KitchenView_Load(object sender, EventArgs e)
        {
            LoadOrders();
        }


        private void LoadOrders()
        {
            List<Order> orders = orderBL.GetKitchenOrders(); // Gọi hàm rút gọn cho kitchen
            flowLayoutPanel1.Controls.Clear();
            FlowLayoutPanel p1;

            for (int i = 0; i < orders.Count; i++)
            {
                Order order = orders[i];
                p1 = new FlowLayoutPanel();
                p1.AutoSize = true;
                p1.Width = 230;
                p1.Height = 350;
                p1.FlowDirection = FlowDirection.TopDown;
                p1.BorderStyle = BorderStyle.FixedSingle;
                p1.Margin = new Padding(10, 10, 10, 10);

                FlowLayoutPanel p2 = new FlowLayoutPanel();
                p2 = new FlowLayoutPanel();
                p2.BackColor = Color.FromArgb(50, 55, 89);
                p2.AutoSize = true;
                p2.Width = 230;
                p2.Height = 125;
                p2.FlowDirection = FlowDirection.TopDown;
                p2.Margin = new Padding(0, 0, 0, 0);

                Label lb1 = new Label();
                lb1.ForeColor = Color.White;
                lb1.Margin = new Padding(10, 5, 3, 0);
                lb1.AutoSize = true;
                lb1.Text = "Table: " + order.TableName.ToString();

                Label lb2 = new Label();
                lb2.ForeColor = Color.White;
                lb2.Margin = new Padding(10, 5, 3, 0);
                lb2.AutoSize = true;
                lb2.Text = "Waiter: " + order.WaiterName.ToString();


                Label lb3 = new Label();
                lb3.ForeColor = Color.White;
                lb3.Margin = new Padding(10, 5, 3, 0);
                lb3.AutoSize = true;
                lb3.Text = "Order Time: " + order.Time.ToString();


                Label lb4 = new Label();
                lb4.ForeColor = Color.White;
                lb4.Margin = new Padding(10, 5, 3, 0);
                lb4.AutoSize = true;
                lb4.Text = "Order Type: " + order.OrderType.ToString() ;

                p2.Controls.Add(lb1);
                p2.Controls.Add(lb2);
                p2.Controls.Add(lb3);
                p2.Controls.Add(lb4);

                p1.Controls.Add(p2);


                if (order.Details != null && order.Details.Count > 0)

                {
                    for (int j = 0; j < order.Details.Count; j++)
                    {
                        OrderDetail orderDetail = order.Details[j];
                        Label itemLabel = new Label();
                        itemLabel.ForeColor = Color.Black;
                        itemLabel.Margin = new Padding(10, 0, 3, 0);
                        itemLabel.AutoSize = true;
                        itemLabel.Text = $"{j + 1} {orderDetail.ProName.ToString()} {orderDetail.Qty.ToString()}";
                        p1.Controls.Add(itemLabel);
                    }
                }    
               


                Guna.UI2.WinForms.Guna2Button b = new Guna.UI2.WinForms.Guna2Button();
                b.AutoRoundedCorners = true;
                b.Size = new Size(100, 35);
                b.FillColor = Color.FromArgb(241, 85, 126);
                b.Margin = new Padding(60, 5, 3, 10);
                b.Text = "Complete";
                b.Tag = order.MainID; // Gán ID để xử lý khi nhấn nút
                b.Click += new EventHandler(b_Click);

                p1.Controls.Add(b);
                flowLayoutPanel1.Controls.Add(p1);

            }
        }

        private void b_Click(object sender, EventArgs e)
        {
            var btn = sender as Guna.UI2.WinForms.Guna2Button;

            if (btn != null && btn.Tag != null)
            {
                int mainId = Convert.ToInt32(btn.Tag.ToString());

                // Hộp thoại xác nhận
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Question;
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;

                var result = guna2MessageDialog1.Show("Are you sure you want to complete this order?");
                if (result == DialogResult.Yes)
                {
                    // Gọi hàm BL đã có sẵn
                    orderBL.MarkOrderAsComplete(mainId);

                    // Hộp thoại thông báo thành công
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Show("Saved Successfully");

                    // Reload lại danh sách
                    LoadOrders();
                }
            }
        }


    }
}