using DesktopApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApp.Models
{
    public class Building: TableAddable
    {
        public Guid Id { get; set; }
        public string Name;
        public int Capacity;
        public BuildingType BuildingType;
        public int Level;

        public Guid townId;

        //public Craftable craftable;
    }
}
