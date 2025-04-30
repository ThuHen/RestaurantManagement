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
    public class StaffCatBL
    {
        private StaffCatDL staffCatDL;
        public StaffCatBL()
        {
            staffCatDL = new StaffCatDL();
        }
        public List<Category> GetStaffCats()
        {
            try
            {
                return staffCatDL.GetStaffCats();
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }
        public int Add(Category category)
        {
            try
            {
                return staffCatDL.Add(category);
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
                staffCatDL.Del(id);
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
                staffCatDL.Edit(id, name);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
