using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Task<ICollection<Item>> GetByUserAsync(Guid userId);
        Task<IEnumerable<Item>> GetTemplateAsync();
    }
}
