using Domain;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IBuildingRepository : IBaseRepository<Building>
    {
        Task<Building> GetTemplateAsync(BuildingType buildingType, string name);
        Task<Building> GetTemplateAsync(Guid id);
        Task<IEnumerable<Building>> GetTemplateAsync();
        Task<IEnumerable<Building>> GetByTownAsync(Guid townId);
    }
}
