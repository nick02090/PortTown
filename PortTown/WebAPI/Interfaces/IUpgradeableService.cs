using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUpgradeableService
    {
        Task<Upgradeable> UpgradeLevel(Upgradeable upgradeable);
        Task<Upgradeable> StartUpgradeLevel(Upgradeable upgradeable);
        Task<Upgradeable> UpdateJobs(Upgradeable upgradeable);
    }
}
