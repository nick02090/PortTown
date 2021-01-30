using DesktopApp.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DesktopApp.Models
{
    public class ProductionBuilding
    {
        public Guid Id { get; set; }
        public  ResourceType ResourceProduced { get; set; }
        public float ProductionRate { get; set; }
        public DateTime LastHarvestTime { get; set; }

        // Parent reference
        public Building ParentBuilding { get; set; }

        public ProductionBuilding()
        {
            // ProductionBuilding
            // TODO: Serialize ResourceProduced
            // TODO: Serialize ProductionRate
            LastHarvestTime = DateTime.UtcNow;
        }

        public ProductionBuilding(Building parentBuilding)
        {
            ParentBuilding = parentBuilding;
        }
    }
}
