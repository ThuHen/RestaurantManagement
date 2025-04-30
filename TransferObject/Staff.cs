using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Staff
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public Staff(int id, string name, string phone, int roleId, string roleName)
        {
            ID = id;
            Name = name;
            Phone = phone;
            RoleId = roleId;
            RoleName = roleName;
        }
        public Staff(string name, string phone, int roleId, string roleName)
        {
            Name = name;
            Phone = phone;
            RoleId = roleId;
            RoleName = roleName;
        }
        public Staff(int id, string name, string phone, int roleId)
        {
            ID = id;
            Name = name;
            Phone = phone;
            RoleId = roleId;
          
        }
        public Staff(string name, string phone, int roleId)
        {
            Name = name;
            Phone = phone;
            RoleId = roleId;

        }
    }
  
}
