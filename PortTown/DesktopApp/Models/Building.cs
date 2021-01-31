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
        public string Name { get; set; }
        public int Level { get; set; }
        public int Capacity { get; set; }
        public BuildingType BuildingType { get; set; }
        public Town Town { get; set; }
        public Upgradeable Upgradeable { get; set; }

        // Reference to parent
        public Craftable ParentCraftable { get; set; }
        // References to children
        public Storage ChildStorage { get; set; }
        public ProductionBuilding ChildProductionBuilding { get; set; }

        public Building()
        {
            // TODO: Serialize Name
            Level = 1;
            // TODO: Serialize Capacity
        }

        public Building(Craftable parentCraftable) : base()
        {
            ParentCraftable = parentCraftable;
        }
    }
}
