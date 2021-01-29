using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IBuildingService
    {
        Task<Building> GetBuilding(Guid id);
        Task<ICollection<Building>> GetBuildings();
        Task<Building> UpgradeLevel(Building building);
        Task<Building> StartUpgradeLevel(Building building);
        Task<JSONFormatter> CanUpgradeLevel(Building building);
        Task<Building> UpdateJobs(Building building);
        Task<int> GatherPaymentFromBuildings(ResourceBatch cost, ICollection<Building> buildings, bool shouldUpdateDb = false);
        ICollection<Storage> FilterStorages(ResourceType resourceType, ICollection<Building> buildings);
        #region Template
        Task<JSONFormatter> CheckInitialTemplateData();
        Task AddInitialTemplateData();
        Task AddDataToTemplate(Building building);
        #endregion
    }
}
