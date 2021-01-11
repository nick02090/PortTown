using Domain.Enums;
using System;
namespace Domain
{
    public class ProductionBuilding : Building
    {
        public virtual ResourceType ResourceProduced { get; set; }
        public virtual float ProductionRate { get; set; }
        public virtual DateTime LastHarvestTime { get; set; }
    }
}
