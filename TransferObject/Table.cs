using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransferObject
{
    public class Table
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public Table(string id, string name)
        {
            Id = id;
            Name = name;
        }
        public Table(string name)
        {
            Name = name;
        }
    } 
}
