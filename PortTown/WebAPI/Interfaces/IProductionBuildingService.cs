using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IProductionBuildingService
    {
        Task<JSONFormatter> CanHarvest(Guid id, ICollection<Building> townBuildings);
        Task Harvest(Guid id, ICollection<Building> townBuildings);
        Task<ProductionBuilding> UpgradeLevel(ProductionBuilding productionBuilding, float upgradeMultiplier);
        Task<JSONFormatter> GetCurrentResources(Guid id);
    }
}
