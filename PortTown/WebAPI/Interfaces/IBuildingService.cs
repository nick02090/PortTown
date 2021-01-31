using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IBuildingService
    {
        Task<ICollection<Building>> GetBuildingsByTown(Guid townId);
        Task<Building> GetBuilding(Guid id);
        Task<ICollection<Building>> GetBuildings();
        Task<Building> UpdateJobs(Building building);

        #region Upgrades
        Task<Building> UpgradeLevel(Building building);
        Task<Building> StartUpgradeLevel(Building building);
        Task<JSONFormatter> CanUpgradeLevel(Building building);
        #endregion

        #region Crafting
        Task<Building> CraftBuilding(Building building);
        Task<Building> StartCraftBuilding(Building building, Guid townId);
        Task<JSONFormatter> CanCraftBuilding(Building building, Guid townId);
        #endregion

        #region Template
        Task<JSONFormatter> CheckInitialTemplateData();
        Task AddInitialTemplateData();
        Task AddDataToTemplate(Building building);
        Task<ICollection<JSONFormatter>> FilterTemplateForTown(Guid townId);
        Task<ICollection<Building>> GetTemplateBuildings();
        #endregion
    }
}
