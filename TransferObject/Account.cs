using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public int Role { get; set; } // 1 = Admin, 2 = Cashier, 3 = Kitchen, 4 = Manager

        public Account(string user, string pass, int role)
        {
            this.Username = user;
            this.Password = pass;
            Role = role;
        }
        public Account(string user, string pass)
        {
            this.Username = user;
            this.Password = pass;
           
        }
    }
}
