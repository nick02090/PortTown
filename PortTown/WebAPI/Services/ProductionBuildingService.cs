using Domain;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ProductionBuildingService : IProductionBuildingService
    {
        private readonly IProductionBuildingRepository ProductionBuildingRepository;

        public ProductionBuildingService(IProductionBuildingRepository productionBuildingRepository)
        {
            ProductionBuildingRepository = productionBuildingRepository;
        }

        public async Task<ProductionBuilding> UpgradeLevel(ProductionBuilding productionBuilding, float upgradeMultiplier)
        {
            productionBuilding.ProductionRate = CalculateNewProductionRate(productionBuilding.ProductionRate, upgradeMultiplier);
            await ProductionBuildingRepository.UpdateAsync(productionBuilding);
            return productionBuilding;
        }

        private float CalculateNewProductionRate(float productionRate, float upgradeMultiplier)
        {
            var newProductionRate = productionRate * upgradeMultiplier;
            return newProductionRate;
        }
    }
}