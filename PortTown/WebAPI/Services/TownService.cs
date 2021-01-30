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
        private readonly ITownRepository TownRepository;
        private readonly IBuildingRepository BuildingRepository;
        private readonly IItemRepository ItemRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IUpgradeableRepository UpgradeableRepository;
        private readonly ICraftableRepository CraftableRepository;
        private readonly IStorageRepository StorageRepository;
        private readonly IProductionBuildingRepository ProductionBuildingRepository;

        private readonly IUpgradeableService UpgradeableService;

        public TownService(ITownRepository townRepository,
            IBuildingRepository buildingRepository, IItemRepository itemRepository,
            IResourceBatchRepository resourceBatchRepository, IUpgradeableRepository upgradeableRepository,
            ICraftableRepository craftableRepository, IUpgradeableService upgradeableService,
            IStorageRepository storageRepository, IProductionBuildingRepository productionBuildingRepository)
        {
            // Repositories
            TownRepository = townRepository;
            BuildingRepository = buildingRepository;
            ItemRepository = itemRepository;
            ResourceBatchRepository = resourceBatchRepository;
            UpgradeableRepository = upgradeableRepository;
            CraftableRepository = craftableRepository;
            StorageRepository = storageRepository;
            ProductionBuildingRepository = productionBuildingRepository;

            // Services
            UpgradeableService = upgradeableService;
        }

        public async Task<Town> GetTown(Guid id)
        {
            var town = await TownRepository.GetAsync(id);
            town = await UpdateJobs(town);
            return town;
        }

        public async Task<ICollection<Town>> GetTowns()
        {
            var towns = await TownRepository.GetAsync();
            var updatedTowns = new List<Town>();
            foreach (var town in towns)
            {
                var updatedTown = await UpdateJobs(town);
                updatedTowns.Add(updatedTown);
            }
            return updatedTowns;
        }

        public async Task<Town> CreateFromTemplate(Town template)
        {
            // Save buildings and upgradeable for later usage
            var buildings = template.Buildings;
            var upgradeable = template.Upgradeable;
            template.Buildings = null;
            template.Upgradeable = null;
            // Create town
            var town = await TownRepository.CreateAsync(template);

            // Create buildings from template
            var townBuildings = new List<Building>();
            foreach (var building in buildings)
            {
                var townBuilding = await BuildingRepository.GetTemplateAsync(building.BuildingType, building.Name);
                var oldBuildingId = townBuilding.Id;
                // Create craftable for this building
                var buildingCraftable = townBuilding.ParentCraftable;
                buildingCraftable.Id = Guid.Empty;
                var buildingCraftableCosts = buildingCraftable.RequiredResources;
                buildingCraftable.RequiredResources = null;
                buildingCraftable = await CraftableRepository.CreateAsync(buildingCraftable);
                // Create resources for the craftable
                foreach (var craftableCost in buildingCraftableCosts)
                {
                    craftableCost.Id = Guid.Empty;
                    craftableCost.Craftable = buildingCraftable;
                    await ResourceBatchRepository.CreateAsync(craftableCost);
                }
                // Create the building
                townBuilding.Id = Guid.Empty;
                townBuilding.ParentCraftable = buildingCraftable;
                townBuilding.Town = town;
                townBuilding = await BuildingRepository.CreateAsync(townBuilding);
                townBuildings.Add(townBuilding);
                // Create the upgradable for this building
                var buildingUpgradeable = townBuilding.Upgradeable;
                buildingUpgradeable.Id = Guid.Empty;
                buildingUpgradeable.Building = townBuilding;
                var buildingUpgradeableCosts = buildingUpgradeable.RequiredResources;
                buildingUpgradeable.RequiredResources = null;
                buildingUpgradeable = await UpgradeableRepository.CreateAsync(buildingUpgradeable);
                // Create resources for the upgradeable
                foreach (var upgradeableCost in buildingUpgradeableCosts)
                {
                    upgradeableCost.Id = Guid.Empty;
                    upgradeableCost.Upgradeable = buildingUpgradeable;
                    await ResourceBatchRepository.CreateAsync(upgradeableCost);
                }
                if (townBuilding.BuildingType == BuildingType.Production)
                {
                    // Create the production for this building
                    var productionBuilding = await ProductionBuildingRepository.GetByBuildingAsync(oldBuildingId);
                    productionBuilding.Id = Guid.Empty;
                    productionBuilding.ParentBuilding = townBuilding;
                    await ProductionBuildingRepository.CreateAsync(productionBuilding);
                }
                else
                {
                    // Create the storage for this building
                    var storage = await StorageRepository.GetByBuildingAsync(oldBuildingId);
                    storage.Id = Guid.Empty;
                    var storageResources = storage.StoredResources;
                    storage.StoredResources = null;
                    storage.ParentBuilding = townBuilding;
                    storage = await StorageRepository.CreateAsync(storage);
                    foreach (var storageCost in storageResources)
                    {
                        storageCost.Id = Guid.Empty;
                        storageCost.Storage = storage;
                        await ResourceBatchRepository.CreateAsync(storageCost);
                    }
                }
            }

            // Create upgradeable
            upgradeable.Town = town;
            upgradeable = await UpgradeableRepository.CreateAsync(upgradeable);

            // Update and return the town
            town.Buildings = townBuildings;
            town.Upgradeable = upgradeable;
            return town;
        }

        public async Task ResetAsync(Guid id)
        {
            var town = await TownRepository.GetAsync(id);
            town.Level = 1;

            foreach(var building in town.Buildings)
            {
                await BuildingRepository.DeleteAsync(building.Id);
            }

            foreach (var item in town.Items)
            {
                await ItemRepository.DeleteAsync(item.Id);
            }
        }

        /// <summary>
        /// Updates the time-relevant information about the town.
        /// </summary>
        /// <param name="town"></param>
        /// <returns></returns>
        public async Task<Town> UpdateJobs(Town town)
        {
            var upgradeable = await UpgradeableService.UpdateJobs(town.Upgradeable);
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
                upgradeable = await UpgradeableService.UpgradeLevel(upgradeable);
                // Update the town level
                town.Level += 1;
                //town.Upgradeable = null; TODO: CHECK THIS OUT
                await TownRepository.UpdateAsync(town);
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
                var upgradeable = await UpgradeableService.StartUpgradeLevel(town.Upgradeable);
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
                town.Buildings = (ICollection<Building>)await BuildingRepository.GetByTownAsync(town.Id);
            }
            return town;
        }

        /// <summary>
        /// Returns true/false based on the towns current level and the wanted level.
        /// </summary>
        /// <param name="town"></param>
        /// <param name="nextLevel"></param>
        /// <returns></returns>
        public bool DoesTownAllowUpgrade(Town town, int nextLevel)
        {
            if (nextLevel > town.Level)
            {
                return false;
            }
            return true;
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
    }
}