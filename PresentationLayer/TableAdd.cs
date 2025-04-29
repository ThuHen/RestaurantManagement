using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferObject;
using BussinessLayer;
using System.Data.SqlClient;

namespace PresentationLayer
{
    public partial class TableAdd: SampleAdd
    {
        private TableBL tableBL;
        public TableAdd()
        {
            InitializeComponent();
            tableBL = new TableBL();
        }

        public int id = 0;

        public override void btnSave_Click(object sender, EventArgs e)
        {
            if (id == 0)// insert
            {
                string name;
                name = txtNameadd.Text;
                Table table = new Table(name);
                try
                {
                    int numberOfRows = tableBL.Add(table);
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
                tableBL.Edit(id, name);
                id = 0;
                txtNameadd.Text = "";
                txtNameadd.Focus();
                this.DialogResult = DialogResult.OK;

            }
        }
    }
}
