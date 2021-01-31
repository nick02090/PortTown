using Domain;
using Domain.Utilities;
using System;
using System.Threading.Tasks;
using WebAPI.Helpers;
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

        public async Task<JSONFormatter> GetCurrentResources(Guid id)
        {
            var result = new JSONFormatter();
            var currentHarvestValue = await CalculateHarvest(id);
            result.AddField("AccumulatedResources", currentHarvestValue["Harvest"]);
            return result;
        }

        public Task<JSONFormatter> Harvest(Guid id)
        {
            throw new NotImplementedException();
        }

        private async Task<JSONFormatter> CalculateHarvest(Guid id)
        {
            var productionBuilding = await ProductionBuildingRepository.GetAsync(id);
            // Create the harvest value (based on previous harvest time) and clamp it with the building capacity
            var maxCapacity = productionBuilding.ParentBuilding.Capacity;
            var productionRate = productionBuilding.ProductionRate;
            var now = DateTime.UtcNow;
            var lastHarvest = productionBuilding.LastHarvestTime;
            var diffSeconds = (now - lastHarvest).TotalSeconds;
            var createdResources = MathUtility.Clamp((int)(productionRate * diffSeconds), 0, maxCapacity);
            var harvest = new JSONFormatter();
            harvest.AddField("Harvest", createdResources);
            return harvest;
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