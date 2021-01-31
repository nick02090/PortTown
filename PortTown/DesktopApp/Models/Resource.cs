using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class Resource: TableAddable
    {
        public Guid Id { get; set; }
        public string Name;
        public int Capacity;
        public int Level;

        public Guid townId;

        //public Craftable craftable;
        public Resource(string name)
        {
            Name = name;
        }
    }
}
