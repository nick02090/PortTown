using Domain.Enums;
using System;

namespace Domain
{
    public class ProductionBuilding : Building
    {
        public virtual ResourceType ResourceProduced { get; set; }
        public virtual int ProductionRate { get; set; }
        public virtual DateTime LastHarvestTime { get; set; }

        // Parent reference
        public virtual Building ParentBuilding { get; set; }

        public ProductionBuilding()
        {
            // Building
            BuildingType = BuildingType.Production;

            // ProductionBuilding
            // TODO: Serialize ResourceProduced
            // TODO: Serialize ProductionRate
            LastHarvestTime = DateTime.UtcNow;
        }

        public ProductionBuilding(Building parentBuilding)
        {
            SetParentBuilding(parentBuilding);
        }

        public virtual void SetParentBuilding(Building parentBuilding)
        {
            ParentBuilding = parentBuilding;
            Name = parentBuilding.Name;
            Level = parentBuilding.Level;
            Capacity = parentBuilding.Capacity;
            Town = parentBuilding.Town;
        }
    }
}
