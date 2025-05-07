using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Staff
    {
        public int sID { get; set; }
        public string sName { get; set; }
        public string sPhone { get; set; }
        public int sRoleID { get; set; }
        public string typeName { get; set; }


        public Staff(int sID, string sName, string sPhone, int sRoleID, string sRoleName)
        {
            this.sID = sID;
            this.sName = sName;
            this.sPhone = sPhone;
            this.sRoleID = sRoleID;
            this.typeName = sRoleName;
        }
        public Staff(string sName, string sPhone, int sRoleID, string sRoleName)
        {
            this.sName = sName;
            this.sPhone = sPhone;
            this.sRoleID = sRoleID;
            this.typeName = sRoleName;
        }
        public Staff(int sID, string sName, string sPhone, int sRoleID)
        {
            this.sID = sID;
            this.sName = sName;
            this.sPhone = sPhone;
            this.sRoleID = sRoleID;
          
        }
        public Staff(string sName, string sPhone, int sRoleID)
        {
            this.sName = sName;
            this.sPhone = sPhone;
            this.sRoleID = sRoleID;

        }

        public Staff(int sID, string sName)
        {

            this.sID = sID;
            this.sName = sName;
        }
    }
  
}
