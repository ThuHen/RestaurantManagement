﻿using Guna.UI2.WinForms;
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
using System.Xml.Linq;
using TransferObject;
using BussinessLayer;

namespace PresentationLayer
{
    public partial class CategoryAdd : SampleAdd
    {
        private CategoryBL categoryBL;
        
        public CategoryAdd()
        {
            InitializeComponent();
            categoryBL = new CategoryBL();
            
        }

        public CategoryAdd(SampleAdd parent)
        {
            InitializeComponent();
        }
        public int id = 0;


        public override void btnSave_Click(object sender, EventArgs e)
        {
            if (id == 0)// insert
            {
                string name;
                name = txtNameadd.Text;
                Category category = new Category(name);
                try
                {
                    int numberOfRows = categoryBL.Add(category);
                    if (numberOfRows > 0)
                    {
                        id = 0;
                        txtNameadd.Text = "";
                        txtNameadd.Focus();
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (SqlException ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            else//update
            {
                string name;
                name = txtNameadd.Text;
                categoryBL.Edit(id, name);
                id = 0;
                txtNameadd.Text = "";
                txtNameadd.Focus();
                this.DialogResult = DialogResult.OK;

            }

        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

        }
    }
}
