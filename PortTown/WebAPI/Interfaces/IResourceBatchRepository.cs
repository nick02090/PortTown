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
    }
}
