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
    public class LoginBL
    {
        private LoginDL loginDL;
        public LoginBL()
        {
            loginDL = new LoginDL();
        }
        public bool Login(Account account)
        {
            try
            {
                return (loginDL.Login(account));
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
    }
}
