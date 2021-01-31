using Domain;
using System;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IProductionBuildingService
    {
        Task<JSONFormatter> CanHarvest(Guid id);
        Task<JSONFormatter> Harvest(Guid id);
        Task<ProductionBuilding> UpgradeLevel(ProductionBuilding productionBuilding, float upgradeMultiplier);
        Task<JSONFormatter> GetCurrentResources(Guid id);
    }
}
