using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class StaffType
    {
        public int typeID { get; set; }
        public string typeName { get; set; }
        public StaffType(int id, string name)
        {
            typeID = id;
            typeName = name;
        }
        public StaffType(string name)
        {
            typeName = name;
        }
    }
    
}
