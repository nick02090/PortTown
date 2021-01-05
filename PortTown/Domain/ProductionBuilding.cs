using Domain.Enums;
using System;
namespace Domain
{
    public class ProductionBuilding : Building
    {

        public ResourceType ResourceProduced { get; set; }
        public float ProductionRate { get; set; }
        public DateTime LastHarvestTime { get; set; }

    }
}
