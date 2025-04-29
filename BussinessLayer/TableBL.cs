using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransferObject;
using DataLayer;
using System.Data.SqlClient;

namespace BussinessLayer
{
    public class TableBL
    {
        private TableDL TableDL;
        public TableBL()
        {
            TableDL = new TableDL();
        }
        public List<Table> GetTables()
        {
            try
            {
                return TableDL.GetTables();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public int Add(Table Table)
        {
            try
            {
                return TableDL.Add(Table);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Del(string id)
        {
            try
            {
                TableDL.Del(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Edit(int id, string name)
        {
            try
            {
                TableDL.Edit(id, name);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
