using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface ICraftableService
    {
        Task<Craftable> UpdateJobs(Craftable craftable);
        Task<Craftable> Craft(Craftable craftable);
        Task<Craftable> StartCraft(Craftable craftable);
    }
}
