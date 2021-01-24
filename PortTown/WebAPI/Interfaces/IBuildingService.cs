using Domain;
using System;
using System.Threading.Tasks;
using WebAPI.Helpers;

namespace WebAPI.Interfaces
{
    public interface IBuildingService
    {
        Task<JSONFormatter> CanUpgrade(Guid id);
        #region Template
        Task<JSONFormatter> CheckInitialTemplateData();
        Task AddInitialTemplateData();
        Task AddDataToTemplate(Building building);
        #endregion
    }
}
