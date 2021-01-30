using System;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using System.Linq;
using Domain.Template;
using Domain;
using Domain.Enums;
using System.Collections.Generic;

namespace WebAPI.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository BuildingRepository;
        private readonly ICraftableRepository CraftableRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IProductionBuildingRepository ProductionBuildingRepository;
        private readonly IStorageRepository StorageRepository;
        private readonly IUpgradeableRepository UpgradeableRepository;

        private readonly ITownService TownService;
        private readonly IUpgradeableService UpgradeableService;
        private readonly ICraftableService CraftableService;
        private readonly IProductionBuildingService ProductionBuildingService;

        public BuildingService(IBuildingRepository buildingRepository,
            ICraftableRepository craftableRepository, IResourceBatchRepository resourceBatchRepository, 
            IProductionBuildingRepository productionBuildingRepository, IStorageRepository storageRepository, 
            IUpgradeableRepository upgradeableRepository,
            ITownService townService, IUpgradeableService upgradeableService,
            ICraftableService craftableService, IProductionBuildingService productionBuildingService)
        {
            // Repositories
            BuildingRepository = buildingRepository;
            CraftableRepository = craftableRepository;
            ResourceBatchRepository = resourceBatchRepository;
            ProductionBuildingRepository = productionBuildingRepository;
            StorageRepository = storageRepository;
            UpgradeableRepository = upgradeableRepository;

            // Services
            TownService = townService;
            UpgradeableService = upgradeableService;
            CraftableService = craftableService;
            ProductionBuildingService = productionBuildingService;
        }

        public async Task<Building> GetBuilding(Guid id)
        {
            var building = await BuildingRepository.GetAsync(id);
            building = await UpdateJobs(building);
            return building;
        }

        public async Task<ICollection<Building>> GetBuildingsByTown(Guid townId)
        {
            var buildings = await BuildingRepository.GetByTownAsync(townId);
            var newBuildings = new List<Building>();
            foreach (var building in buildings)
            {
                var newBuilding = await UpdateJobs(building);
                if (newBuilding.BuildingType == BuildingType.Production)
                {
                    // get production child
                    var productionChild = await ProductionBuildingRepository.GetByBuildingAsync(newBuilding.Id);
                    newBuilding.ChildProductionBuilding = productionChild;
                }
                else
                {
                    // get storage child
                    var storageChild = await StorageRepository.GetByBuildingAsync(newBuilding.Id);
                    newBuilding.ChildStorage = storageChild;
                }
                newBuildings.Add(newBuilding);
            }
            return newBuildings;
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

        private async Task PayForAction(ICollection<ResourceBatch> requiredResources, Guid townId)
        {
            var town = await TownService.GetTown(townId);
            foreach (var cost in requiredResources)
            {
                await TownService.GatherPaymentFromBuildings(cost, town.Buildings, true);
                town.Buildings = (ICollection<Building>)await BuildingRepository.GetByTownAsync(townId);
            }
        }

        #region Crafting
        public async Task<Building> CraftBuilding(Building building)
        {
            var craftable = building.ParentCraftable;
            if (craftable.IsFinishedCrafting)
            {
                craftable = await CraftableService.Craft(craftable);
            }
            building.ParentCraftable = craftable;
            return building;
        }

        public async Task<Building> StartCraftBuilding(Building building, Guid townId)
        {
            // NOTE: This check is also made in controller (just in case)
            var canCraft = await CanCraftBuilding(building, townId);
            if ((bool)canCraft["CanCraft"])
            {
                // Remove resources (payment)
                await PayForAction(building.ParentCraftable.RequiredResources, townId);
                // Create the building based on the template and start the crafting process
                building = await AddBuildingToTown(building, townId);
                var craftable = await CraftableService.StartCraft(building.ParentCraftable);
                building.ParentCraftable = craftable;
            }
            return building;
        }

        private async Task<Building> AddBuildingToTown(Building building, Guid townId)
        {
            // Remove the template id
            building.Id = Guid.Empty; // TODO: CHECK THIS OUT
            // Get the craftable parent and remove unnecessary attributes
            var craftable = building.ParentCraftable;
            craftable.RequiredResources = null;
            craftable.Id = Guid.Empty;
            // Create the craftable entity and update the building
            craftable = await CraftableRepository.CreateAsync(craftable);
            building.ParentCraftable = craftable;

            // Get the town entity and update the building
            var town = await TownService.GetTown(townId);
            building.Town = town;

            // Production building
            if (building.BuildingType == BuildingType.Production)
            {
                // Save the child for later usage
                var child = building.ChildProductionBuilding;
                // Create the building entity
                building = await BuildingRepository.CreateAsync(building);
                // Update and create the production building
                child.ParentBuilding = building;
                child.Id = Guid.Empty;
                child = await ProductionBuildingRepository.CreateAsync(child);
                building.ChildProductionBuilding = child;
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
                child.Id = Guid.Empty;
                await StorageRepository.CreateAsync(child);
                building.ChildStorage = child;
            }
            return building;
        }

        public async Task<JSONFormatter> CanCraftBuilding(Building building, Guid townId)
        {
            var craftable = new JSONFormatter();
            craftable.AddField("CanCraft", true);
            var town = await TownService.GetTown(townId);
            var craftCosts = building.ParentCraftable.RequiredResources;
            foreach (var cost in craftCosts)
            {
                var remainingCost = await TownService.GatherPaymentFromBuildings(cost, town.Buildings);
                if (remainingCost > 0)
                {
                    craftable["CanCraft"] = false;
                    break;
                }
            }
            return craftable;
        }
        #endregion

        #region Upgrades
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
                    building.ChildProductionBuilding = await ProductionBuildingService.UpgradeLevel(
                        building.ChildProductionBuilding, upgradeable.UpgradeMultiplier);
                }
                //building.Upgradeable = null; TODO: CHECK THIS OUT
                await BuildingRepository.UpdateAsync(building);
            }
            building.Upgradeable = upgradeable;
            return building;
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
                await PayForAction(building.Upgradeable.RequiredResources, building.Town.Id);
                // Update the upgradable to start the upgrade process
                var upgradeable = await UpgradeableService.StartUpgradeLevel(building.Upgradeable);
                building.Upgradeable = upgradeable;
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
                var remainingCost = await TownService.GatherPaymentFromBuildings(cost, town.Buildings);
                if (remainingCost > 0)
                {
                    upgradeable["CanUpgrade"] = false;
                    break;
                }
            }
            return upgradeable;
        }
        #endregion

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

            // Get upgradeable
            var upgradeable = building.Upgradeable;
            // Save the cost for the upgradeable for later usage
            var upgradeableCosts = upgradeable.RequiredResources;
            upgradeable.RequiredResources = null;

            building.Upgradeable = null;

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

            // Create the upgradeable entity and update the building
            upgradeable = await UpgradeableRepository.CreateAsync(upgradeable);
            building.Upgradeable = upgradeable;
            // Create the upgradeable costs
            foreach (var upgradeableCost in upgradeableCosts)
            {
                upgradeable.Building = building;
                upgradeableCost.Upgradeable = upgradeable;
                await ResourceBatchRepository.CreateAsync(upgradeableCost);
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

                // Get upgradeable
                var upgradeable = building.Upgradeable;
                // Save the cost for the upgradeable for later usage
                var upgradeableCosts = upgradeable.RequiredResources;
                upgradeable.RequiredResources = null;
                building.Upgradeable = null;

                // Create the building entity and update the production building
                building = await BuildingRepository.CreateAsync(building);
                productionBuilding.ParentBuilding = building;
                // Create the production buildling
                await ProductionBuildingRepository.CreateAsync(productionBuilding);


                upgradeable.Building = building;
                // Create the upgradeable entity and update the building
                upgradeable = await UpgradeableRepository.CreateAsync(upgradeable);
                building.Upgradeable = upgradeable;
                // Create the upgradeable costs
                foreach (var upgradeableCost in upgradeableCosts)
                {
                    upgradeableCost.Upgradeable = upgradeable;
                    await ResourceBatchRepository.CreateAsync(upgradeableCost);
                }
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

                // Get upgradeable
                var upgradeable = building.Upgradeable;
                // Save the cost for the upgradeable for later usage
                var upgradeableCosts = upgradeable.RequiredResources;
                upgradeable.RequiredResources = null;
                building.Upgradeable = null;

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

                upgradeable.Building = building;
                // Create the upgradeable entity and update the building
                upgradeable = await UpgradeableRepository.CreateAsync(upgradeable);
                building.Upgradeable = upgradeable;
                // Create the upgradeable costs
                foreach (var upgradeableCost in upgradeableCosts)
                {
                    upgradeableCost.Upgradeable = upgradeable;
                    await ResourceBatchRepository.CreateAsync(upgradeableCost);
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