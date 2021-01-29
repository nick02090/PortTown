using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface ICraftableService
    {
        Task<Craftable> UpdateJobs(Craftable craftable);
    }
}
