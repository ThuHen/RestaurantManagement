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
    public class UserBL
    {
        private UserDL userDL;
        public UserBL()
        {
            userDL = new UserDL();
        }
        public List<Account> GetAccounts()
        {
            try
            {
                return userDL.GetAccounts();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public int Add(Account user)
        {
            try
            {
                return userDL.Add(user);
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
                userDL.Del(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public void Update(Account account)
        {
            try
            {
                userDL.Update(account);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
        public Account GetAccount(int id)
        {
            try
            {
                return userDL.GetAccount(id);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}