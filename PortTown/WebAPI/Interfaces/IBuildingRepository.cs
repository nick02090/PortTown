using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IBuildingRepository : IBaseRepository<Building>
    {
        Task<IEnumerable<Building>> GetTemplateAsync();
    }
}
