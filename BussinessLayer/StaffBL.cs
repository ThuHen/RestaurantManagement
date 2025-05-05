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
    public class StaffBL
    {
        private StaffDL staffDL;
        public StaffBL()
        {
            staffDL = new StaffDL();
        }
        public Staff GetStaff(int id)
        {
            try
            {
                return staffDL.GetStaff(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public List<Staff> GetStaffs()
        {
            try
            {
                return staffDL.GetStaffs();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Staff> GetStaffCustomer()
        {
            return staffDL.GetStaffCustomer();
        }
        public int Add(Staff staff)
        {
            try
            {
                return staffDL.Add(staff);
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
                staffDL.Del(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Edit(Staff staff)
        {
            try
            {
                staffDL.Edit(staff);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public List<Staff> GetStaffByRole(String role)
        {
            List<Staff> staffList = this.GetStaffs();
            List<Staff> rs = staffList.Where(staff => staff.RoleName == role).ToList();
            return rs;
        }
    }
    
    
   
}
