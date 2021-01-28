using System;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Interfaces;
using System.Linq;
using Domain.Template;
using Domain;
using Domain.Enums;

namespace WebAPI.Services
{
    public class BuildingService : IBuildingService
    {
        private readonly ITownRepository TownRepository;
        private readonly IBuildingRepository BuildingRepository;

        private readonly ICraftableRepository CraftableRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;
        private readonly IProductionBuildingRepository ProductionBuildingRepository;
        private readonly IStorageRepository StorageRepository;
        private readonly IUpgradeableRepository UpgradeableRepository;

        public BuildingService(ITownRepository townRepository, IBuildingRepository buildingRepository,
            ICraftableRepository craftableRepository, IResourceBatchRepository resourceBatchRepository,
            IProductionBuildingRepository productionBuildingRepository, IStorageRepository storageRepository,
            IUpgradeableRepository upgradeableRepository)
        {
            TownRepository = townRepository;
            BuildingRepository = buildingRepository;

            CraftableRepository = craftableRepository;
            ResourceBatchRepository = resourceBatchRepository;
            ProductionBuildingRepository = productionBuildingRepository;
            StorageRepository = storageRepository;
            UpgradeableRepository = upgradeableRepository;
        }

        public async Task<JSONFormatter> CanUpgrade(Guid id)
        {
            var upgradeable = new JSONFormatter();
            var building = await BuildingRepository.GetAsync(id);

            var town = await TownRepository.GetAsync(building.Town.Id);

            if (building.Level + 1 > town.Level)
            {
                upgradeable.AddField("CanUpgrade", false);
            }
            else
            {
                upgradeable.AddField("CanUpgrade", true);
            }
            return upgradeable;
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