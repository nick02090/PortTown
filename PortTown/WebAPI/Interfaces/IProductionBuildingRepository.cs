using Domain;
using System;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IProductionBuildingRepository : IBaseRepository<ProductionBuilding>
    {
        Task<ProductionBuilding> GetByBuildingAsync(Guid buildingId);
    }
}
