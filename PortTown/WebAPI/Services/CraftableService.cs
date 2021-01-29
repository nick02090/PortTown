using Domain;
using System;
using System.Threading.Tasks;
using WebAPI.Interfaces;

namespace WebAPI.Services
{
    public class CraftableService : ICraftableService
    {
        private readonly ICraftableRepository CraftableRepository;

        public CraftableService(ICraftableRepository craftableRepository)
        {
            CraftableRepository = craftableRepository;
        }

        public async Task<Craftable> UpdateJobs(Craftable craftable)
        {
            if (craftable.TimeUntilCrafted.HasValue)
            {
                var timeUntilCrafted = craftable.TimeUntilCrafted.Value;
                if (timeUntilCrafted.CompareTo(DateTime.UtcNow) <= 0)
                {
                    craftable.TimeUntilCrafted = null;
                    craftable.IsFinishedCrafting = true;
                    // Store the cost for the later purpose
                    var craftableCost = craftable.RequiredResources;
                    //craftable.RequiredResources = null; TODO: CHECK THIS OUT
                    // Update the craftable and return the reference to the costs
                    await CraftableRepository.UpdateAsync(craftable);
                    craftable.RequiredResources = craftableCost;
                }
            }
            return craftable;
        }
    }
}