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
    public class TownService : ITownService
    {
        private readonly ITownRepository townRepository;
        private readonly IBuildingRepository buildingRepository;
        private readonly IItemRepository itemRepository;
        private readonly IUpgradeableRepository upgradeableRepository;
        private readonly IResourceBatchRepository resourceBatchRepository;

        private readonly IBuildingService buildingService;

        public TownService(ITownRepository _townRepository,
            IBuildingRepository _buildingRepository, IItemRepository _itemRepository,
            IUpgradeableRepository _upgradeableRepository, IResourceBatchRepository _resourceBatchRepository,
            IBuildingService _buildingService)
        {
            townRepository = _townRepository;
            buildingRepository = _buildingRepository;
            itemRepository = _itemRepository;
            upgradeableRepository = _upgradeableRepository;
            resourceBatchRepository = _resourceBatchRepository;

            buildingService = _buildingService;
        }

        public async Task<Town> GetTown(Guid id)
        {
            var town = await townRepository.GetAsync(id);
            town = await UpdateJobs(town);
            town.Buildings = await UpdateTownBuildings(town);
            return town;
        }

        public async Task<ICollection<Town>> GetTowns()
        {
            var towns = await townRepository.GetAsync();
            var updatedTowns = new List<Town>();
            foreach (var town in towns)
            {
                var updatedTown = await UpdateJobs(town);
                updatedTown.Buildings = await UpdateTownBuildings(updatedTown);
                updatedTowns.Add(updatedTown);
            }
            return updatedTowns;
        }

        public async Task ResetAsync(Guid id)
        {
            var town = await townRepository.GetAsync(id);
            town.Level = 1;

            foreach(var building in town.Buildings)
            {
                await buildingRepository.DeleteAsync(building.Id);
            }

            foreach (var item in town.Items)
            {
                await itemRepository.DeleteAsync(item.Id);
            }
        }

        /// <summary>
        /// Updates the time-relevant information about the town.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public async Task<Town> UpdateJobs(Town town)
        {
            var upgradeable = town.Upgradeable;
            if (upgradeable.TimeUntilUpgraded.HasValue)
            {
                var timeUntilUpgrade = upgradeable.TimeUntilUpgraded.Value;
                if (timeUntilUpgrade.CompareTo(DateTime.UtcNow) <= 0)
                {
                    upgradeable.TimeUntilUpgraded = null;
                    upgradeable.IsFinishedUpgrading = true;
                    // Store the cost for later purpose
                    var upgradeableCost = upgradeable.RequiredResources;
                    upgradeable.RequiredResources = null;
                    // Update the upgradeable and return the reference to the costs
                    await upgradeableRepository.UpdateAsync(upgradeable);
                    upgradeable.RequiredResources = upgradeableCost;
                }
            }
            town.Upgradeable = upgradeable;
            return town;
        }

        /// <summary>
        /// When the town is finished upgrading user can confirm it's upgrade.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public async Task<Town> UpgradeLevel(Town town)
        {
            var upgradeable = town.Upgradeable;
            // NOTE: This check is also made in controller (just in case)
            if (upgradeable.IsFinishedUpgrading)
            {
                // Update the upgradable properties
                upgradeable.IsFinishedUpgrading = false;
                foreach (var cost in upgradeable.RequiredResources)
                {
                    var quantity = cost.Quantity * upgradeable.UpgradeMultiplier;
                    cost.Quantity = (int)quantity;
                    await resourceBatchRepository.UpdateAsync(cost);
                }
                var ticks = upgradeable.TimeToUpgrade.Ticks * upgradeable.UpgradeMultiplier;
                upgradeable.TimeToUpgrade = new DateTime((long)ticks);
                // Store the cost for later purpose
                var upgradeableCost = upgradeable.RequiredResources;
                upgradeable.RequiredResources = null;
                // Update the upgradeable and return the reference to the costs
                await upgradeableRepository.UpdateAsync(upgradeable);
                upgradeable.RequiredResources = upgradeableCost;
                // Update the town level
                town.Level += 1;
                town.Upgradeable = null;
                await townRepository.UpdateAsync(town);
            }
            town.Upgradeable = upgradeable;
            return town;
        }

        /// <summary>
        /// Starts the process of the town upgrade by taking away the resources.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public async Task<Town> StartUpgradeLevel(Town town)
        {
            // NOTE: This check is also made in controller (just in case)
            var canUpgradeLevel = await CanUpgradeLevel(town);
            if ((bool)canUpgradeLevel["CanUpgrade"])
            {
                // Remove resources (payment)
                town = await PayForLevelUpgrade(town);
                // Update the upgradable to start the upgrade process
                var upgradeable = town.Upgradeable;
                upgradeable.TimeUntilUpgraded = new DateTime(DateTime.UtcNow.Ticks + upgradeable.TimeToUpgrade.Ticks);
                // Store the cost for later purpose
                var upgradeableCost = upgradeable.RequiredResources;
                upgradeable.RequiredResources = null;
                // Update the upgradeable and return the reference to the costs
                await upgradeableRepository.UpdateAsync(upgradeable);
                upgradeable.RequiredResources = upgradeableCost;
                town.Upgradeable = upgradeable;
            }
            return town;
        }

        /// <summary>
        /// Pays for the level upgrade by taking away the required resources.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        private async Task<Town> PayForLevelUpgrade(Town town)
        {
            var upgradeable = town.Upgradeable;
            foreach (var cost in upgradeable.RequiredResources)
            {
                await GatherPaymentFromBuildings(cost, town.Buildings, true);
                town.Buildings = (ICollection<Building>)await buildingRepository.GetByTownAsync(town.Id);
            }
            return town;
        }

        /// <summary>
        /// Filters buildings into storage buildings that consist of specific resource type.
        /// </summary>
        /// <param name="resourceType"></param>
        /// <param name="buildings"></param>
        /// <returns></returns>
        private ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings)
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
        private async Task<int> GatherPaymentFromBuildings(ResourceBatch cost, ICollection<Building> buildings, 
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
                        await resourceBatchRepository.UpdateAsync(resource);
                    }
                    var diff = quantity - newQuantity;
                    remainingCost -= diff;
                    if (remainingCost == 0) break;
                }
                if (remainingCost == 0) break;
            }
            return remainingCost;
        }

        /// <summary>
        /// Determines whether this town can be upgraded based on user resources.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public async Task<JSONFormatter> CanUpgradeLevel(Town town)
        {
            var result = new JSONFormatter();
            result.AddField("CanUpgrade", true);
            var upgradeCosts = town.Upgradeable.RequiredResources;
            foreach (var cost in upgradeCosts)
            {
                var remainingCost = await GatherPaymentFromBuildings(cost, town.Buildings);
                if (remainingCost > 0)
                {
                    result["CanUpgrade"] = false;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Updates the time-relevant information about the town buildings.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        private async Task<ICollection<Building>> UpdateTownBuildings(Town town)
        {
            var buildings = new List<Building>();
            foreach (var building in town.Buildings)
            {
                buildings.Add(await buildingService.UpdateJobs(building));
            }
            return buildings;
        }
    }
}