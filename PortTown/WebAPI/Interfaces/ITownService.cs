using Domain;
using Domain.Enums;
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
        Task<int> GatherPaymentFromBuildings(ResourceBatch cost, ICollection<Building> buildings, bool shouldUpdateDb = false);
        ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings);
        Task<Town> CreateFromTemplate(Town template);
    }
}
