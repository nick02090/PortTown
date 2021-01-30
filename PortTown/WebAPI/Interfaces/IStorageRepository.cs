using Domain;
using System;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IStorageRepository : IBaseRepository<Storage>
    {
        Task<Storage> GetByBuildingAsync(Guid buildingId);
    }
}
