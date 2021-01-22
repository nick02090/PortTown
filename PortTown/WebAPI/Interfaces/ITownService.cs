using System;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface ITownService
    {
        Task ResetAsync(Guid id);
    }
}
