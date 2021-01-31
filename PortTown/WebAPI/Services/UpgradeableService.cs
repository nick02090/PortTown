using Domain;
using System;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class UpgradeableService : IUpgradeableService
    {
        private readonly IUpgradeableRepository UpgradeableRepository;
        private readonly IResourceBatchRepository ResourceBatchRepository;

        public UpgradeableService(IUpgradeableRepository upgradeableRepository,
            IResourceBatchRepository resourceBatchRepository)
        {
            UpgradeableRepository = upgradeableRepository;
            ResourceBatchRepository = resourceBatchRepository;
        }

        public async Task<Upgradeable> StartUpgradeLevel(Upgradeable upgradeable)
        {
            // Set the time for the upgrade
            upgradeable.TimeUntilUpgraded = new DateTime(DateTime.UtcNow.Ticks + upgradeable.TimeToUpgrade.Ticks);
            // Update the upgradeable
            await UpgradeableRepository.UpdateAsync(upgradeable);
            return upgradeable;
        }

        public async Task<Upgradeable> UpdateJobs(Upgradeable upgradeable)
        {
            if (upgradeable.TimeUntilUpgraded.HasValue)
            {
                var timeUntilUpgrade = upgradeable.TimeUntilUpgraded.Value;
                if (timeUntilUpgrade.CompareTo(DateTime.UtcNow) <= 0)
                {
                    upgradeable.TimeUntilUpgraded = null;
                    upgradeable.IsFinishedUpgrading = true;
                    await UpgradeableRepository.UpdateAsync(upgradeable);
                }
            }
            return upgradeable;
        }

        public async Task<Upgradeable> UpgradeLevel(Upgradeable upgradeable)
        {
            // Update the upgradable properties
            upgradeable.IsFinishedUpgrading = false;
            foreach (var cost in upgradeable.RequiredResources)
            {
                cost.Upgradeable = upgradeable;
                cost.Quantity = CalculateNewCost(cost.Quantity, upgradeable.UpgradeMultiplier);
                await ResourceBatchRepository.UpdateAsync(cost);
            }
            upgradeable.TimeToUpgrade = CalculateNewUpgradeTime(upgradeable.TimeToUpgrade, upgradeable.UpgradeMultiplier);
            // Update the upgradeable and return the reference to the costs
            await UpgradeableRepository.UpdateAsync(upgradeable);
            return upgradeable;
        }

        private int CalculateNewCost(int cost, float upgradeMultiplier)
        {
            var newCost = cost * upgradeMultiplier;
            return (int)newCost;
        }

        private DateTime CalculateNewUpgradeTime(DateTime upgradeTime, float upgradeMultiplier)
        {
            var ticks = upgradeTime.Ticks * upgradeMultiplier;
            return new DateTime((long)ticks);
        }
    }
}