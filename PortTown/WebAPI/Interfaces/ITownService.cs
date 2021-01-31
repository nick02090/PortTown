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
        Task<Town> ResetAsync(Guid id, ICollection<Building> townBuildings);
        Task<ICollection<Town>> GetTowns();
        Task<Town> GetTown(Guid id);
        Task<Town> UpdateJobs(Town town);
        Task<Town> UpgradeLevel(Town town, ICollection<Building> townBuildings);
        Task<Town> StartUpgradeLevel(Town town, ICollection<Building> townBuildings);
        Task<JSONFormatter> CanUpgradeLevel(Town town, ICollection<Building> townBuildings);
        bool DoesTownAllowUpgrade(Town town, int nextLevel);
        Task<int> GatherPaymentFromBuildings(ResourceBatch cost, ICollection<Building> buildings, bool shouldUpdateDb = false);
        ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings);
        Task<Town> CreateFromTemplate(Town template);
        Task<Building> AddBuildingToTown(Building building, Town town, bool isTemplate = false);
        Task<JSONFormatter> GetTownWithCraftInfo(Guid id, ICollection<Building> townBuildings);
    }
}
