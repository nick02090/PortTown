using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IBuildingService
    {
        Task<bool> CanUpgradeAsync(Guid id);
    }
}
