using Domain;
using Domain.Enums;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class ProductionBuildingService : IProductionBuildingService
    {
        private readonly IProductionBuildingRepository ProductionBuildingRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;

        public ProductionBuildingService(IProductionBuildingRepository productionBuildingRepository,
            IResourceBatchRepository resourceBatchRepository)
        {
            ProductionBuildingRepository = productionBuildingRepository;
            ResourceBatchRepository = resourceBatchRepository;
        }

        public async Task<JSONFormatter> GetCurrentResources(Guid id)
        {
            var result = new JSONFormatter();
            var currentHarvestValue = await CalculateHarvest(id);
            result.AddField("AccumulatedResources", currentHarvestValue["Harvest"]);
            return result;
        }

        private ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings)
        {
            // Get the storage buildings from the sent list
            var storages = buildings.Where(x => x.BuildingType == BuildingType.Storage)
                .Select(x => new Storage
                {
                    Id = x.ChildStorage.Id,
                    StoredResources = x.ChildStorage.StoredResources,
                    ParentBuilding = new Building
                    {
                        Id = x.Id,
                        Capacity = x.Capacity
                    }
                });
            // Filter storages that can store the required resource type
            var properStorages = new List<Storage>();
            foreach (var storage in storages)
            {
                var resources = storage.StoredResources;
                if (resources.Where(x => x.ResourceType == resourceType).Any())
                {
                    properStorages.Add(storage);
                }
            }
            return properStorages;
        }

        private async Task<int> StoreHarvestIntoStorages(int harvestValue, ResourceType harvestType, 
            ICollection<Storage> storages, bool shouldUpdateDb = false)
        {
            var remainingHarvest = harvestValue;
            // Remove the stored resources from the storages and update them accordingly
            foreach (var storage in storages)
            {
                var storageMaxCapacity = storage.ParentBuilding.Capacity;
                var storageCurrentCapacity = storage.StoredResources.Select(x => x.Quantity).Sum();
                var storageRemainingCapacity = storageMaxCapacity - storageCurrentCapacity;
                if (storageRemainingCapacity <= 0)
                {
                    break;
                }
                var resources = storage.StoredResources.Where(x => x.ResourceType == harvestType);
                foreach (var resource in resources)
                {
                    var quantity = resource.Quantity;
                    var newQuantity = quantity + MathUtility.Clamp(remainingHarvest, 0, storageRemainingCapacity);
                    resource.Quantity = newQuantity;
                    if (shouldUpdateDb)
                    {
                        resource.Storage = storage;
                        await ResourceBatchRepository.UpdateAsync(resource);
                    }
                    var diff = newQuantity - quantity;
                    remainingHarvest -= diff;
                    if (remainingHarvest == 0) break;
                }
                if (remainingHarvest == 0) break;
            }
            return remainingHarvest;
        }

        public async Task<JSONFormatter> CanHarvest(Guid id, ICollection<Building> townBuildings)
        {
            var productionBuilding = await ProductionBuildingRepository.GetAsync(id);
            var harvestType = productionBuilding.ResourceProduced;
            var harvest = await CalculateHarvest(id);
            var harvestValue = (int)harvest["Harvest"];
            var result = new JSONFormatter();
            result.AddField("CanHarvest", true);
            // Filter buildings to show only relevant storages
            var relevantStorages = FilterStorages(harvestType, townBuildings);
            // Check if storages have enough space to put the harvest
            var remainingHarvest = await StoreHarvestIntoStorages(harvestValue, harvestType, relevantStorages);
            if (remainingHarvest > 0)
            {
                result["CanHarvest"] = false;
            }
            return result;
        }

        public async Task Harvest(Guid id, ICollection<Building> townBuildings)
        {
            var productionBuilding = await ProductionBuildingRepository.GetAsync(id);
            var harvest = await CalculateHarvest(id);
            var harvestValue = (int)harvest["Harvest"];
            var harvestType = productionBuilding.ResourceProduced;
            // Filter buildings to show only relevant storages
            var relevantStorages = FilterStorages(harvestType, townBuildings);
            // Store the harvest
            await StoreHarvestIntoStorages(harvestValue, harvestType, relevantStorages, true);
            // Remove accumulated from the production building
            productionBuilding.LastHarvestTime = DateTime.UtcNow;
            await ProductionBuildingRepository.UpdateAsync(productionBuilding);
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