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
        public Building(string name, int capacity, int level)
        {
            // TODO: Remove this when connected to API
            Id = Guid.NewGuid();

            Name = name;
            Capacity = capacity;
            Level = level;
        }
    }
}
