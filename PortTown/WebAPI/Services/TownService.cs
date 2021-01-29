using Domain;
using System;
using System.Collections.Generic;
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

        private readonly IBuildingService BuildingService;
        private readonly IUpgradeableService UpgradeableService;

        public TownService(ITownRepository townRepository,
            IBuildingRepository buildingRepository, IItemRepository itemRepository,
            IBuildingService buildingService, IUpgradeableService upgradeableService)
        {
            // Repositories
            TownRepository = townRepository;
            BuildingRepository = buildingRepository;
            ItemRepository = itemRepository;

            // Services
            BuildingService = buildingService;
            UpgradeableService = upgradeableService;
        }

        public async Task<Town> GetTown(Guid id)
        {
            var town = await TownRepository.GetAsync(id);
            town = await UpdateJobs(town);
            town.Buildings = await UpdateTownBuildings(town);
            return town;
        }

        public async Task<ICollection<Town>> GetTowns()
        {
            var towns = await TownRepository.GetAsync();
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
                await BuildingService.GatherPaymentFromBuildings(cost, town.Buildings, true);
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
                var remainingCost = await BuildingService.GatherPaymentFromBuildings(cost, town.Buildings);
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
                buildings.Add(await BuildingService.UpdateJobs(building));
            }
            return buildings;
        }
    }
}