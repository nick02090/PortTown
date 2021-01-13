using Domain.Enums;
using System;

namespace Domain
{
    public class ProductionBuilding : Building
    {
        public virtual ResourceType ResourceProduced { get; set; }
        public virtual int ProductionRate { get; set; }
        public virtual DateTime LastHarvestTime { get; set; }
        public virtual Building ParentBuilding { get; set; }

        public ProductionBuilding()
        {
            // Building
            BuildingType = BuildingType.Production;

            // ProductionBuilding
            // SERIALIZE: ResourceProduced
            // SERIALIZE: ProductionRate
            LastHarvestTime = DateTime.UtcNow;
        }
    }
}
