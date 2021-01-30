using Domain;
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

        public Task<JSONFormatter> CanHarvest(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<JSONFormatter> Harvest(Guid id)
        {
            throw new NotImplementedException();
        }

        //public async Task<JSONFormatter> CanHarvest(Guid id)
        //{
        //    var productionBuilding = await ProductionBuildingRepository.GetAsync(id);

        //}

        //public async Task<JSONFormatter> Harvest(Guid id)
        //{
        //    // Create the harvest value (based on previous harvest time) and clamp it with the building capacity
        //    var productionBuilding = await ProductionBuildingRepository.GetAsync(id);
        //    var building = productionBuilding.ParentBuilding;
        //}

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