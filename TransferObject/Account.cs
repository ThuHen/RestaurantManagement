using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Account
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int RoleID { get; set; } // 1 = Admin, 2 = Cashier, 3 = Kitchen, 4 = Manager

        public Account(string user, string pass, int role)
        {
            this.Username = user;
            this.Password = pass;
            RoleID = role;
        }
        public Account(string user, string pass)
        {
            this.Username = user;
            this.Password = pass;
           
        }
        public Account(string id, string user, string pass, int role)
        {
            this.Id = id;
            this.Username = user;
            this.Password = pass;
            RoleID = role;
        }
    }
}
