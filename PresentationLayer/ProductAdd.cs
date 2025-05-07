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
using BussinessLayer;
using TransferObject;

namespace PresentationLayer
{
    public partial class ProductAdd : SampleAdd
    {
        private ProductBL productBL;
        
        public ProductAdd()
        {
            InitializeComponent();
            productBL = new ProductBL();
        }

        public int id = 0;
        public int catID = 0;
        public Product product = null;
        string filePath;
        Byte[] imageByteArray;
        private void ProductAdd_Load(object sender, EventArgs e)
        {

            CategoryBL catBL = new CategoryBL();
            cbCat.DataSource = catBL.GetCategories();
            cbCat.DisplayMember = "name";
            cbCat.ValueMember = "id";
            cbCat.SelectedIndex = -1;

            if (catID > 0)
            {
                cbCat.SelectedValue = catID;
            }
            if (id > 0)
            {
              
                ForUpdateLoadData();
            }

            //For image
            if (txtImage.Image != null)
            {
                Image temp = new Bitmap(txtImage.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();
            }
            else
            {
                imageByteArray = null; // hoặc gán một ảnh mặc định nếu cần
            }

        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image(.jpg, .png)|*.png; *.jpg";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                txtImage.Image = new Bitmap(filePath);
            }
            Image temp = new Bitmap(txtImage.Image);
            MemoryStream ms = new MemoryStream();
            temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imageByteArray = ms.ToArray();
        }

        public override void btnSave_Click(object sender, EventArgs e)
        {
            if (id > 0)//edit
            {
                if (txtImage.Image != null)
                {
                    Image temp = new Bitmap(txtImage.Image);
                    MemoryStream ms = new MemoryStream();
                    temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imageByteArray = ms.ToArray();
                }
                else
                {
                    imageByteArray = null; // hoặc gán một ảnh mặc định nếu cần
                }
                Product productEdit = new Product(id, txtNameadd.Text, double.Parse(txtPrice.Text), int.Parse(cbCat.SelectedValue.ToString()), imageByteArray);
                productBL.Edit(productEdit);
                this.DialogResult = DialogResult.OK;
            }
            else//save
            {
                string name, price, categoryId;
                name = txtNameadd.Text;
                price = txtPrice.Text;
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(price) || cbCat.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                    return;
                }
                categoryId = cbCat.SelectedValue.ToString();

                Product product = new Product(name, double.Parse(price), int.Parse(categoryId), imageByteArray);
                try
                {
                    int numberOfRows = productBL.Add(product);
                    if (numberOfRows > 0)
                    {
                        id = 0;
                        catID = 0;
                        txtNameadd.Text = "";
                        txtPrice.Text = "";
                        txtImage.Image = null;
                        //cbCat.SelectedIndex = 0;
                        cbCat.SelectedIndex = -1;
                        txtNameadd.Focus();
                        this.DialogResult = DialogResult.OK;
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
            product = productBL.GetProduct(id);
            if (product!= null)
            {
                txtNameadd.Text = product.Name;
                txtPrice.Text = product.Price.ToString();
                //cbCat.SelectedValue = product.CategoryId;

                if (product.Image != null)
                {
                    byte[] imageArray = product.Image;
                    txtImage.Image = Image.FromStream(new MemoryStream(imageArray));
                }
                else
                {
                    txtImage.Image = null; 
                }
            }
        }

        private void txtImage_Click(object sender, EventArgs e)
        {

        }

       
    }
}
