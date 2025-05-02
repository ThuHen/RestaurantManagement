using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class UcProduct : UserControl
    {

        public UcProduct()
        {
            InitializeComponent();
        }

        public event EventHandler onSelect = null;

        public int id { get; set; }
        public String price { get; set; }

        public String category { get; set; }

        public string name
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }
        public Image image
        {
            get { return txtImage.Image; }
            set { txtImage.Image = value; }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);
        }
    }
}
