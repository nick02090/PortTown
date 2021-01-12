using Domain.Enums;
using System;

namespace Domain
{
    public class ProductionBuilding : Building
    {
        public virtual ResourceType ResourceProduced { get; set; }
        public virtual int ProductionRate { get; set; }
        public virtual DateTime LastHarvestTime { get; set; }

        public ProductionBuilding()
        {
            // Craftable
            // SERIALIZE: RequiredResources
            // SERIALIZE: TimeToBuild
            TimeUntilCrafted = null;

            // Building
            // SERIALIZE: Name
            Level = 1;
            // SERIALIZE: Capacity
            BuildingType = BuildingType.Production;

            // ProductionBuilding
            // SERIALIZE: ResourceProduced
            // SERIALIZE: ProductionRate
            LastHarvestTime = DateTime.UtcNow;
        }
    }
}
