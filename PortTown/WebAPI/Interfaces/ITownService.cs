using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface ITownService
    {
        Task ResetAsync(Guid id);
        Task<ICollection<Town>> GetTowns();
        Task<Town> GetTown(Guid id);
        Task<Town> UpdateJobs(Town town);
        Task<Town> UpgradeLevel(Town town);
        Task<Town> StartUpgradeLevel(Town town);
        Task<JSONFormatter> CanUpgradeLevel(Town town);
        bool DoesTownAllowUpgrade(Town town, int nextLevel);
    }
}
