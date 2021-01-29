using Domain;
using System.Threading.Tasks;

namespace WebAPI.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetByTokenAsync(string token);
    }
}
