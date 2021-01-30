using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IResourceBatchRepository : IBaseRepository<ResourceBatch>
    {
        Task<ResourceBatch> GetTemplateAsync(Guid id);
        Task<IEnumerable<ResourceBatch>> GetTemplateAsync();
        Task<IEnumerable<ResourceBatch>> GetByCraftableAsync(Guid craftableId);
        Task<IEnumerable<ResourceBatch>> GetByUpgradeableAsync(Guid upgradeableId);
    }
}
