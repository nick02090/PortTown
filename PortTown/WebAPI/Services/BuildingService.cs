﻿using System;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using System.Linq;
using Domain.Template;
using Domain;
using Domain.Enums;
using System.Collections.Generic;
using Domain.Utilities;

namespace WebAPI.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository BuildingRepository;
        private readonly ICraftableRepository CraftableRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IProductionBuildingRepository ProductionBuildingRepository;
        private readonly IStorageRepository StorageRepository;

        private readonly ITownService TownService;
        private readonly IUpgradeableService UpgradeableService;
        private readonly ICraftableService CraftableService;

        public BuildingService(IBuildingRepository buildingRepository,
            ICraftableRepository craftableRepository, IResourceBatchRepository resourceBatchRepository, 
            IProductionBuildingRepository productionBuildingRepository, IStorageRepository storageRepository, 
            ITownService townService, IUpgradeableService upgradeableService,
            ICraftableService craftableService)
        {
            // Repositories
            BuildingRepository = buildingRepository;
            CraftableRepository = craftableRepository;
            ResourceBatchRepository = resourceBatchRepository;
            ProductionBuildingRepository = productionBuildingRepository;
            StorageRepository = storageRepository;

            // Services
            TownService = townService;
            UpgradeableService = upgradeableService;
            CraftableService = craftableService;
        }

        public async Task<Building> GetBuilding(Guid id)
        {
            var building = await BuildingRepository.GetAsync(id);
            building = await UpdateJobs(building);
            return building;
        }

        public async Task<ICollection<Building>> GetBuildings()
        {
            var buildings = await BuildingRepository.GetAsync();
            var updatedBuildings = new List<Building>();
            foreach (var building in buildings)
            {
                var updatedBuilding = await UpdateJobs(building);
                updatedBuildings.Add(updatedBuilding);
            }
            return updatedBuildings;
        }

        public async Task<Building> UpdateJobs(Building building)
        {
            var craftable = await CraftableService.UpdateJobs(building.ParentCraftable);
            var upgradeable = await UpgradeableService.UpdateJobs(building.Upgradeable);
            building.ParentCraftable = craftable;
            building.Upgradeable = upgradeable;
            return building;
        }

        public async Task<Building> UpgradeLevel(Building building)
        {
            var upgradeable = building.Upgradeable;
            // NOTE: This check is also made in controller (just in case)
            if (upgradeable.IsFinishedUpgrading)
            {
                upgradeable = await UpgradeableService.UpgradeLevel(upgradeable);
                // Update the building level
                building.Level += 1;
                building.Capacity = CalculateNewCapacity(building.Capacity, upgradeable.UpgradeMultiplier);
                if (building.BuildingType == BuildingType.Production)
                {
                    // TODO: Move this to the Production Building Service!!!!
                    var productionBuilding = building.ChildProductionBuilding;
                    productionBuilding.ProductionRate = CalculateNewProductionRate(productionBuilding.ProductionRate, upgradeable.UpgradeMultiplier);
                    await ProductionBuildingRepository.UpdateAsync(productionBuilding);
                }
                //building.Upgradeable = null; TODO: CHECK THIS OUT
                await BuildingRepository.UpdateAsync(building);
            }
            building.Upgradeable = upgradeable;
            return building;
        }

        private float CalculateNewProductionRate(float productionRate, float upgradeMultiplier)
        {
            var newProductionRate = productionRate * upgradeMultiplier;
            return newProductionRate;
        }

        private int CalculateNewCapacity(int capacity, float upgradeMultiplier)
        {
            var newCapacity = capacity * upgradeMultiplier;
            return (int)newCapacity;
        }

        public async Task<Building> StartUpgradeLevel(Building building)
        {
            // NOTE: This check is also made in controller (just in case)
            var canUpgradeLevel = await CanUpgradeLevel(building);
            if ((bool)canUpgradeLevel["CanUpgrade"])
            {
                // Remove resources (payment)
                building = await PayForLevelUpgrade(building);
                // Update the upgradable to start the upgrade process
                var upgradeable = await UpgradeableService.StartUpgradeLevel(building.Upgradeable);
                building.Upgradeable = upgradeable;
            }
            return building;
        }

        private async Task<Building> PayForLevelUpgrade(Building building)
        {
            var town = await TownService.GetTown(building.Town.Id);
            var upgradeable = building.Upgradeable;
            foreach (var cost in upgradeable.RequiredResources)
            {
                await GatherPaymentFromBuildings(cost, town.Buildings, true);
                town.Buildings = (ICollection<Building>)await BuildingRepository.GetByTownAsync(town.Id);
            }
            return building;
        }

        public async Task<JSONFormatter> CanUpgradeLevel(Building building)
        {
            var upgradeable = new JSONFormatter();
            upgradeable.AddField("CanUpgrade", true);
            var town = await TownService.GetTown(building.Town.Id);
            if (!TownService.DoesTownAllowUpgrade(town, building.Level + 1))
            {
                upgradeable["CanUpgrade"] = false;
                // NOTE: this is an early return to avoid the computational heavy cost calculation
                return upgradeable;
            }
            var upgradeCosts = building.Upgradeable.RequiredResources;
            foreach (var cost in upgradeCosts)
            {
                var remainingCost = await GatherPaymentFromBuildings(cost, town.Buildings);
                if (remainingCost > 0)
                {
                    upgradeable["CanUpgrade"] = false;
                    break;
                }
            }
            return upgradeable;
        }

        /// <summary>
        /// Filters buildings into storage buildings that consist of specific resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="buildings"></param>
        /// <returns></returns>
        public ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings)
        {
            // Get the storage buildings from the sent list
            var storages = buildings.Where(x => x.BuildingType == BuildingType.Storage)
                .Select(x => new Storage
                {
                    Id = x.ChildStorage.Id,
                    StoredResources = x.ChildStorage.StoredResources
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

        /// <summary>
        /// Filters the buildings required for the cost and takes their resources.
        /// Additionally it updates the buildings and returns the remaining cost.
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="buildings"></param>
        /// <returns></returns>
        public async Task<int> GatherPaymentFromBuildings(ResourceBatch cost, ICollection<Building> buildings,
            bool shouldUpdateDb = false)
        {
            var resourceType = cost.ResourceType;
            var storages = FilterStorages(resourceType, buildings);
            var remainingCost = cost.Quantity;
            // Remove the stored resources from the storages and update them accordingly
            foreach (var storage in storages)
            {
                var resources = storage.StoredResources.Where(x => x.ResourceType == resourceType
                                                                && x.Quantity > 0);
                foreach (var resource in resources)
                {
                    var quantity = resource.Quantity;
                    var newQuantity = quantity - MathUtility.Clamp(remainingCost, 0, quantity);
                    resource.Quantity = newQuantity;
                    if (shouldUpdateDb)
                    {
                        await ResourceBatchRepository.UpdateAsync(resource);
                    }
                    var diff = quantity - newQuantity;
                    remainingCost -= diff;
                    if (remainingCost == 0) break;
                }
                if (remainingCost == 0) break;
            }
            return remainingCost;
        }

        #region Template
        public async Task AddDataToTemplate(Building building)
        {
            // Get the craftable parent
            var craftable = building.ParentCraftable;
            // Save the cost for the craftable for later usage
            var craftableCosts = craftable.RequiredResources;
            craftable.RequiredResources = null;
            // Create the craftable entity and update the building
            craftable = await CraftableRepository.CreateAsync(craftable);
            building.ParentCraftable = craftable;
            // Create the craftable costs
            foreach (var craftableCost in craftableCosts)
            {
                craftableCost.Craftable = craftable;
                await ResourceBatchRepository.CreateAsync(craftableCost);
            }

            // Production building
            if (building.BuildingType == BuildingType.Production)
            {
                // Save the child for later usage
                var child = building.ChildProductionBuilding;
                // Create the building entity
                building = await BuildingRepository.CreateAsync(building);
                // Update and create the production building
                child.ParentBuilding = building;
                await ProductionBuildingRepository.CreateAsync(child);
            }
            // Storage
            else
            {
                // Save the child for later usage
                var child = building.ChildStorage;
                // Create the building entity
                building = await BuildingRepository.CreateAsync(building);
                // Update and create the production building
                child.ParentBuilding = building;
                await StorageRepository.CreateAsync(child);
            }
        }

        public async Task AddInitialTemplateData()
        {
            // Create template for production buildings
            foreach (var productionBuilding in ProductionBuildingTemplate.Template())
            {
                // Get the building and the craftable parents
                var building = productionBuilding.ParentBuilding;
                var craftable = building.ParentCraftable;
                // Save the cost for the craftable for later usage
                var craftableCosts = craftable.RequiredResources;
                craftable.RequiredResources = null;
                // Create the craftable entity and update the building
                craftable = await CraftableRepository.CreateAsync(craftable);
                building.ParentCraftable = craftable;
                // Create the craftable costs
                foreach (var craftableCost in craftableCosts)
                {
                    craftableCost.Craftable = craftable;
                    await ResourceBatchRepository.CreateAsync(craftableCost);
                }
                // Create the building entity and update the production building
                building = await BuildingRepository.CreateAsync(building);
                productionBuilding.ParentBuilding = building;
                // Create the production buildling
                await ProductionBuildingRepository.CreateAsync(productionBuilding);
            }
            // Create template for storages
            foreach (var storage in StorageTemplate.Template())
            {
                // Get the building and the craftable parents
                var building = storage.ParentBuilding;
                var craftable = building.ParentCraftable;
                // Save the cost for the craftable for later usage
                var craftableCosts = craftable.RequiredResources;
                craftable.RequiredResources = null;
                // Create the craftable entity and update the building
                craftable = await CraftableRepository.CreateAsync(craftable);
                building.ParentCraftable = craftable;
                // Create the craftable costs
                foreach (var craftableCost in craftableCosts)
                {
                    craftableCost.Craftable = craftable;
                    await ResourceBatchRepository.CreateAsync(craftableCost);
                }
                // Create the building entity and update the storage
                building = await BuildingRepository.CreateAsync(building);
                storage.ParentBuilding = building;
                // Save the storage resources for later usage
                var storageResources = storage.StoredResources;
                storage.StoredResources = null;
                // Create the storage
                var storagedb = await StorageRepository.CreateAsync(storage);
                // Create the storage resources
                foreach (var storageResource in storageResources)
                {
                    storageResource.Storage = storagedb;
                    await ResourceBatchRepository.CreateAsync(storageResource);
                }
            }
        }

        public async Task<JSONFormatter> CheckInitialTemplateData()
        {
            var hasData = new JSONFormatter();
            var buildings = await BuildingRepository.GetTemplateAsync();
            if (buildings.Any())
            {
                hasData.AddField("HasData", true);
            }
            else
            {
                hasData.AddField("HasData", false);
            }
            return hasData;
        }
        #endregion
    }
}