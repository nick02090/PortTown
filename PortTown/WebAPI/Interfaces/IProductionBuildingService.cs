using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IProductionBuildingService
    {
        Task<ProductionBuilding> UpgradeLevel(ProductionBuilding productionBuilding, float upgradeMultiplier);
    }
}
